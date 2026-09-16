namespace CommitGraph;

public static class SampleRepository
{
    public static Repository GetSampleRepository()
    {
        var repo = new Repository();

        // --- Authors ---
        var knuth = new Author("knuth", "Donald Knuth", "knuth@artofprogramming.com");
        var turing = new Author("turing", "Alan Turing", "turing@enigma.dev");
        var lovelace = new Author("lovelace", "Ada Lovelace", "ada@analytical.engine");
        var hoare = new Author("hoare", "Tony Hoare", "hoare@quicksort.org");
        var dijkstra = new Author("dijkstra", "Edsger Dijkstra", "dijkstra@structured.dev");

        repo.AddAuthor(knuth);
        repo.AddAuthor(turing);
        repo.AddAuthor(lovelace);
        repo.AddAuthor(hoare);
        repo.AddAuthor(dijkstra);

        static string fakeSHA1(int id) => $"c{id}";

        // --- Commits History ---
        var c1 = new Commit
        {
            Hash = fakeSHA1(1),
            Timestamp = new DateTime(2025, 1, 10, 10, 0, 0),
            AuthorId = lovelace.Id,
            Message = "Initial commit: Project structure and solution file",
            ParentHashes = [],
            Changes =
            [
                new FileChange("LibraryManager.sln", 25, 0),
                new FileChange("README.md", 15, 0),
                new FileChange(".gitignore", 50, 0)
            ]
        };
        repo.AddObject(c1);

        var c2 = new Commit
        {
            Hash = fakeSHA1(2),
            Timestamp = new DateTime(2025, 1, 12, 14, 30, 0),
            AuthorId = knuth.Id,
            Message = "feat: Add core domain models for Book and Author",
            ParentHashes = [c1.Hash],
            Changes =
            [
                new FileChange("LibraryManager.Domain/Models/Book.cs", 60, 0),
                new FileChange("LibraryManager.Domain/Models/Author.cs", 45, 0)
            ]
        };
        repo.AddObject(c2);

        var c3 = new Commit
        {
            Hash = fakeSHA1(3),
            Timestamp = new DateTime(2025, 1, 15, 9, 0, 0),
            AuthorId = turing.Id,
            Message = "feat(auth): Add User model and basic repository interface",
            ParentHashes = [c2.Hash],
            Changes =
            [
                new FileChange("LibraryManager.Domain/Models/User.cs", 55, 0),
                new FileChange("LibraryManager.Domain/Interfaces/IUserRepository.cs", 20, 0)
            ]
        };
        repo.AddObject(c3);

        var c4 = new Commit
        {
            Hash = fakeSHA1(4),
            Timestamp = new DateTime(2025, 1, 16, 11, 0, 0),
            AuthorId = knuth.Id,
            Message = "feat: Implement BookService for managing book catalog",
            ParentHashes = [c2.Hash],
            Changes =
            [
                new FileChange("LibraryManager.Application/Services/BookService.cs", 150, 0),
                new FileChange("LibraryManager.Domain/Interfaces/IBookRepository.cs", 22, 0)
            ]
        };
        repo.AddObject(c4);

        var c5 = new Commit
        {
            Hash = fakeSHA1(5),
            Timestamp = new DateTime(2025, 1, 17, 18, 0, 0),
            AuthorId = dijkstra.Id,
            Message = "fix: Correctly handle null ISBN in Book model",
            ParentHashes = [c4.Hash],
            Changes = [new FileChange("LibraryManager.Domain/Models/Book.cs", 5, 3)]
        };
        repo.AddObject(c5);

        var c6 = new Commit
        {
            Hash = fakeSHA1(6),
            Timestamp = new DateTime(2025, 1, 18, 12, 0, 0),
            AuthorId = turing.Id,
            Message = "feat(auth): Implement in-memory user repository and user service",
            ParentHashes = [c3.Hash],
            Changes =
            [
                new FileChange("LibraryManager.Infrastructure/Repositories/InMemoryUserRepository.cs", 120, 0),
                new FileChange("LibraryManager.Application/Services/UserService.cs", 90, 0)
            ]
        };
        repo.AddObject(c6);

        var c7 = new Commit
        {
            Hash = fakeSHA1(7),
            Timestamp = new DateTime(2025, 1, 20, 10, 0, 0),
            AuthorId = hoare.Id,
            Message = "refactor(auth): Simplify user creation logic in UserService",
            ParentHashes = [c6.Hash],
            Changes = [new FileChange("LibraryManager.Application/Services/UserService.cs", 25, 15)]
        };
        repo.AddObject(c7);

        var c8 = new Commit
        {
            Hash = fakeSHA1(8),
            Timestamp = new DateTime(2025, 1, 22, 9, 30, 0),
            AuthorId = turing.Id,
            Message = "Merge branch 'feature/users' into main",
            ParentHashes = [c5.Hash, c7.Hash],
            Changes =
            [
                new FileChange("LibraryManager.Domain/Models/User.cs", 55, 0),
                new FileChange("LibraryManager.Domain/Interfaces/IUserRepository.cs", 20, 0),
                new FileChange("LibraryManager.Infrastructure/Repositories/InMemoryUserRepository.cs", 120, 0),
                new FileChange("LibraryManager.Application/Services/UserService.cs", 100, 0) // Zmiany z refaktorem
            ]
        };
        repo.AddObject(c8);

        var c9 = new Commit
        {
            Hash = fakeSHA1(9),
            Timestamp = new DateTime(2025, 2, 1, 15, 0, 0),
            AuthorId = lovelace.Id,
            Message = "feat: Introduce Loan model and LoanService",
            ParentHashes = [c8.Hash],
            Changes =
            [
                new FileChange("LibraryManager.Domain/Models/Loan.cs", 70, 0),
                new FileChange("LibraryManager.Application/Services/LoanService.cs", 220, 0)
            ]
        };
        repo.AddObject(c9);

        var c10 = new Commit
        {
            Hash = fakeSHA1(10),
            Timestamp = new DateTime(2025, 2, 5, 17, 0, 0),
            AuthorId = hoare.Id,
            Message = "perf: Optimize book search algorithm using quicksort on titles",
            ParentHashes = [c9.Hash],
            Changes =
            [
                new FileChange("LibraryManager.Application/Services/BookService.cs", 40, 20)
            ]
        };
        repo.AddObject(c10);

        var c11 = new Commit
        {
            Hash = fakeSHA1(11),
            Timestamp = new DateTime(2025, 2, 8, 11, 45, 0),
            AuthorId = dijkstra.Id,
            Message = "test: Add unit tests for LoanService business logic",
            ParentHashes = [c10.Hash],
            Changes = [new FileChange("LibraryManager.Tests/Services/LoanServiceTests.cs", 350, 0)]
        };
        repo.AddObject(c11);

        var c12 = new Commit
        {
            Hash = fakeSHA1(12),
            Timestamp = new DateTime(2025, 2, 10, 16, 0, 0),
            AuthorId = knuth.Id,
            Message = "docs: Update README with API usage examples",
            ParentHashes = [c11.Hash],
            Changes = [new FileChange("README.md", 80, 5)]
        };
        repo.AddObject(c12);

        // --- Branches and HEAD ---
        repo.CreateBranch("main", c12.Hash);
        repo.CreateBranch("feature/users", c7.Hash);
        repo.Head = c12.Hash;

        return repo;
    }
}