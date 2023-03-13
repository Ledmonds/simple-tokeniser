using SimpleTokeniser.Builder;

namespace SimpleTokeniser.Test.Builder.Delimiters;

public class DeliminationTest
{
    public static IEnumerable<object[]> TestData()
    {
        yield return new object[]
        {
            "This, is: some basic lorem-ipsum text.",
            new Delimiter[] { " " },
            new Token[] { "This,", "is:", "some", "basic", "lorem-ipsum", "text." }
        };

        yield return new object[]
        {
            " what about delims at the start or end ",
            new Delimiter[] { " " },
            new Token[] { "what", "about", "delims", "at", "the", "start", "or", "end" }
        };

        yield return new object[]
        {
            "This, is: some basic lorem-ipsum text.",
            new Delimiter[] { " ", "," },
            new Token[] { "This", "is:", "some", "basic", "lorem-ipsum", "text." }
        };

        yield return new object[]
        {
            "This, is: some basic lorem-ipsum text.",
            new Delimiter[] { " ", ",", ":", ".", "-" },
            new Token[] { "This", "is", "some", "basic", "lorem", "ipsum", "text" }
        };

        yield return new object[]
        {
            "Let us try deliminating on entire words",
            new Delimiter[] { "us", "ing" },
            new Token[] { "Let", "try deliminat", "on entire words" }
        };

        yield return new object[]
        {
            "Let us try deliminating on entire words, and symbols",
            new Delimiter[] { "us", "ing", " ", "," },
            new Token[] { "Let", "try", "deliminat", "on", "entire", "words", "and", "symbols" }
        };
    }

    [Theory]
    [MemberData(nameof(TestData))]
    public void Deliminates_tokens_with_given_delimiter_strings(
        string input,
        Delimiter[] delimiters,
        Token[] expected
    )
    {
        // Act
        var result = new TokeniserBuilder(input).Deliminate(delimiters).Tokenise();

        // Assert
        result.Should().BeEquivalentTo(expected, opts => opts.WithStrictOrdering());
    }

    [Fact]
    public void Deliminates_tokens_with_a_given_delimiter_string()
    {
        // Arrange
        var input = "this is some kind of input";
        var delimiter = new Delimiter(" ");

        // Act
        var result = new TokeniserBuilder(input).Deliminate(delimiter).Tokenise();

        // Assert
        var expected = new Token[] { "this", "is", "some", "kind", "of", "input" };
        result.Should().BeEquivalentTo(expected, opts => opts.WithStrictOrdering());
    }
}