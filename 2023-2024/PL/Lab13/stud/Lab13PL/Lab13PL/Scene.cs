namespace Lab13PL;

public class Scene : IDisposable
{
    public List<Model> Models { get; } = new();

    // TODO: Stage 3 (1.5pt)
    // Stwórz katalog pod podaną ścieżką
    // Jeżeli już istnieje to usuń wszystkie pliki które się tam znajdują
    // Przeiteruj po wszystkich modelach w liście Models i zserializuj je
    // do pliku o nazwie $"{model.Name}({i}).json" używając odpowiedniej serializacji.
    // Żeby zagwarantować poprawną serializację zserializuj także pola (opcja IncludeFields = true).
    // Nie serializuj właściwości Mesh i Texture!
    // Dodaj odpowiednie szczegóły implementacyjne do klasy Model.
    public void Serialize(string path)
    {
        
    }

    // TODO: Stage 4 (1pt)
    // Zdeserializuj pod wskazaną ścieżką wszystkie pliki o rozszerzeniu .json
    // Pamiętaj o tym że właściwości Mesh i Texture nie zostały zserializowane.
    // Na wczytanych obiektach należy po zserializowaniu na nowo wczytać teksturę i siatkę.
    // W tym celu użyj interfejsu IJsonOnDeserialized do wywołania metod LoadMesh i LoadTexture.
    // Żeby zagwarantować poprawną deserializację zdeserializuj także pola (opcja IncludeFields = true).
    // Dodaj wszystkie zdeserializowane modele do listy modeli Models.
    // Dodaj odpowiednie szczegóły implementacyjne do klasy Model.
    public void Deserialize(string path)
    {
        
    }

    public void Clear()
    {
        foreach (var model in Models)
        {
            model.Dispose();
        }
        Models.Clear();
    }

    public void Dispose()
    {
        Clear();
        GC.SuppressFinalize(this);
    }
}