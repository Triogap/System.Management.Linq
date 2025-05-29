using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace System.Management.Types;

/// <summary>
/// Provides extension methods for working with WMI <see cref="ManagementObject"/> instances,
/// including property access, system property retrieval, SQL-like pattern matching, and type conversions.
/// These helpers simplify common WMI data manipulation and querying scenarios.
/// </summary>
public static class WMIExtensions
{
    /// <summary>
    /// The datetime format used for parsing and formatting WMI date and time strings.
    /// </summary>
    public const string DateTimeFormat = "yyyyMMddHHmmss.ffffff";

    /// <summary>
    /// Determines whether the specified string matches the given SQL-like pattern.
    /// </summary>
    /// <param name="value">The string to test for a match.</param>
    /// <param name="pattern">
    /// The SQL-like pattern to match against. Supports '%' as a wildcard for any sequence of characters and '_' for any single character.
    /// </param>
    /// <returns>
    /// <c>true</c> if the string matches the pattern; otherwise, <c>false</c>.
    /// </returns>
    public static bool Like(this string? value, string pattern)
        => value != null && GetRegularExpressionFromLikeExpression(pattern).IsMatch(value);

    /// <summary>
    /// Gets the system properties of the specified <see cref="ManagementObject"/>.
    /// </summary>
    /// <param name="object">The <see cref="ManagementObject"/> instance.</param>
    /// <returns>
    /// An <see cref="ISystemProperties"/> implementation for accessing system properties.
    /// </returns>
    public static ISystemProperties GetSystemProperties(this ManagementObject @object)
        => new Base._Object(@object);

    /// <summary>
    /// Gets a specific system property value from the specified <see cref="ManagementObject"/>.
    /// </summary>
    /// <typeparam name="T">The type of the property value to return.</typeparam>
    /// <param name="object">The <see cref="ManagementObject"/> instance.</param>
    /// <param name="propertyGetter">
    /// A function that retrieves the desired property from an <see cref="ISystemProperties"/> instance.
    /// </param>
    /// <returns>
    /// The value of the specified system property.
    /// </returns>
    public static T GetSystemProperty<T>(this ManagementObject @object, Func<ISystemProperties, T> propertyGetter)
        => propertyGetter(new Base._Object(@object));

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
}

