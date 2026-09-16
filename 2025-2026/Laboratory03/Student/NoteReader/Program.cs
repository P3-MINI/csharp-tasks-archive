using System.IO;
//#define STAGE3
public static class Program
{
    public static void Main(string[] args)
    {
#if STAGE3
        string directory = FileSystemUtils.PrepareDirectory(args[0]);

        using (NoteReader noteReader = new(directory))
        {
            noteReader.RunBlinker();
        }
#endif
    }
}