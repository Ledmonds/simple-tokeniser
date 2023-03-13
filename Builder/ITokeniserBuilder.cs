using SimpleTokeniser.Builder.Standardisers;

namespace SimpleTokeniser.Builder;

public interface ITokeniserBuilder
{
    TokeniserBuilder Deliminate(Delimiter delimiter);

    TokeniserBuilder Deliminate(IEnumerable<Delimiter> delimiters);

    TokeniserBuilder ToLower();

    TokeniserBuilder ToUpper();

    TokeniserBuilder Standardise(ISet<Standardisation> standardisations);

    IReadOnlyCollection<Token> Tokenise();
}