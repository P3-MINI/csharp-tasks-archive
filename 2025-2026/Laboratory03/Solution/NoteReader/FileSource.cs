using ConsolePainter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class FileSource : IDataSource<string>, IDisposable
{
    public string Name { get; }
    public IEnumerable<string> Data => lines;
    public int Count => lines.Count;
    public event EventHandler? DataChanged;
    public FileSource(string containingDirectory, string file)
    {
        Name = Path.Combine(containingDirectory, file);
        if(!File.Exists(Name))
            throw new FileNotFoundException();
        lines = [.. File.ReadLines(Name)];
        watcher = new FileSystemWatcher(containingDirectory);
        watcher.Filter = file;
        watcher.NotifyFilter = NotifyFilters.LastWrite;
        watcher.Changed += FileChanged;
        watcher.EnableRaisingEvents = true;
    }
    private void FileChanged(object sender, FileSystemEventArgs e)
    {
        while (!Guard.FileReadAvailable(Name));
        lines.Clear();
        lines.AddRange(File.ReadLines(Name));
        DataChanged?.Invoke(this, EventArgs.Empty);
    }

    public void Dispose()
    {
        FileSystemUtils.Log?.WriteLine($"{DateTime.Now}: Disposed {this}.");
        watcher.Dispose();
    }
    FileSystemWatcher watcher;
    List<string> lines;
}

