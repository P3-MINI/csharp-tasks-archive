using System.IO;
using System.IO.Compression;
using System.Net.Http.Headers;
using System.Text;

public static class Program
{
    public static void Main(string[] args)
    {
        string directory = FileSystemUtils.PrepareDirectory(args[0]);

        using (NoteReader noteReader = new(directory))
        {
            noteReader.RunBlinker();
        }
    }
}