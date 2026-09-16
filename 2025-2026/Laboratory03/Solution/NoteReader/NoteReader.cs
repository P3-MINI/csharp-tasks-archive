using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class NoteReader : IDisposable
{
    DirectorySource openDirectory;
    Stack<string> parents = new();
    FileSource? openFile;
    ConsolePainter.TextWindow currentWindow;
    ConsolePainter.TextWindow directoryWindow;
    ConsolePainter.TextWindow fileWindow;

    CancellationTokenSource cancellationTokenSource = new();
    InputEventGenerator inputEventGenerator;
    public NoteReader(string directory)
    {
        Console.Title = directory;
        inputEventGenerator = new(cancellationTokenSource.Token);
        openDirectory = new DirectorySource(directory);
        
        directoryWindow = new(
            (0, 0),
            (Console.WindowWidth / 2, Console.WindowHeight - 3),
            openDirectory);
        fileWindow = new(
            (Console.WindowWidth / 2 + 1, 0),
            (Console.WindowWidth - 1, Console.WindowHeight - 3),
            ConsolePainter.EmptySource<string>.Empty);
        currentWindow = directoryWindow;
        Console.SetCursorPosition(0, Console.WindowHeight - 2);
        directoryWindow.DrawBorder();

        // Task 3.2
        inputEventGenerator.Input += (s,e) => HandleInput(e.Key, e.Modifiers);
    }

    private void HandleInput(ConsoleKey key, ConsoleModifiers mods)
    {
        switch (key)
        {
            case ConsoleKey.UpArrow:
                if (currentWindow == directoryWindow && !mods.HasFlag(ConsoleModifiers.Shift))
                    openDirectory.SelectUp();
                else
                    currentWindow.ScrollUp();
                break;
            case ConsoleKey.DownArrow:
                if (currentWindow == directoryWindow && !mods.HasFlag(ConsoleModifiers.Shift))
                    openDirectory.SelectDown();
                else
                    currentWindow.ScrollDown();
                break;
            case ConsoleKey.Enter:
                if (openFile != null)
                    return;
                var path = Path.Combine(openDirectory.Name, openDirectory.Selected);
                if (File.Exists(path))
                    try
                    {
                        openFile = new FileSource(openDirectory.Name, path);
                        fileWindow.SetSource(openFile);
                        currentWindow.bold = false;
                        currentWindow = fileWindow;
                        fileWindow.DrawBorder();
                    }
                    catch (IOException)
                    {
                        FileSystemUtils.Log?.WriteLine($"{DateTime.Now}: File unavailable");
                        return;
                    }
                else if(Directory.Exists(path))
                {
                    parents.Push(openDirectory.Name);
                    openDirectory.Dispose();
                    openDirectory = new DirectorySource(path);
                    directoryWindow.SetSource(openDirectory);
                }
                break;
            case ConsoleKey.Escape:
                if (directoryWindow == currentWindow)
                    if (parents.Count == 0)
                        cancellationTokenSource.Cancel();
                    else
                    {
                        openDirectory.Dispose();
                        openDirectory = new DirectorySource(parents.Pop());
                        directoryWindow.SetSource(openDirectory);
                    }
                else if (openFile != null)
                {
                    currentWindow.Clear();
                    currentWindow = directoryWindow;
                    fileWindow.SetSource(ConsolePainter.EmptySource<string>.Empty);
                    fileWindow.Clear();
                    openFile.Dispose();
                    openFile = null;
                }
                break;
            default:
                return;
        }
    }

    public void Dispose()
    {
        cancellationTokenSource.Cancel();
        openDirectory.Dispose();
        openFile?.Dispose();
    }

    public void RunBlinker()
    {
        while (!cancellationTokenSource.Token.WaitHandle.WaitOne(500))
        {
            currentWindow.bold = !currentWindow.bold;
            currentWindow.DrawBorder();
        }
    }
}

