using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace System.Management.Types;

public static class WMIExtensions
{
    public const string DateTimeFormat = "yyyyMMddHHmmss.ffffff";
    internal static DateTimeOffset[]? GetDateTimePropertyValues(this ManagementObject managementObject, string propertyName)
        => managementObject[propertyName] is string[] s ? s.Select(ParseDateTime).ToArray() : null;
    internal static DateTimeOffset? GetDateTimePropertyValue(this ManagementObject managementObject, string propertyName)
        => managementObject[propertyName] is string s ? ParseDateTime(s) : null;
    private static DateTimeOffset ParseDateTime(string value)
        => new(DateTime.ParseExact(value[0..21], DateTimeFormat, CultureInfo.InvariantCulture), TimeSpan.FromMinutes(double.Parse(value[22..])));

    internal static TEnum? GetFlaggedEnumFromArray<TEnum>(this ManagementObject managementObject, string propertyName)
        where TEnum : struct, IConvertible
    {
        var value = managementObject[propertyName];
        if (value is not ushort[] values)
        {
            return null;
        }

        var flaggedValue = 0;
        for (var i = 0; i < values.Length; i++)
        {
            flaggedValue |= 1 << values[i];
        }
        return (TEnum)Convert.ChangeType(flaggedValue, typeof(TEnum));
    }

    public static bool Like(this string? value, string pattern)
        => value != null && GetRegularExpressionFromLikeExpression(pattern).IsMatch(value);

    private static Regex GetRegularExpressionFromLikeExpression(string pattern)
    {
        var expressionBuilder = new StringBuilder(pattern.Length);
        bool escaped = false;
        bool inBrackets = false;
        bool lastCharIsStar = false;

        if (pattern[0] != '%')
        {
            expressionBuilder.Append('^');
        }

        foreach (var c in pattern)
        {
            lastCharIsStar = false;
            if (escaped)
            {
                escaped = false;
            }
            else if (inBrackets)
            {
                switch (c)
                {
                    case '!':
                        expressionBuilder.Append('^');
                        continue;
                    case ']':
                        inBrackets = false;
                        break;
                }
            }
            else
            {

                switch (c)
                {
                    case '%':
                        expressionBuilder.Append(".*");
                        lastCharIsStar = true;
                        continue;
                    case '_':
                        expressionBuilder.Append('.');
                        continue;
                    case '[':
                        inBrackets = true;
                        break;
                    case '\\':
                        escaped = true;
                        break;
                }
            }
            expressionBuilder.Append(c);
        }

        if (!lastCharIsStar)
        {
            expressionBuilder.Append('$');
        }

        return new(expressionBuilder.ToString());
    }

    public static ISystemProperties GetSystemProperties(this ManagementObject @object)
        => new Base._Object(@object);

    public static T GetSystemProperty<T>(this ManagementObject @object, Func<ISystemProperties, T> propertyGetter)
        => propertyGetter(new Base._Object(@object));
}

