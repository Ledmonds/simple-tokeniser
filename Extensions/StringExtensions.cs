using SimpleTokeniser.Builder;

namespace SimpleTokeniser.Extensions;

public static class StringExtensions
{
    public static IEnumerable<string> Split(this string value, Delimiter delimiter, StringSplitOptions options = StringSplitOptions.None)
    {
        return value.Split(delimiter.Value, options);
    }

    public static IEnumerable<string> Split(this string value, IEnumerable<Delimiter> delimiters, StringSplitOptions options = StringSplitOptions.None)
    {
        var delimeterValues = delimiters
            .Select(delim => delim.Value)
            .ToArray();

        return value.Split(delimeterValues, options);
    }
}