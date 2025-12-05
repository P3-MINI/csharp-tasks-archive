using System.Text;
using System.Collections;

namespace Lab15Retake
{
    internal class FileSource : IEnumerable<Vector3D<float>>
    {
        private string Filename { get; set; }

        public FileSource(string filename)
        {
            this.Filename = filename;
        }

        public IEnumerator<Vector3D<float>> GetEnumerator()
        {
            using FileStream stream = File.OpenRead(Filename);

            using BinaryReader reader = new BinaryReader(stream);

            while (stream.Position < stream.Length)
            {
                float x = reader.ReadSingle();
                float y = reader.ReadSingle();
                float z = reader.ReadSingle();

                yield return new Vector3D<float>(x, y, z);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
