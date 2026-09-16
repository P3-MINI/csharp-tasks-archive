using System.Text.RegularExpressions;

namespace CommitGraph;

public sealed record Revision
{
    private const string Base = "base";
    
    private const string Modifier = "modifier";
    
    private const string Value = "value";

    private const string pattern = @$"^(?<{Base}>[^\s~^]+)(?:(?<{Modifier}>[~^])(?<{Value}>(?:[1-9]\d*)*))*$";
    
    private static readonly Regex regex = new Regex(pattern);
    public static Revision Parse(string pattern)
    {
        var match = regex.Match(pattern);

        if (!match.Success)
            throw new InvalidOperationException($"Pattern '{pattern}' does not follow revision syntax");

        var baseRef = match.Groups[Base].Value;
        var modifierSymbols = match.Groups[Modifier].Captures;
        var modifierValues = match.Groups[Value].Captures;

        var modifiers = modifierSymbols
            .Zip(modifierValues, (symbol, value) =>
            {
                var n = value.Value.Length != 0
                    ? int.Parse(value.Value)
                    : 1;

                IRevisionModifier modifier = symbol.Value == AncestorModifier.Symbol
                        ? new AncestorModifier(n)
                        : new ParentModifier(n);

                return modifier;
            })
            .ToList();

        return new Revision()
            {
                BaseRef = baseRef,
                Modifiers = modifiers,
            };
    }

    public string BaseRef { get; set; } = "HEAD";

    public IReadOnlyList<IRevisionModifier> Modifiers { get; set; } = [];

    public override string ToString()
    {
        var mods = string.Join("", Modifiers.Select(m => m.ToString()));
        return $"{BaseRef}{mods}";
    }
}