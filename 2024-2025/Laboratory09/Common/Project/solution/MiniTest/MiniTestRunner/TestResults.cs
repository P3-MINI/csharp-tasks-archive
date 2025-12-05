using System.Text;

namespace MiniTestRunner;

public readonly record struct TestResults(int Passed, int Total)
{
    public int Failed => Total - Passed;

    public static TestResults operator +(TestResults left, TestResults right)
    {
        int passed = left.Passed + right.Passed;
        int run = left.Total + right.Total;
        return new TestResults(passed, run);
    }

    private bool PrintMembers(StringBuilder builder)
    {
        builder.Append($"Tests passed: {Passed}/{Total}, Failed: {Failed}");
        return true;
    }

    public void Summarize()
    {
        Console.WriteLine("******************************");
        Console.WriteLine($"* Test passed: {Passed,5} / {Total,-5} *");
        Console.WriteLine($"* Failed:      {Failed,5}         *");
        Console.WriteLine("******************************");
    }
}