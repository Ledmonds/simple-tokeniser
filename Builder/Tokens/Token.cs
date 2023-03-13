namespace SimpleTokeniser.Builder;

public record Token(string Value)
{
    public static implicit operator Token(string value) => new(value);
};