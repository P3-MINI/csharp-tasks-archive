namespace CommitGraph;

public sealed record FileChange(
    string Path, 
    int Insertions, 
    int Deletions
);
