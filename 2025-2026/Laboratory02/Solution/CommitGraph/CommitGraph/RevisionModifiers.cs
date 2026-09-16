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
        ArgumentNullException.ThrowIfNull(hash);

        if (!repository.TryGetCommit(hash, out var commit))
            throw new KeyNotFoundException($"Commit '{hash}' not found");

        var index = Nth - 1;
        if (commit?.ParentHashes is null || index < 0 || index >= commit.ParentHashes.Count)
            throw new InvalidOperationException($"Commit {hash} does not have parent #{index}");

        yield return commit.ParentHashes[index];
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
        ArgumentNullException.ThrowIfNull(hash);

        for (var i = 0; i < Nth; i++)
        {
            if (!repository.TryGetCommit(hash, out var commit))
                throw new KeyNotFoundException($"Commit '{hash}' not found");
            if (commit?.ParentHashes == null || commit.ParentHashes.Count == 0)
                throw new InvalidOperationException($"Commit 'hash' has no parent");

            hash = commit.ParentHashes[0];
            yield return hash;
        }
    }

    public override string ToString()
    {
        var n = Nth == 1
            ? string.Empty
            : Nth.ToString();

        return $"{Symbol}{n}";
    }
}