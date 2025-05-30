using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using System.Threading.Channels;
namespace System.Management.Generator;

internal partial class DefinitionLoader(ChannelWriter<ClassDefinition> channel)
{

    [GeneratedRegex(@"<code class=""lang-syntax"">([^<]*)</code>")]
    private static partial Regex GetCodeRegex();
    private static readonly Regex CodeRegex = GetCodeRegex();
    [GeneratedRegex(@"<p[^>]*>(.*)</p>")]
    private static partial Regex GetParagraphRegex();
    private static readonly Regex ParagraphRegex = GetParagraphRegex();
    [GeneratedRegex(@"<dt[^>]*>(.*)</dt>")]
    private static partial Regex GetPropertyRegex();
    private static readonly Regex PropertyRegex = GetPropertyRegex();
    [GeneratedRegex(@"</dl>(.*)</dd>")]
    private static partial Regex GetDescriptionRegex();
    private static readonly Regex DescriptionRegex = GetDescriptionRegex();
    [GeneratedRegex(@",? *([^(,]*)\(""?([^)""]*)""?\)")]
    private static partial Regex GetQualifierRegex();
    private static readonly Regex QualifierRegex = GetQualifierRegex();
    [GeneratedRegex(@"<.*?>")]
    private static partial Regex GetHTMLReplaceRegex();
    private static readonly Regex HTMLReplaceRegex = GetHTMLReplaceRegex();
    [GeneratedRegex(@"<tr[^>]*>(.*?)</tr>", RegexOptions.Singleline)]
    private static partial Regex GetTableRowRegex();
    private static readonly Regex TableRowRegex = GetTableRowRegex();
    [GeneratedRegex(@"<td[^>]*>(.*?)</td>", RegexOptions.Singleline)]
    private static partial Regex GetTableCellRegex();
    private static readonly Regex TableCellRegex = GetTableCellRegex();
    [GeneratedRegex(@"<a[^>]* href=[""']([^""']*)[""'][^>]*>")]
    private static partial Regex GetLinkRegex();
    private static readonly Regex LinkRegex = GetLinkRegex();

    private readonly Dictionary<string, string> _typeMismatches = new() { ["Win32_LogicalElement"] = "CIM_LogicalElement" };
    private readonly HashSet<string> _loadedClassDefinitions = new(StringComparer.OrdinalIgnoreCase);

    public async Task Load(IEnumerable<string> classesToLoad)
    {
        foreach (var className in classesToLoad)
        {
            await TryLoadClass(className);
        }

        channel.Complete();
    }

    private async Task<string?> TryLoadClass(string? className)
    {
        if (className == null)
        {
            return null;
        }

        if (_typeMismatches.TryGetValue(className, out var correctClassName))
        {
            return await TryLoadClass(correctClassName);
        }

        if (_loadedClassDefinitions.Add(className))
        {
            if (await LoadClassDefinition(className) is ClassDefinition classDefinition)
            {
                Console.WriteLine($"Loaded info for {className}:");
                await channel.WriteAsync(classDefinition);
                return className;
            }
            else if (className.IndexOf('_') == -1)
            {
                var prefixedClassName = $"__{className}";
                _typeMismatches[className] = prefixedClassName;
                return await TryLoadClass(prefixedClassName);
            }
        }

        return null;
    }

    private async Task<ClassDefinition?> LoadClassDefinition(string className)
    {
        Console.WriteLine($"Loading info for {className}:");
        Uri? classUri = null;
        Uri[] uris = className[0] == '_'
            ? [new Uri($"https://learn.microsoft.com/en-us/windows/win32/wmisdk/{className.Replace('_', '-')}")]
            : [new Uri($"https://learn.microsoft.com/en-us/windows/win32/cimwin32prov/{className.Replace('_', '-')}"),
               new Uri($"https://learn.microsoft.com/en-us/previous-versions/windows/desktop/secrcw32prov//{className.Replace('_', '-')}")];

        string? pageContents = null;
        for (var i = 0; i < uris.Length; ++i)
        {
            classUri = uris[i];
            try
            {
                pageContents = await GetPageContentsAsync(classUri);
                break;
            }
            catch (Exception ex)
            {
                if (i + 1 == uris.Length)
                {
                    ErrorReporter.Report($"Unable to find Microsoft Learn page to parse for {className}: {ex.Message}", ex, throwOrBreak: false);
                    break;
                }
            }
        }

        if (classUri == null || pageContents == null)
        {
            return null;
        }

        var codeMatch = CodeRegex.Match(pageContents);
        if (!codeMatch.Success)
        {
            Console.WriteLine($"Failed to find expected code block for {className}.");
            return new(className, null) { Properties = [], Methods = [] };
        }
        var codeLines = codeMatch.Groups[1].Value.Split(['\n', '\r'], options: StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        string? superClass;
        var lineIndex = 0;
        while (true)
        {
            var classLine = codeLines[lineIndex];
            if (!classLine.StartsWith("class"))
            {
                if (++ lineIndex == codeLines.Length)
                {
                    ErrorReporter.Report($"No class definition found while parsing data for {className}.");
                }
                continue;
            }

            var classDef = classLine.Split(' ', options: StringSplitOptions.RemoveEmptyEntries);
            if (!classDef[1].Equals(className, StringComparison.InvariantCultureIgnoreCase))
            {
                ErrorReporter.Report($"Encounterd different type {classDef[1]} when parsing data for {className}.");
            }

            superClass = await TryLoadClass(classDef.Length > 2 ? classDef[^1] : null);

            break;
        }

        var propertyBlock = pageContents.IndexOf("<h3 id=\"properties\"");
        var endBlock = pageContents.IndexOf("<h3", propertyBlock + 1);
        var properties = propertyBlock == -1 ? [] : await ParseProperties(codeLines[(lineIndex + 2)..^1], pageContents[propertyBlock..endBlock]);

        var methodBlock = pageContents.IndexOf("<h3 id=\"methods\"");
        endBlock = pageContents.IndexOf("<h3", methodBlock + 1);
        List<MethodDefinition> methods = methodBlock == -1 ? [] : await ParseMethods(classUri, pageContents[methodBlock..endBlock]);

        Console.WriteLine($"Super: {superClass}, Properties: {properties.Count}, Methods: {methods.Count}");
        return new(className, superClass) { Properties = properties, Methods = methods };
    }

    private static async Task<string> GetPageContentsAsync(Uri url)
    {
        using HttpClient client = new();
        using HttpResponseMessage response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }

    private async Task<List<PropertyDefinition>> ParseProperties(string[] propertyLines, string propertiesBlock)
    {
        var result = new List<PropertyDefinition>(propertyLines.Length);
        foreach (var propertyLine in propertyLines)
        {
            if (await ParsePropertyLine(propertyLine) is PropertyDefinition property)
            {
                if (TryGetPropertyV1(property.Name, propertiesBlock, out var propertyBlock))
                {
                    if (PropertyIsInherited(propertyBlock))
                    {
                        continue;
                    }
                    result.Add(await UpdatePropertyV1(property, propertyBlock));
                }
                else if (TryGetPropertyV2(property.Name, propertiesBlock, out propertyBlock))
                {
                    if (PropertyIsInherited(propertyBlock))
                    {
                        continue;
                    }
                    result.Add(await UpdatePropertyV2(property, propertyBlock));
                }
                else
                {
                    ErrorReporter.Report($"No description found for property {property.Name}.", throwOrBreak: false);
                }
            }
        }

        return result;
    }

    private static bool TryGetPropertyV1(string propertyName, string propertiesBlock, [MaybeNullWhen(false)]out string propertyBlock)
    {
        var startIndex = propertiesBlock.IndexOf($"<p><strong>{propertyName}</strong></p>");
        if (startIndex == -1)
        {
            propertyBlock = null;
            return false;
        }
        var endIndex = propertiesBlock.IndexOf("</dd>", startIndex);

        propertyBlock = propertiesBlock[(startIndex + 1)..endIndex];
        return true;
    }

    private static bool TryGetPropertyV2(string propertyName, string propertiesBlock, [MaybeNullWhen(false)] out string propertyBlock)
    {
        var startIndex = propertiesBlock.IndexOf($"<dt><b>{propertyName}</b></dt>");
        if (startIndex == -1)
        {
            propertyBlock = null;
            return false;
        }
        var endIndex = propertiesBlock.IndexOf("</dd>", startIndex);

        propertyBlock = propertiesBlock[(startIndex + 1)..endIndex];
        return true;
    }

    private static bool PropertyIsInherited(string propertyBlock)
        => propertyBlock.Contains("This property is inherited from");

    private async Task<PropertyDefinition> UpdatePropertyV1(PropertyDefinition property, string propertyBlock)
    {
        foreach (var paragraph in ParagraphRegex.Matches(propertyBlock).Select(TrimHTML))
        {
            (var parsed, property) = await ParseSubProperty(property, paragraph);
            if (!parsed)
            {
                property = property with { Description = paragraph };
                break;
            }
        }

        return property;
    }

    private async Task<PropertyDefinition> UpdatePropertyV2(PropertyDefinition property, string propertyBlock)
    {
        foreach (var propertyDescription in PropertyRegex.Matches(propertyBlock).Select(TrimHTML))
        {
            (_, property) = await ParseSubProperty(property, propertyDescription);
        }

        if (TrimHTML(DescriptionRegex.Match(propertyBlock)) is string description && description.Length > 0)
        {
            property = property with { Description = description };
        }

        return property;
    }

    private async Task<(bool, PropertyDefinition)> ParseSubProperty(PropertyDefinition property, string paragraph)
    {
        var colonIndex = paragraph.IndexOf(':');
        if (colonIndex == -1)
        {
            return (false, property);
        }

        switch (paragraph[..colonIndex])
        {
            case "Data type":
                if (property.Type == CimType.None)
                {
                    var typeName = paragraph[(colonIndex + 2)..];
                    if (Enum.TryParse<CimType>(typeName, ignoreCase: true, out var type))
                    {
                        property = property with { Type = type };
                    }
                    else if (typeName.EndsWith(" array") &&
                        Enum.TryParse(typeName[..^6], ignoreCase: true, out type))
                    {
                        property = property with { IsArray = true, Type = type };
                    }
                    else if (typeName.Contains('_'))
                    {
                        property = property with { Type = CimType.Reference, ReferencedClass = await TryLoadClass(typeName) };
                    }
                    else
                    {
                        ErrorReporter.Report($"Unable to determine type for {property.Name}: {paragraph}");
                    }
                }
                break;
            case "Access type":
                switch (paragraph[(colonIndex + 2)..])
                {
                    case "Read-only":
                        break;
                    case "Read/write":
                        property = property with { ReadOnly = false };
                        break;
                    default:
                        ErrorReporter.Report($"No support implemented for {paragraph}");
                        break;
                }
                break;
            case "Qualifiers":
                property = property with { Qualifiers = ParseQualifiers(paragraph[(colonIndex + 2)..]) };
                break;
        }
        return (true, property);
    }

    private static List<QualifierDefinition> ParseQualifiers(string qualifiers)
        => QualifierRegex.Matches(qualifiers).Select(GetQualifier).ToList();

    private static QualifierDefinition GetQualifier(Match qualifier)
        => new(qualifier.Groups[1].Value.Trim(), qualifier.Groups[2].Value.Trim());

    private static string TrimHTML(Match source)
        => source.Success 
            ?  TrimHTML(source.Groups[^1].Value)
            : string.Empty;

    private static string TrimHTML(string source)
        => HTMLReplaceRegex
            .Replace(source, string.Empty)
            .Replace("&nbsp;", " ")
            .Trim();

    private async Task<PropertyDefinition?> ParsePropertyLine(string propertyLine)
    {
        var parts = TrimHTML(propertyLine).Split(' ', options: StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length == 1)
        {
            return null;
        }

        string? referenceType = null;
        if (!Enum.TryParse(parts[0], ignoreCase: true, out CimType type) && parts[0].Contains('_'))
        {
            type = CimType.Reference;
            referenceType = await TryLoadClass(parts[0]);
        }

        var name = parts[^2][0] == '=' ? parts[^3] : parts[^1];
        var isArray = name[^2] == ']';
        if (isArray)
        {
            name = name[..name.IndexOf('[')];
        }
        else if (name[^1] == ';')
        {
            name = name[..^1];
        }

        return new(type, isArray, name, referenceType);
    }
    /// <summary>
    /// Schedules Chkdsk to be run at the next restart if the dirty bit has been set.
    /// </summary>
    /// <remarks>This method is only applicable to those instances of logical disk that represent a physical disk in the machine. This method is not applicable to mapped logical drives.</remarks>
    /// <param name="classUri">Specifies the list of drives to schedule for Autochk at the next reboot. The string syntax consists of the drive letter followed by a colon for the logical disk, for example: "C:"</param>
    /// <param name="methodBlock"></param>
    /// <returns>Returns a value of 0 (zero) if successful, and some other value if any other error occurs. Values are listed in the following list.</returns>
    private static Task<List<MethodDefinition>> ParseMethods(Uri classUri, string methodBlock)
    {
        List<MethodDefinition> result = [];
        // TODO: Implement method parsing logic
        //var tBodyIndex = methodBlock.IndexOf("<tbody");
        //foreach (Match methodRow in TableRowRegex.Matches(methodBlock[tBodyIndex..]))
        //{
        //    List<string> cells = [..TableCellRegex.Matches(methodRow.Groups[1].Value).Select(c => c.Groups[1].Value)];
        //    if (cells.Count != 2)
        //    {
        //        ErrorReporter.Report($"Found unexpected number({cells.Count}) of cells in method Row.");
        //    }

        //    var nameCell = cells[0];
        //    var linkMatch = LinkRegex.Match(nameCell);
        //    if (!linkMatch.Success)
        //    {
        //        continue;
        //    }

        //    var name = TrimHTML(nameCell);
        //    var description = TrimHTML(cells[1]);
        //    var methodPageContent = await GetPageContentsAsync(new Uri(classUri, linkMatch.Groups[1].Value));
        //}

        return Task.FromResult(result);
    }
}
