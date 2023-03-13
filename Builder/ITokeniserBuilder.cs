using SimpleTokeniser.Builder.Tokens;

namespace SimpleTokeniser.Builder
{
    public interface ITokeniserBuilder
    {
        TokeniserBuilder Deliminate(Delimiter delimiter);

        TokeniserBuilder Deliminate(IEnumerable<Delimiter> delimiters);

        TokeniserBuilder ToLower();

        TokeniserBuilder ToUpper();

        IReadOnlyCollection<Token> Tokenise();
    }
}