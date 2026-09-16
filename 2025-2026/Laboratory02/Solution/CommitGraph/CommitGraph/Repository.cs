namespace CommitGraph;

public sealed class Repository
{
    public Dictionary<string, string> Branches { get; set; } = [];
    
    public Dictionary<string, RepositoryObject> Objects { get; set; } = [];

    public Dictionary<string, Author> Authors { get; } = [];

    public IEnumerable<Commit> Commits => Objects.Values.OfType<Commit>();

    public void AddAuthor(Author a) => Authors[a.Id] = a;

    public void AddObject(RepositoryObject o) => Objects[o.Hash] = o;
    
    public void CreateBranch(string name, string hash) => Branches[name] = hash;

    public string? Head { get; set; }
}

public static class RepositoryExtensions
{
    public static bool TryGetCommit(this Repository repository, string hash, out Commit? commit)
    {
        commit = null;
        if (hash == null) return false;
        if (repository.Objects.TryGetValue(hash, out var o) && o is Commit c)
        {
            commit = c;
            return true;
        }
        return false;
    }

    public static Commit GetCommitOrThrow(this Repository repository, string hash)
    {
        if (!repository.TryGetCommit(hash, out var c))
            throw new KeyNotFoundException($"Commit {hash} not found");
        return c!;
    }

    public static IEnumerable<Commit> TraverseBranchByFirstParent(this Repository repository, string? startHash = null)
    {
        var hash = startHash ?? repository.Head;

        if (hash == null)
            yield break;

        while (hash is not null)
        {
            if (!repository.TryGetCommit(hash, out var commit))
                continue;

            yield return commit!;

            hash = commit!.ParentHashes.Count == 0
                ? null
                : commit.ParentHashes[0];
        }
    }

    public static IEnumerable<Commit> TraverseByRevision(this Repository repository, string pattern)
    {
        var revision = Revision.Parse(pattern);

        string currentHash;
        if (repository.Branches.TryGetValue(revision.BaseRef, out var branchHash))
        {
            currentHash = branchHash;
        }
        else if (string.Equals(revision.BaseRef, "HEAD", StringComparison.OrdinalIgnoreCase))
        {
            if (repository.Head == null)
                throw new InvalidOperationException("Repository HEAD is null");
            currentHash = repository.Head;
        }
        else if (repository.Objects.ContainsKey(revision.BaseRef))
        {
            currentHash = revision.BaseRef;
        }
        else
        {
            throw new KeyNotFoundException($"Base reference '{revision.BaseRef}' not found as branch or object hash.");
        }

        var baseCommit = repository.GetCommitOrThrow(currentHash);
        yield return baseCommit!;

        foreach (var modifier in revision.Modifiers)
        {
            var innerHash = currentHash;
            foreach (var hash in modifier.Apply(innerHash, repository))
            {
                currentHash = hash;
                var commit = repository.GetCommitOrThrow(currentHash);
                yield return commit!;
            }
        }
    }
}