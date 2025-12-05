using System.Text;
using System.Collections;

namespace Lab15Retake
{
    internal class FileSource : IEnumerable<char>
    {
        private string Filename { get; set; }

        public FileSource(string filename)
        {
            this.Filename = filename;
        }

        public IEnumerator<char> GetEnumerator()
        {
            using FileStream stream = File.OpenRead(Filename);

            using BinaryReader reader = new BinaryReader(stream, Encoding.ASCII);

            while (stream.Position < stream.Length)
            {
                char character = reader.ReadChar();

                yield return character;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
