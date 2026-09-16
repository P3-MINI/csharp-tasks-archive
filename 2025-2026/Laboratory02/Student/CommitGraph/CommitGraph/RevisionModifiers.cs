namespace CommitGraph;

public interface IRevisionModifier
{
    IEnumerable<string> Apply(string hash, Repository repository);
}

public record ParentModifier(int Nth = 1) : IRevisionModifier
{
    public const string Symbol = "^";

    public IEnumerable<string> Apply(string hash, Repository repository)
    {
        // TODO
        throw new NotImplementedException();
    }

    public override string ToString()
    {
        var n = Nth == 1
            ? string.Empty
            : Nth.ToString();

        return $"{Symbol}{n}";
    }
}

public record AncestorModifier(int Nth = 1) : IRevisionModifier
{
    public const string Symbol = "~";

    public IEnumerable<string> Apply(string hash, Repository repository)
    {
        // TODO
        throw new NotImplementedException();
    }

    public override string ToString()
    {
        var n = Nth == 1
            ? string.Empty
            : Nth.ToString();

        return $"{Symbol}{n}";
    }
}