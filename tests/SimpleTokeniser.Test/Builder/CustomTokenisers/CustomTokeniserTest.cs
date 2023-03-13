using SimpleTokeniser.Builder;

namespace SimpleTokeniser.Test.Builder.CustomTokenisers;

public class CustomTokeniserTest
{
    [Fact]
    public void Injects_and_evaluates_a_custom_tokeniser_to_reverse_all_tokens()
    {
        // Arrange
        Func<Token, Token> customTokeniser = (token) =>
        {
            return token.Value.Reverse()?.ToString() ?? string.Empty;
        };

        var tokens = new Token[] { "these", "are", "some", "tokens" };
        var result = new TokeniserBuilder(tokens).Select(customTokeniser).Tokenise();

        var expected = new Token[] { "eseht", "era", "emos", "snekot" };
    }
}