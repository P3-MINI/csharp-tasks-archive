public static class FileSystemUtils
{
    public static StreamWriter? Log { get; private set; }
    public static string PrepareDirectory(string path) // Stage1.1
    {
        throw new NotImplementedException();
    }
    public static int CountFiles(string path, string suffix = "") // Stage1.2
    {
        throw new NotImplementedException();
    }
    public static void WatchDirectory(string path) // Stage2.2
    {
        throw new NotImplementedException();

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
