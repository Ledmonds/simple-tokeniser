using SimpleTokeniser.Builder.Tokens;
using SimpleTokeniser.Extensions;
using System.Globalization;

namespace SimpleTokeniser.Builder;

public class TokeniserBuilder : ITokeniserBuilder
{
    private ISet<Token> _tokens { get; set; } = new HashSet<Token>();
    private readonly CultureInfo _cultureInfo = CultureInfo.InvariantCulture;

    public TokeniserBuilder(string input)
    {
        _tokens.Add(new(input));
    }

    public TokeniserBuilder(string input, CultureInfo cultureInfo)
    {
        _tokens.Add(new(input));
        _cultureInfo = cultureInfo;
    }

    public TokeniserBuilder ToLower()
    {
        _tokens = _tokens
            .Select(token =>
                token.Value.ToLower(_cultureInfo))
            .Select(token => new Token(token))
            .ToHashSet();

        return this;
    }

    public TokeniserBuilder ToUpper()
    {
        _tokens = _tokens
            .Select(token =>
                token.Value.ToUpper(_cultureInfo))
            .Select(token => new Token(token))
            .ToHashSet();

        return this;
    }

    public TokeniserBuilder Select(ICustomTokeniser customTokeniser)
    {
        _tokens = _tokens
            .Select(customTokeniser.Tokeniser)
            .ToHashSet();

        return this;
    }

    public TokeniserBuilder Deliminate(Delimiter delimiter)
    {
        _tokens = _tokens
            .SelectMany(token =>
                token.Value.Split(delimiter, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries))
            .Select(token => new Token(token))
            .ToHashSet();

        return this;
    }

    public TokeniserBuilder Deliminate(IEnumerable<Delimiter> delimiters)
    {
        _tokens = _tokens
            .SelectMany(token =>
                token.Value.Split(delimiters, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries))
            .Select(token => new Token(token))
            .ToHashSet();

        return this;
    }

    public IReadOnlyCollection<Token> Tokenise()
    {
        return _tokens.ToArray();
    }
}