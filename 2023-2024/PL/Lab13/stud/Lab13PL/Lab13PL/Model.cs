using System.Numerics;

namespace Lab13PL;

public class Model : IDisposable
{
    public string Name { get; set; }
    public string MeshPath { get; set; }
    public string TexturePath { get; set; }

    public Vector3 Position { get; set; } = Vector3.Zero;
    public Quaternion Rotation { get; set; } = Quaternion.Identity;
    public Vector3 Scale { get; set; } = Vector3.One;
    public Spline Spline { get; set; } = new();

    public Mesh? Mesh { get; private set; }
    public Texture? Texture { get; private set; }

    public Model(string name)
    {
        Name = name;
        MeshPath = $"Resources/{name}.mesh";
        TexturePath = $"Resources/{name}.tex";
        LoadMesh();
        LoadTexture();
        Update(0);
    }

    public void Update(float dt)
    {
        Spline.Update(dt);
        var position = Spline.GetPosition();
        var rotation = Spline.GetRotation();
        Position = new Vector3(position.X, 0, position.Y);
        Rotation = Quaternion.CreateFromYawPitchRoll(rotation, 0, 0);
    }

    // TODO: Stage 1a (1pt)
    // Wczytaj plik siatki modelu znajdujący się pod ścieżką MeshPath.
    // Siatka modelu składa się z listy wierzchołków i listy ścian.
    // Każdy wierzchołek zawiera 8 atrybutów:
    // - 3 liczby zmiennoprzecinkowe określające pozycję wierzchołka
    // - 3 liczby zmiennoprzecinkowe określające wektor normalny wierzchołka
    // - 2 liczby zmiennoprzecinkowe określające współrzędne tekstury wierzchołka
    // Ściany są trójkątami o 3 indeksach, które odpowiadają wierzchołkom z listy wierzchołków
    //
    // Wczytywane pliki będą w fikcyjnym formacie .mesh:
    // Każda linia takiego pliku zaczyna się znakiem 'v' lub 't',
    // określającym czy definiowany jest wierzchołek czy trójkąt.
    // Jeżeli linia zaczyna się od znaku 'v', to następuje po nim 8 atrybutów wierzchołka.
    // Należy sparsować pozycję, wektor normalny i współrzędne tekstury tego wierzchołka,
    // i dodać je do odpowiedniej listy positions/normals/textures.
    // Jeżeli linia zaczyna się od znaku 't', to następują po nim 3 indeksy budujące trójkąt.
    // Należy sparsować te indeksy i dodać je do listy triangles.
    // Po przeczytaniu całego pliku należy stworzyć nową siatkę modelu:
    // Mesh = new Mesh(positions, normals, textures, triangles);
    //
    // Przykładowy plik można znaleźć w Resources/duck.mesh
    public void LoadMesh()
    {
        // var positions = new List<(float x, float y, float z)>();
        // var normals = new List<(float x, float y, float z)>();
        // var textures = new List<(float u, float v)>();
        // var triangles = new List<(uint a, uint b, uint c)>();
        // Fill lists using the file at path MeshPath
        // Mesh = new Mesh(positions, normals, textures, triangles);

        Mesh = Mesh.CreateCube(100);
    }

    // TODO: Stage 1b (0.5pt)
    // Wczytaj plik tekstury modelu znajdujący się pod ścieżką TexturePath.
    //
    // Wczytywane pliki będą w fikcyjnym formacie .tex:
    // Jest to plik w formacie binarnym.
    // Pierwsze cztery bajty są intem przechowującym szerokość tekstury w pikselach
    // Następne cztery bajty są intem przechowującym wysokość tekstury w pikselach
    // Następne 3 * width * height bajtów to tablica bajtów zawierająca dane tekstury,
    // czyli kolejno wartości w bajtach czerwonego, zielonego, niebieskiego komponentu
    // kolejnych pikseli z kolejnych wierszy tekstury.
    // Tablica bajtów w pliku jest już w dobrej kolejności.
    //
    // Po przeczytaniu pliku należy stworzyć nową teksturę modelu:
    // Texture = new Texture(data, width, height);
    //
    // Przykładowy plik można znaleźć w Resources/duck.tex
    public void LoadTexture()
    {
        var width = 1;
        var height = 1;

        var data = new byte[] {255, 127, 0};

        Texture = new Texture(data, width, height);
    }

    public void Dispose()
    {
        Mesh?.Dispose();
        Texture?.Dispose();
        GC.SuppressFinalize(this);
    }

    // TODO: Stage 2 (1pt)
    // Stwórz klasę MeshParseException dziedziczącą po Exception.
    // Klasa ta powinna zawierać dwa konstruktory:
    // - Przyjmujący nazwę pliku (string filename), numer linii (int line),
    //   w której wystąpił problem oraz wiadomość (string message)
    // - Przyjmujący dodatkowo wyjątek który spowodował ten wyjątek (Exception innerException)
    // Konstruktory powinny wywołać odpowiednie konstruktory z klasy bazowej Exception
    // Cała wiadomość wyjątku powinna być sformatowana w następujący sposób:
    // $"Exception while parsing \"{filename}\"({line}): {message}"
    // W metodzie LoadMesh w przypadku problemów z parsowaniem wyrzucaj odpowiednie wyjątki.
    // Rzuć wyjątkiem w następujących sytuacjach: 
    // - Nieprawidłowa liczba atrybutu wierzchołka
    // - Nieprawidłowy format atrybut wierzchołka
    // - Nieprawidłowa liczba wierzchołków trójkąta
    // - Nieprawidłowy format indeksu trójkąta
    // - Nieprawidłowy znak rozpoczynający linię
}