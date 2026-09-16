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