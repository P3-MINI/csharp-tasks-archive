using System.Reflection;
using static System.Console;

namespace SharpArgs.Console;

public sealed class Program
{
    static void Main(string[] args)
    {
        try
        {
            var assembly = Assembly.GetExecutingAssembly();
            SharpOptionsAssemblyValidator.ValidateAssembly(assembly);
            WriteLine("All SharpOptions models are valid.");
        }
        catch (AggregateException ex)
        {
            WriteLine("SharpOptions model validation failed with the following errors:");
            
            var oldColor = ForegroundColor;
            ForegroundColor = ConsoleColor.Red;
            foreach (var innerEx in ex.InnerExceptions)
            {
                WriteLine($"  - [{innerEx.GetType().Name}] {innerEx.Message}");
            }
            ForegroundColor = oldColor;
        }

        WriteLine("Press any key to continue...");
        ReadKey();
    }
}