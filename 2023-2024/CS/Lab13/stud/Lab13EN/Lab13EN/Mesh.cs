using OpenTK.Graphics.OpenGL4;

namespace Lab13EN
{
    public class Mesh : IDisposable
    {
        private int Vao { get; }
        private List<int> Vbos { get; } = new();
        private PrimitiveType Type { get; }
        private int Count { get; }

        public Mesh(IEnumerable<(float x, float y, float z)> positions, IEnumerable<(float x, float y, float z)> normals, IEnumerable<(float u, float v)> texCoords, IEnumerable<(uint a, uint b, uint c)> faces) :
            this(PrimitiveType.Triangles, faces.SelectMany(f => new[] { f.a, f.b, f.c }).ToArray(),
                (positions.SelectMany(v => new[] { v.x, v.y, v.z }).ToArray(), 0, 3),
                (normals.SelectMany(v => new[] { v.x, v.y, v.z }).ToArray(), 1, 3),
                (texCoords.SelectMany(v => new[] { v.u, v.v }).ToArray(), 2, 2))
        {

        }

        public Mesh(PrimitiveType type, uint[] indices, params (float[] data, int index, int size)[] buffers)
        {
            Type = type;
            Count = indices.Length;
            Vao = GL.GenVertexArray();
            GL.BindVertexArray(Vao);
            foreach (var (data, index, size) in buffers.OrderBy(buffer => buffer.index))
                LoadData(data, index, size);
            LoadIndices(indices);
            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        }

        private void LoadData(float[] data, int index, int size)
        {
            var vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, data.Length * sizeof(float), data, BufferUsageHint.StaticDraw);
            GL.EnableVertexAttribArray(index);
            GL.VertexAttribPointer(index, size, VertexAttribPointerType.Float, false, 0, 0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            Vbos.Add(vbo);
        }

        public void UpdateData(float[] data, int index)
        {
            var vbo = Vbos[index];
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferSubData(BufferTarget.ArrayBuffer, IntPtr.Zero, data.Length * sizeof(float), data);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        private void LoadIndices(uint[] data)
        {
            var vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ElementArrayBuffer, data.Length * sizeof(uint), data, BufferUsageHint.StaticDraw);
            Vbos.Add(vbo);
        }

        public void Render()
        {
            GL.BindVertexArray(Vao);
            GL.DrawElements(Type, Count, DrawElementsType.UnsignedInt, 0);
            GL.BindVertexArray(0);
        }

        public void Dispose()
        {
            GL.DeleteVertexArray(Vao);
            foreach (var vbo in Vbos)
            {
                GL.DeleteBuffer(vbo);
            }
            Vbos.Clear();
            GC.SuppressFinalize(this);
        }

        public static Mesh CreateCube(int size)
        {
            var positions = new List<(float x, float y, float z)>();
            var normals = new List<(float x, float y, float z)>();
            var textures = new List<(float u, float v)>();
            var triangles = new List<(uint a, uint b, uint c)>();
            
            positions.Add((-0.5f * size, -0.5f * size, -0.5f * size)); normals.Add((0.0f, 0.0f, -1.0f)); textures.Add((0.0f, 0.0f));
            positions.Add((+0.5f * size, -0.5f * size, -0.5f * size)); normals.Add((0.0f, 0.0f, -1.0f)); textures.Add((1.0f, 0.0f));
            positions.Add((+0.5f * size, +0.5f * size, -0.5f * size)); normals.Add((0.0f, 0.0f, -1.0f)); textures.Add((1.0f, 1.0f));
            positions.Add((-0.5f * size, +0.5f * size, -0.5f * size)); normals.Add((0.0f, 0.0f, -1.0f)); textures.Add((0.0f, 1.0f));
            
            positions.Add((+0.5f * size, -0.5f * size, +0.5f * size)); normals.Add((0.0f, 0.0f, 1.0f)); textures.Add((0.0f, 0.0f));
            positions.Add((-0.5f * size, -0.5f * size, +0.5f * size)); normals.Add((0.0f, 0.0f, 1.0f)); textures.Add((1.0f, 0.0f));
            positions.Add((-0.5f * size, +0.5f * size, +0.5f * size)); normals.Add((0.0f, 0.0f, 1.0f)); textures.Add((1.0f, 1.0f));
            positions.Add((+0.5f * size, +0.5f * size, +0.5f * size)); normals.Add((0.0f, 0.0f, 1.0f)); textures.Add((0.0f, 1.0f));
           
            positions.Add((-0.5f * size, -0.5f * size, +0.5f * size)); normals.Add((-1.0f, 0.0f, 0.0f)); textures.Add((0.0f, 0.0f));
            positions.Add((-0.5f * size, -0.5f * size, -0.5f * size)); normals.Add((-1.0f, 0.0f, 0.0f)); textures.Add((1.0f, 0.0f));
            positions.Add((-0.5f * size, +0.5f * size, -0.5f * size)); normals.Add((-1.0f, 0.0f, 0.0f)); textures.Add((1.0f, 1.0f));
            positions.Add((-0.5f * size, +0.5f * size, +0.5f * size)); normals.Add((-1.0f, 0.0f, 0.0f)); textures.Add((0.0f, 1.0f));

            positions.Add((+0.5f * size, -0.5f * size, -0.5f * size)); normals.Add((1.0f, 0.0f, 0.0f)); textures.Add((0.0f, 0.0f));
            positions.Add((+0.5f * size, -0.5f * size, +0.5f * size)); normals.Add((1.0f, 0.0f, 0.0f)); textures.Add((1.0f, 0.0f));
            positions.Add((+0.5f * size, +0.5f * size, +0.5f * size)); normals.Add((1.0f, 0.0f, 0.0f)); textures.Add((1.0f, 1.0f));
            positions.Add((+0.5f * size, +0.5f * size, -0.5f * size)); normals.Add((1.0f, 0.0f, 0.0f)); textures.Add((0.0f, 1.0f));

            positions.Add((-0.5f * size, -0.5f * size, +0.5f * size)); normals.Add((0.0f, -1.0f, 0.0f)); textures.Add((0.0f, 0.0f));
            positions.Add((+0.5f * size, -0.5f * size, +0.5f * size)); normals.Add((0.0f, -1.0f, 0.0f)); textures.Add((1.0f, 0.0f));
            positions.Add((+0.5f * size, -0.5f * size, -0.5f * size)); normals.Add((0.0f, -1.0f, 0.0f)); textures.Add((1.0f, 1.0f));
            positions.Add((-0.5f * size, -0.5f * size, -0.5f * size)); normals.Add((0.0f, -1.0f, 0.0f)); textures.Add((0.0f, 1.0f));

            positions.Add((-0.5f * size, +0.5f * size, -0.5f * size)); normals.Add((0.0f, 1.0f, 0.0f)); textures.Add((0.0f, 1.0f));
            positions.Add((+0.5f * size, +0.5f * size, -0.5f * size)); normals.Add((0.0f, 1.0f, 0.0f)); textures.Add((0.0f, 0.0f));
            positions.Add((+0.5f * size, +0.5f * size, +0.5f * size)); normals.Add((0.0f, 1.0f, 0.0f)); textures.Add((1.0f, 0.0f));
            positions.Add((-0.5f * size, +0.5f * size, +0.5f * size)); normals.Add((0.0f, 1.0f, 0.0f)); textures.Add((1.0f, 1.0f));

            triangles.Add((0, 2, 1));
            triangles.Add((0, 3, 2));
            triangles.Add((4, 6, 5));
            triangles.Add((4, 7, 6));
            triangles.Add((8, 10, 9));
            triangles.Add((8, 11, 10));
            triangles.Add((12, 14, 13));
            triangles.Add((12, 15, 14));
            triangles.Add((16, 18, 17));
            triangles.Add((16, 19, 18));
            triangles.Add((20, 22, 21));
            triangles.Add((20, 23, 22));
            return new Mesh(positions, normals, textures, triangles);
        }
    }
}