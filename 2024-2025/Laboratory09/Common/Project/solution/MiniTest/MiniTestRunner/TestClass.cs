namespace MiniTestRunner;

public record TestClass(
    Type Type,
    List<TestMethod> TestMethods,
    TestSetup? BeforeEach,
    TestSetup? AfterEach,
    string? Description)
{
    public TestResults Run()
    {
        TestResults results = new TestResults();
        var instance = Activator.CreateInstance(Type);
        if (instance is null)
        {
            using var _ = new ConsoleColoring(ConsoleColor.Yellow);
            Console.WriteLine($"Failed to create a class {Type.FullName} instance.");
            return results;
        }
        
        Console.WriteLine($"Running tests from class {Type.FullName}...");
        if (Description is not null) Console.WriteLine(Description);
        
        foreach (var testMethod in TestMethods
                     .OrderBy(tm => tm.Priority)
                     .ThenBy(tm => tm.MethodInfo.Name))
        {
            BeforeEach?.Run(instance);
            results += testMethod.Run(instance);
            AfterEach?.Run(instance);
        }

        results.Summarize();
        return results;
    }
}