using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DirectoryWatcher : IDisposable
{
    public event EventHandler? DirectoryChanged;
    public string[] Files => [.. files];
    public string[] Directories => [.. directories];
    public DirectoryWatcher(string directory)
    {
        var c = Directory.GetCurrentDirectory();
        if (!Directory.Exists(directory))
            throw new ArgumentException("Directory does not exist");

        files = Directory.EnumerateFiles(directory).Select((p) => Path.GetFileName(p)).ToList();
        directories = Directory.EnumerateDirectories(directory).Select((p) => Path.GetFileName(p)).ToList();

        fileWatcher = new FileSystemWatcher(directory);
        fileWatcher.NotifyFilter = NotifyFilters.FileName;
        fileWatcher.Renamed += FileRenamed;
        fileWatcher.Created += (s, e) => files.Add(e.Name);
        fileWatcher.Deleted += (s, e) => files.Remove(e.Name);
        
        directoryWatcher = new FileSystemWatcher(directory);
        directoryWatcher.NotifyFilter = NotifyFilters.DirectoryName;
        directoryWatcher.Renamed += DirectoryRenamed;
        directoryWatcher.Created += (s, e) => directories.Add(e.Name);
        directoryWatcher.Deleted += (s, e) => directories.Remove(e.Name);

        fileWatcher.Renamed += OnDirectoryChanged;
        fileWatcher.Created += OnDirectoryChanged;
        fileWatcher.Deleted += OnDirectoryChanged;
        directoryWatcher.Renamed += OnDirectoryChanged;
        directoryWatcher.Created += OnDirectoryChanged;
        directoryWatcher.Deleted += OnDirectoryChanged;

        fileWatcher.EnableRaisingEvents = true;
        directoryWatcher.EnableRaisingEvents = true;
    }
    private void OnDirectoryChanged(object sender, FileSystemEventArgs e)
    {
        DirectoryChanged?.Invoke(this, EventArgs.Empty);
    }

    private void FileRenamed(object sender, RenamedEventArgs e)
    {
        int id = files.FindIndex((s) => s == e.OldName);
        if (id == -1)
            throw new UnreachableException("unregistered file renamed");

        files[id] = e.Name;
        DirectoryChanged?.Invoke(this, EventArgs.Empty);
    }
    private void DirectoryRenamed(object sender, RenamedEventArgs e)
    {
        int id = directories.FindIndex((s) => s == e.OldName);
        if (id == -1)
            throw new UnreachableException("unregistered directory renamed");

        directories[id] = e.Name;
        DirectoryChanged?.Invoke(this, EventArgs.Empty);
    }

    public void Dispose()
    {
        FileSystemUtils.Log?.WriteLine($"{DateTime.Now}: Disposed {this}.");
        fileWatcher.Dispose();
        directoryWatcher.Dispose();
    }

    private readonly List<string> files;
    private readonly List<string> directories;
    private FileSystemWatcher fileWatcher;
    private FileSystemWatcher directoryWatcher;
}
