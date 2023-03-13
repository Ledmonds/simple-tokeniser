using SimpleTokeniser.Builder.Standardisers;
using SimpleTokeniser.Builder.Tokens;
using SimpleTokeniser.Extensions;
using System.Globalization;
using System.Text;

namespace SimpleTokeniser.Builder;

public class TokeniserBuilder : ITokeniserBuilder
{
    private ISet<Token> _tokens { get; set; } = new HashSet<Token>();
    private readonly CultureInfo _cultureInfo = CultureInfo.InvariantCulture;

    private const StringSplitOptions SPLIT_OPTIONS =
        StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries;

    public TokeniserBuilder(string input)
    {
        _tokens.Add(new(input));
    }

    public TokeniserBuilder(string input, CultureInfo cultureInfo)
    {
        _tokens.Add(new(input));
        _cultureInfo = cultureInfo;
    }

    public TokeniserBuilder(IEnumerable<Token> tokens)
    {
        _tokens = tokens.ToHashSet();
    }

    public TokeniserBuilder(IEnumerable<Token> tokens, CultureInfo cultureInfo)
    {
        _tokens = tokens.ToHashSet();
        _cultureInfo = cultureInfo;
    }

    public TokeniserBuilder ToLower()
    {
        _tokens = _tokens
            .Select(token => token.Value.ToLower(_cultureInfo))
            .Select(token => new Token(token))
            .ToHashSet();

        return this;
    }

    public TokeniserBuilder ToUpper()
    {
        _tokens = _tokens
            .Select(token => token.Value.ToUpper(_cultureInfo))
            .Select(token => new Token(token))
            .ToHashSet();

        return this;
    }

    public TokeniserBuilder Select(ICustomTokeniser customTokeniser)
    {
        _tokens = _tokens.Select(customTokeniser.Tokeniser).ToHashSet();

        return this;
    }

    public TokeniserBuilder Deliminate(Delimiter delimiter)
    {
        _tokens = _tokens
            .SelectMany(token => token.Value.Split(delimiter, SPLIT_OPTIONS))
            .Select(token => new Token(token))
            .ToHashSet();

        return this;
    }

    public TokeniserBuilder Deliminate(IEnumerable<Delimiter> delimiters)
    {
        _tokens = _tokens
            .SelectMany(token => token.Value.Split(delimiters, SPLIT_OPTIONS))
            .Select(token => new Token(token))
            .ToHashSet();

        return this;
    }

    public IReadOnlyCollection<Token> Tokenise()
    {
        return _tokens.ToArray();
    }

    public TokeniserBuilder Standardise(ISet<Standardisation> standardisations)
    {
        _tokens = _tokens
            .Select(
                token =>
                    StandardiseToken(
                        token,
                        standardisations.ToDictionary(
                            pattern => pattern.Pattern,
                            mapping => mapping.Mapping
                        )
                    )
            )
            .ToHashSet();

        return this;
    }

    private Token StandardiseToken(Token token, IDictionary<string, string> standardisations)
    {
        var patterns = standardisations.Select(standardisation => standardisation.Key).ToArray();

        var tokenSegments = token.Value.SplitAndKeep(patterns);

        var stringBuilder = new StringBuilder();
        foreach (var tokenSegment in tokenSegments)
        {
            var updatedSegment = standardisations.TryGetValue(tokenSegment, out var replacement)
                ? replacement
                : tokenSegment;

            stringBuilder.Append(updatedSegment);
        }

        return stringBuilder.ToString();
    }
}