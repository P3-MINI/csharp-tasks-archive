namespace CommitGraph;

public record RepositoryObject
{
    public string Hash { get; set; } = default!;

    public DateTime Timestamp { get; set; }
}

public sealed record Commit : RepositoryObject
{
    public string Message { get; init; } = default!;

    public string AuthorId { get; init; } = default!;

    public IReadOnlyList<string> ParentHashes { get; init; } = [];

    public IReadOnlyList<FileChange> Changes { get; init; } = [];

    public int TotalLinesChanged =>
        (Changes?.Sum(ch => Math.Abs(ch.Insertions) + Math.Abs(ch.Deletions))) ?? 0;

    public void PrettyPrint(Repository repository)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"commit {Hash}");
        Console.ResetColor();

        if (repository.Authors.TryGetValue(AuthorId, out var author))
        {
            Console.WriteLine($"Author: {author.Name} <{author.Email}>");
        }
        else
        {
            Console.WriteLine($"Author: {AuthorId}");
        }

        Console.WriteLine($"Date:   {Timestamp:ddd MMM dd HH:mm:ss yyyy}");

        Console.WriteLine();

        Console.ForegroundColor = ConsoleColor.Green;
        var indentedMessage = string.Join(
            Environment.NewLine,
            Message.Split('\n').Select(line => $"    {line}")
        );
        Console.WriteLine(indentedMessage);
        Console.ResetColor();

        Console.WriteLine();
        Console.WriteLine();
    }
}