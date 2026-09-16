using System.Text.Json;

namespace CommitGraph;

public static class RepositoryQueries
{
    public static void RunQueries(this Repository repository)
    {
        repository.CommitWithHighestTotalLinesChanged();
        repository.AverageNumberOfFilesPerCommitByAuthor();
        repository.AuthorWithTheMostCommits();
        repository.TheFirstCommitForEachFile();
        repository.FilesWithMostContributors();
    }
    /// <summary>
    /// Finds the single commit with the highest number of lines changed (added + deleted).
    /// If there's a tie, it's resolved by the author's name alphabetically.
    /// Returns an object with the following fields:
    /// - Commit (object): Contains Hash and Message.
    /// - TotalLinesChanged (int): The sum of added and deleted lines.
    /// - AuthorName (string): The name of the commit's author.
    /// </summary>
    public static void CommitWithHighestTotalLinesChanged(this Repository repository)
    {
        var commits = repository.Commits.ToList();
        var authors = repository.Authors.Values.ToList();

        var queryResult = commits
            .Join(authors,
                commit => commit.AuthorId,
                author => author.Id,
                (commit, author) => new
                {
                    Commit = new { commit.Hash, commit.Message },
                    commit.TotalLinesChanged,
                    AuthorName = author.Name
                })
            .OrderByDescending(x => x.TotalLinesChanged)
            .ThenBy(x => x.AuthorName, StringComparer.OrdinalIgnoreCase)
            .FirstOrDefault();

        Console.WriteLine("Commit With Highest Total Lines Changed:");
        DisplayQueryResult(queryResult);
        Console.WriteLine();
    }

    /// <summary>
    /// Calculates the average number of unique files touched per commit for each author.
    /// Returns a list of objects, sorted by the average in descending order.
    /// Each object in the list contains:
    /// - AuthorName (string): The name of the author.
    /// - AvgFilesPerCommit (double): The calculated average number of files per commit.
    /// </summary>
    public static void AverageNumberOfFilesPerCommitByAuthor(this Repository repository)
    {
        var commits = repository.Commits.ToList();
        var authors = repository.Authors.Values.ToList();

        var queryResult = commits
            .GroupBy(c => c.AuthorId)
            .Join(authors,
                commitGroup => commitGroup.Key,
                author => author.Id,
                (commitGroup, author) => new
                {
                    AuthorName = author.Name,
                    AvgFilesPerCommit = commitGroup
                        .Average(c => c.Changes.DistinctBy(f => f.Path).Count())
                })
            .OrderByDescending(x => x.AvgFilesPerCommit)
            .ThenBy(x => x.AuthorName, StringComparer.OrdinalIgnoreCase)
            .ToList();

        Console.WriteLine("Average Number Of Files Per Commit By Author:");
        DisplayQueryResult(queryResult);
        Console.WriteLine();
    }

    /// <summary>
    /// Finds the single author who has made the most commits.
    /// Ties are broken alphabetically by the author's name.
    /// Returns an object with the following fields:
    /// - AuthorName (string): The name of the author.
    /// - CommitsCount (int): The total number of commits made by the author.
    /// </summary>
    public static void AuthorWithTheMostCommits(this Repository repository)
    {
        var commits = repository.Commits.ToList();
        var authors = repository.Authors.Values.ToList();

        var queryResult = commits
            .GroupBy(c => c.AuthorId)
            .Select(g => new
            {
                AuthorId = g.Key,
                CommitsCount = g.Count()
            })
            .Join(authors,
                stat => stat.AuthorId,
                author => author.Id,
                (stat, author) => new
                {
                    AuthorName = author.Name,
                    stat.CommitsCount
                })
            .OrderByDescending(x => x.CommitsCount)
            .ThenBy(x => x.AuthorName, StringComparer.OrdinalIgnoreCase)
            .FirstOrDefault();

        Console.WriteLine("Author With The Most Commits:");
        DisplayQueryResult(queryResult);
        Console.WriteLine();
    }

    /// <summary>
    /// For every file in the repository's history, finds the very first commit that introduced or modified it.
    /// Returns a list of objects, one for each unique file path, sorted by path.
    /// Each object contains:
    /// - Path (string): The file path.
    /// - FirstCommitHash (string): The hash of the first commit affecting this file.
    /// - Timestamp (DateTime): The timestamp of that commit.
    /// - AuthorName (string): The name of the author of that commit.
    /// - Message (string): The message of that commit.
    /// </summary>
    public static void TheFirstCommitForEachFile(this Repository repository)
    {
        var commits = repository.Commits.ToList();
        var authors = repository.Authors.Values.ToList();

        var queryResult = commits
            .SelectMany(c => c.Changes.Select(ch => new { ch.Path, Commit = c }))
            .GroupBy(x => x.Path)
            .Select(g =>
            {
                var firstChange = g.OrderBy(x => x.Commit.Timestamp).First();
                return new
                {
                    Path = g.Key,
                    FirstCommit = firstChange.Commit
                };
            })
            .Join(authors,
                fileInfo => fileInfo.FirstCommit.AuthorId,
                author => author.Id,
                (fileInfo, author) => new
                {
                    fileInfo.Path,
                    FirstCommitHash = fileInfo.FirstCommit.Hash,
                    fileInfo.FirstCommit.Timestamp,
                    AuthorName = author.Name,
                    fileInfo.FirstCommit.Message
                })
            .OrderBy(x => x.Path, StringComparer.OrdinalIgnoreCase)
            .ToList();

        Console.WriteLine("The First Commit For Each File:");
        DisplayQueryResult(queryResult);
        Console.WriteLine();
    }

    /// <summary>
    /// Finds the file (or files) that have been worked on by the highest number of distinct authors.
    /// Returns a list of objects for each file that meets this "most contributors" criteria, sorted by path.
    /// Each object contains:
    /// - Path (string): The file path.
    /// - DistinctAuthorsCount (int): The count of unique authors who touched this file.
    /// - Authors (List<string>): A sorted list of the names of these authors.
    /// </summary>
    public static void FilesWithMostContributors(this Repository repository)
    {
        var commits = repository.Commits.ToList();
        var authors = repository.Authors.Values.ToList();

        var queryResult = commits
            .SelectMany(c => c.Changes.Select(ch => new { ch.Path, c.AuthorId }))
            .GroupBy(x => x.Path)
            .Select(g => new
            {
                Path = g.Key,
                DistinctAuthorIds = g.Select(x => x.AuthorId).Distinct().ToList()
            })
            .Select(x => new
            {
                x.Path,
                x.DistinctAuthorIds,
                DistinctAuthorsCount = x.DistinctAuthorIds.Count
            })
            .GroupBy(x => x.DistinctAuthorsCount)
            .OrderByDescending(g => g.Key)
            .FirstOrDefault()?
            .SelectMany(fileGroup => fileGroup.DistinctAuthorIds
                .Join(authors,
                    authorId => authorId,
                    author => author.Id,
                    (authorId, author) => new { fileGroup.Path, fileGroup.DistinctAuthorsCount, AuthorName = author.Name })
            )
            .GroupBy(x => new { x.Path, x.DistinctAuthorsCount })
            .Select(g => new
            {
                g.Key.Path,
                g.Key.DistinctAuthorsCount,
                Authors = g.Select(x => x.AuthorName).OrderBy(name => name).ToList()
            })
            .OrderBy(x => x.Path)
            .ToList();

        Console.WriteLine("Files With Most Contributors:");
        DisplayQueryResult(queryResult);
        Console.WriteLine();
    }

    private static JsonSerializerOptions serializerOptions 
        = new() { WriteIndented = true };

    private static void DisplayQueryResult<T>(T query)
    {
        var json = JsonSerializer.Serialize(query, serializerOptions);

        Console.WriteLine(json);
    }
}