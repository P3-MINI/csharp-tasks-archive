using System.Reflection;

namespace MiniTestRunner;

public record TestMethod(
    MethodInfo MethodInfo,
    List<TestParameters>? Parameters,
    string? Description,
    int Priority = int.MinValue)
{
    public TestResults Run(object instance)
    {
        return Parameters is {Count: > 0} ? RunParameterizedTest(instance) : RunTest(instance);
    }
    
    private TestResults RunTest(object instance)
    {
        Console.Write($"{MethodInfo.Name,-60}: ");
        TestResults results;
        try
        {
            using var _ = new ConsoleColoring(ConsoleColor.Green);
            MethodInfo.Invoke(instance, []);
            Console.WriteLine("PASSED");
            results = new TestResults(1, 1);
        }
        catch (Exception e)
        {
            using var _ = new ConsoleColoring(ConsoleColor.Red);
            Console.WriteLine("FAILED");
            Console.WriteLine(e.InnerException?.Message);
            results = new TestResults(0, 1);
        }

        if (Description is not null)
        {
            Console.WriteLine(Description);
        }
        return results;
    }

    private TestResults RunParameterizedTest(object obj)
    {
        TestResults results = new TestResults();
        Console.WriteLine(MethodInfo.Name);
        foreach (var param in Parameters!)
        {
            Console.Write($" - {param.Description,-57}: ");
            try
            {
                using var _ = new ConsoleColoring(ConsoleColor.Green);
                MethodInfo.Invoke(obj, param.Parameters);
                Console.WriteLine("PASSED");
                results += new TestResults(1, 1);
            }
            catch (Exception e)
            {
                using var _ = new ConsoleColoring(ConsoleColor.Red);
                Console.WriteLine("FAILED");
                Console.WriteLine(e.InnerException?.Message);
                results += new TestResults(0, 1);
            }
        }

        if (Description is not null)
        {
            Console.WriteLine(Description);
        }
        return results;
    }

}