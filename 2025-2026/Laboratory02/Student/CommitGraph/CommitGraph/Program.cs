//#define COMMIT_HISTORY
//#define QUERIES
namespace CommitGraph;

public sealed class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("-- Lab 2 --");
        PressAnyKeyToContinue();
        var repository = SampleRepository.GetSampleRepository();
#if COMMIT_HISTORY
        Console.WriteLine("--- Commit History ---");
        Console.WriteLine();
        foreach (var commit in repository.TraverseBranchByFirstParent())
        {
            commit.PrettyPrint(repository);
        }
        Console.WriteLine("--- End of History ---");

        PressAnyKeyToContinue();

        Console.WriteLine("--- Commit History from HEAD to HEAD~4^2~4 ---");
        Console.WriteLine();
        foreach (var commit in repository.TraverseByRevision("HEAD~4^2~4"))
        { 
            commit.PrettyPrint(repository); 
        }
        Console.WriteLine("--- End of History ---");

        PressAnyKeyToContinue();
#endif // COMMIT_HISTORY
#if QUERIES
        Console.WriteLine("--- Queries ---");

        repository.RunQueries();

        PressAnyKeyToContinue();
#endif // QUERIES
    }

    private static void PressAnyKeyToContinue()
    {
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}