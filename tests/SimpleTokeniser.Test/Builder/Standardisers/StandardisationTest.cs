using SimpleTokeniser.Builder;
using SimpleTokeniser.Builder.Standardisers;

namespace SimpleTokeniser.Test.Build;

public class TokeniserStandardisationTest
{
    public static IEnumerable<object[]> WholeWordTestData()
    {
        var wordStandardisation = new HashSet<Standardisation>()
        {
            new("fuck", "duck"),
            new("shit", "flower"),
        };

        // Whole words are standardised in place.
        yield return new object[]
        {
            new Token[] { "Lets", "see", "how", "this", "handles", "some", "fucking", "shiting" },
            wordStandardisation,
            new Token[] { "Lets", "see", "how", "this", "handles", "some", "ducking", "flowering" },
        };
    }

    public static IEnumerable<object[]> GermanStandardisationTestData()
    {
        var germanStandardisations = new HashSet<Standardisation>()
        {
            new("ä", "ae"),
            new("ö", "oe"),
            new("ü", "ue"),
            new("ß", "ss"),
        };

        // Text remains unaffected when there is nothing to standardise
        yield return new object[]
        {
            new Token[] { "This", "text", "contains", "no", "patterns", "to", "standardise" },
            germanStandardisations,
            new Token[] { "This", "text", "contains", "no", "patterns", "to", "standardise" },
        };

        // Single character German umlauts are standardised in place
        yield return new object[]
        {
            new Token[] { "This", "text", "contains", "für", "patterns", "it's", "süßes" },
            germanStandardisations,
            new Token[] { "This", "text", "contains", "fuer", "patterns", "it's", "suesses" },
        };

        // Standardisation is case sensitive
        yield return new object[]
        {
            new Token[] { "fÜr" },
            germanStandardisations,
            new Token[] { "fÜr" },
        };
    }

    [Theory]
    [MemberData(nameof(WholeWordTestData))]
    [MemberData(nameof(GermanStandardisationTestData))]
    public void Standardises_patterns_in_tokens_with_their_mapping_equivalent(Token[] input, ISet<Standardisation> standardisations, Token[] expected)
    {
        var result = new TokeniserBuilder(input)
            .Standardise(standardisations)
            .Tokenise();

        result.Should().BeEquivalentTo(expected, opts =>
            opts.WithStrictOrdering());
    }

    [Fact]
    public void Standardisation_does_not_recursively_standardise_mappings()
    {
        var circularReferencingStandardisations = new HashSet<Standardisation>()
        {
            new("a", "e"),
            new("e", "a"),
        };

        var input = new Token[] { "essential", "text", "contains", "no", "patterns", "to", "standardise" };

        var result = new TokeniserBuilder(input)
            .Standardise(circularReferencingStandardisations)
            .Tokenise();

        var expected = new Token[] { "assantiel", "taxt", "conteins", "no", "pettarns", "to", "stenderdisa" };

        result.Should().BeEquivalentTo(expected, opts =>
            opts.WithStrictOrdering());
    }
}