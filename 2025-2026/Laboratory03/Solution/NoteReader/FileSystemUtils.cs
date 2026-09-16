using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class FileSystemUtils
{
    public static StreamWriter? Log { get; private set; }
    public static string PrepareDirectory(string path)
    {
        if (!Directory.Exists(path))
        {
            var archive = path;
            if (!File.Exists(archive) || Path.GetExtension(path) != ".zip")
                throw new FileSystemException("Argument was neither a directory nor a zip archive.");
            path = Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(archive));
            if (Directory.Exists(path))
                Directory.Delete(path, true);
            ZipFile.ExtractToDirectory(archive, path);
        }
        return path;
    }
    public static int CountFiles(string path, string end = "")
    {
        int count = 0;
        foreach (var entry in Directory.EnumerateFileSystemEntries(path))
            if (Directory.Exists(entry))
                count += CountFiles(entry, end);
            else if (entry.EndsWith(end))
                count++;
        return count;
    }
    public static void WatchDirectory(string path)
    {
        using DirectoryWatcher watcher = new DirectoryWatcher(path);
        void OnDirectoryChanged(object? s, EventArgs e)
        {
            foreach (var dir in watcher.Directories)
                Console.WriteLine($"d:{dir}");
            foreach (var file in watcher.Files)
                Console.WriteLine($"f:{file}");
        }
        watcher.DirectoryChanged += OnDirectoryChanged;

        Log = new StreamWriter(Path.Combine(path, $"{DateTime.Now.ToString("yyyy_MM_dd hh_mm_ss")}.log"));
        AppDomain.CurrentDomain.ProcessExit += (s, e) =>
        {
            Log?.WriteLine($"{DateTime.Now}: Application exit.");
            Log?.Dispose();
            Log = null;
        };
        Console.ReadLine();
    }
    public class FileSystemException(string? message) : Exception(message) { }
}

