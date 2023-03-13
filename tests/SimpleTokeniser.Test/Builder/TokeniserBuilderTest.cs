using SimpleTokeniser.Builder;

namespace SimpleTokeniser.Test.Builder;

public class TokeniserBuilderTest
{
    [Fact]
    public void ToUpper_uppers_all_tokens()
    {
        var tokens = new Token[] { "this", "is", "a", "BUNCH", "of", "lower", "case", "tokens" };

        var result = new TokeniserBuilder(tokens).ToUpper().Tokenise();

        var expected = new Token[] { "THIS", "IS", "A", "BUNCH", "OF", "LOWER", "CASE", "TOKENS" };
        result.Should().BeEquivalentTo(expected, opts => opts.WithStrictOrdering());
    }

    [Fact]
    public void ToLower_uppers_all_tokens()
    {
        var tokens = new Token[] { "THIS", "IS", "A", "bunch", "OF", "UPPER", "CASE", "TOKENS" };

        var result = new TokeniserBuilder(tokens).ToLower().Tokenise();

        var expected = new Token[] { "this", "is", "a", "bunch", "of", "upper", "case", "tokens" };
        result.Should().BeEquivalentTo(expected, opts => opts.WithStrictOrdering());
    }
}