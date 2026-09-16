using ConsolePainter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// » selected directory
// - unselected directory
// ■ selected file
// · unselected file
internal class DirectorySource : IDataSource<string>, IDisposable
{
    public string Name { get; }
    public IEnumerable<string> Data
    {
        get
        {
            int i = 0;
            foreach (var dir in watcher.Directories)
                yield return $"{(i++ == Select ? "»" : "-"),2}{dir} ({FileSystemUtils.CountFiles(Path.Combine(Name, dir), "en.md")})";
            foreach (var fil in watcher.Files)
                yield return $"{(i++ == Select ? "■" : "·"),2}{fil}";
            yield break;
        }
    }
    public int Count => watcher.Directories.Length + watcher.Files.Length;
    public event EventHandler? DataChanged;

    public DirectorySource(string directory)
    {
        watcher = new DirectoryWatcher(directory);
        Name = directory;
        watcher.DirectoryChanged += (s, e) =>
        {
            Select = Math.Min(Select, Count - 1);
            DataChanged?.Invoke(this, EventArgs.Empty);
        };
    }
    public int Select { get; private set; } = 0;
    public string Selected => Select < watcher.Directories.Length ? watcher.Directories[Select] : watcher.Files[Select - watcher.Directories.Length];
    public void SelectUp()
    {
        if (Select > 0)
            Select--;
        DataChanged?.Invoke(watcher, EventArgs.Empty);
    }
    public void SelectDown()
    {
        if (Select + 1 < watcher.Directories.Length + watcher.Files.Length)
            Select++;
        DataChanged?.Invoke(watcher, EventArgs.Empty);
    }

    public void Dispose()
    {
        FileSystemUtils.Log?.WriteLine($"{DateTime.Now}: Disposed {this}.");
        watcher.Dispose();
    }

    private DirectoryWatcher watcher;
}

