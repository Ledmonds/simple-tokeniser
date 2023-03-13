namespace SimpleTokeniser.Builder.Tokens;

public interface ICustomTokeniser
{
    Func<Token, Token> Tokeniser { get; }
}