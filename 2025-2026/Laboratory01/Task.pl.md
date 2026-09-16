### Wprowadzenie do Grafów

Graf to struktura danych, która składa się ze zbioru **wierzchołków** oraz zbioru **krawędzi**, które łączą pary wierzchołków. Grafy są używane do modelowania relacji między obiektami i są szeroko stosowane w różnych dziedzinach, takich jak sieci społecznościowe, systemy map i nawigacji, sieci komputerowe i inne.

Istnieje kilka sposobów reprezentacji grafu w pamięci. Dwie najczęstsze reprezentacje to:

1.  **Macierz sąsiedztwa**: Dwuwymiarowa tablica, w której wpis `macierz[i][j]` ma wartość `1` lub `true`, jeśli istnieje krawędź od wierzchołka `i` do wierzchołka `j`, a `0` lub `false` w przeciwnym razie. Ta reprezentacja jest wydajna do sprawdzania, czy istnieje krawędź między dwoma wierzchołkami, ale może zużywać dużo pamięci dla grafów rzadkich (grafów z małą liczbą krawędzi).

2.  **Lista sąsiedztwa**: Tablica list, w której lista o indeksie `i` zawiera wszystkie wierzchołki sąsiadujące z wierzchołkiem `i`. Ta reprezentacja jest bardziej oszczędna pamięciowo dla grafów rzadkich, ponieważ przechowuje tylko istniejące krawędzie.

W tym laboratorium zaimplementujesz obie te reprezentacje.

### **Wymagania ogólne**

Celem tego laboratorium jest zaimplementowanie struktury danych grafu skierowanego oraz algorytmu przeszukiwania w głąb (DFS). Wszystkie klasy powinny znajdować się w przestrzeni nazw `Graphs`.

### **Etap 1: Reprezentacja grafu (6 punktów)**

Ten etap koncentruje się na stworzeniu podstawowej struktury grafu i dwóch różnych implementacji reprezentacji krawędzi grafu.

* **1.1. Abstrakcyjna klasa `Graph` (2 punkty)**
  * Zdefiniuj publiczną klasę abstrakcyjną `Graph`.
  * Klasa ta powinna mieć chroniony konstruktor `Graph(int vertexCount)`, który inicjalizuje publiczną właściwość tylko do odczytu `int VertexCount`.
  * Zadeklaruj następujące metody abstrakcyjne:
    * publiczną `List<int> GetOutVertices(int vertex);` - zwraca listę sąsiadów wierzchołka `vertex`
    * publiczną `void AddEdge(int from, int to);` - dodaje skierowaną krawędź do grafu
  * Zaimplementuj publiczną metodę `void Display()`, która wypisuje informacje o sąsiedztwie grafu na konsolę. Wynik dla każdego wierzchołka powinien być w formacie `wierzchołek -> [sasiad1,sasiad2,...]`, na przykład:
    ```
    0 -> [1,2]
    1 -> [3]
    2 -> []
    3 -> [1]
    ```

* **1.2. Implementacja listy sąsiedztwa (2 punkty)**
  * Stwórz publiczną klasę `AdjacencyListGraph` dziedziczącą po `Graph`.
  * Powinna mieć publiczny konstruktor `AdjacencyListGraph(int vertexCount)`.
  * Zaimplementuj metody `GetOutVertices` i `AddEdge` używając `List<int>[]` do przechowywania struktury grafu.

* **1.3. Implementacja macierzy sąsiedztwa (2 punkty)**
  * Stwórz publiczną klasę `AdjacencyMatrixGraph` dziedziczącą po `Graph`.
  * Powinna mieć publiczny konstruktor `AdjacencyMatrixGraph(int vertexCount)`.
  * Zaimplementuj metody `GetOutVertices` i `AddEdge` używając `bool[,]` do przechowywania struktury grafu.

### **Etap 2: Przechodzenie grafu i wyszukiwanie ścieżki (6 punktów)**

Ten etap obejmuje dodanie logiki przechodzenia po grafie za pomocą algorytmu DFS i zaimplementowanie wzorca wizytora do przetwarzania wierzchołków podczas przechodzenia.

Przeszukiwanie w głąb (DFS) to algorytm do przechodzenia lub przeszukiwania struktur danych w postaci drzewa lub grafu. Algorytm rozpoczyna przechodzenie od węzła głównego (lub dowolnego węzła w grafie) i eksploruje każdą gałąź tak daleko, jak to możliwe, przed powrotem. Jest on zazwyczaj implementowany przy użyciu rekurencji.

* **2.1. Algorytm DFS (3 punkty)**
  * Dodaj publiczną metodę `void DepthFirstSearch(int start, IVertexVisitor visitor)` do abstrakcyjnej klasy `Graph`.
  * Metoda ta powinna implementować algorytm DFS rekurencyjnie.
  * Przechodzenie powinno zaczynać się od wierzchołka `start`.
  * Przed zejściem rekurencyjnym, metoda powinna wywołać metodę `PreVisit` z interfejsu `IVertexVisitor`
  * Po zejściu rekurencyjnym, metoda powinna wywołać metodę `PostVisit` z interfejsu `IVertexVisitor`
  * Metoda może używać metody pomocniczej, na przykład `DepthFirstSearchRecursive(int vertex, bool[] visited, IVertexVisitor visitor)`.
    * Użyj tablicy `bool[] visited`, aby oznaczać i pomijać już odwiedzone wierzchołki.

* **2.1. Interfejs `IVertexVisitor`**
  * Zdefiniuj publiczny interfejs `IVertexVisitor`.
  * Interfejs ten powinien deklarować następujące metody:
    * `void PreVisit(int vertex);`
    * `void PostVisit(int vertex);`

* **2.2. `PathFinderVisitor` (3 punkty)**
  * Stwórz publiczną klasę `PathFinderVisitor` implementującą interfejs `IVertexVisitor`.
  * Powinna mieć publiczny konstruktor `PathFinderVisitor(int target)`.
  * Powinna mieć następujące właściwości:
    * `List<int> Path` - publiczna, tylko do odczytu - zawiera wierzchołki tworzące znalezioną ścieżkę
    * `bool Found` - publiczny odczyt, zapis tylko z poziomu klasy
  * Zaimplementuj metody `PreVisit` i `PostVisit`, aby znaleźć ścieżkę do wierzchołka `target`.
  * Przesłoń metodę `ToString()`, aby zwracała znalezioną ścieżkę w formacie `w1 --> w2 --> ...` lub napis "not found".
