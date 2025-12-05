namespace MiniTestRunner;

public readonly struct ConsoleColoring : IDisposable
{
    public ConsoleColor PreviousForeground { get; }
    public ConsoleColor? PreviousBackground { get; }
    
    public ConsoleColoring()
    {
        PreviousForeground = Console.ForegroundColor;
        PreviousBackground = Console.BackgroundColor;
        Console.ResetColor();
    }

    public ConsoleColoring(ConsoleColor foreground, ConsoleColor? background = null)
    {
        PreviousForeground = Console.ForegroundColor;
        Console.ForegroundColor = foreground;
        if (background is {} bgColor)
        {
            PreviousBackground = Console.BackgroundColor;
            Console.BackgroundColor = bgColor;
        }
    }

    public void Dispose()
    {
        if (PreviousBackground is { } bgColor)
        {
            Console.BackgroundColor = bgColor;
        }
        Console.ForegroundColor = PreviousForeground;
    }
}