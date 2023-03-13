using SimpleTokeniser.Builder;

namespace SimpleTokeniser.Extensions;

public static class StringExtensions
{
    public static IEnumerable<string> Split(
        this string input,
        Delimiter delimiter,
        StringSplitOptions options = StringSplitOptions.None
    )
    {
        return input.Split(delimiter.Value, options);
    }

    public static IEnumerable<string> Split(
        this string input,
        IEnumerable<Delimiter> delimiters,
        StringSplitOptions options = StringSplitOptions.None
    )
    {
        var delimeterValues = delimiters.Select(delim => delim.Value).ToArray();

        return input.Split(delimeterValues, options);
    }

    public static IEnumerable<string> SplitAndKeep(
        this string input,
        IEnumerable<string> delimiters
    )
    {
        var splits = new List<string>() { input };

        foreach (var delimiter in delimiters)
        {
            var lastElementTracker = splits.Count - 1;

            for (int i = 0; i < splits.Count; ++i)
            {
                int index = splits[i].IndexOf(delimiter);
                if (index > -1 && splits[i].Length > index)
                {
                    var leftHanging = splits[i][..index];
                    var rightHanging = splits[i][(index + delimiter.Length)..];

                    splits[i] = leftHanging;
                    splits.Insert(i + 1, delimiter);
                    splits.Insert(i + 2, rightHanging);

                    // Skip over the delimeter element you have just added.
                    i += 1;
                }
            }
        }

        return splits.Where(segment => !string.IsNullOrEmpty(segment));
    }
}