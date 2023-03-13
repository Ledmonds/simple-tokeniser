namespace SimpleTokeniser.Builder;

public record Delimiter(string Value)
{
    public static implicit operator Delimiter(string value) => new(value);
};