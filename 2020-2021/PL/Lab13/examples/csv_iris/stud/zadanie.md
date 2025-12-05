# Laboratorium 10 - Pliki, serializacja

## Etap 0 (0 pkt)

1. Zbuduj projekt (Ctrl + Shift + B)
2. Otwórz katalog projektu (patrz visual.png)
3. Przejdź do folderu `bin/Debug` i wklej do niego pliki `iris.csv` oraz `__iris.xml`
4. Jeżeli utworzyłeś pusty projekt dodaj referencję do:
    - `System.Xml`
    - `Microsoft.CSharp`

## Etap 1 (2.0 pkt)

Stwórz plik `CsvFile.cs`, a w nim zaimplementuj dwie klasy:

- `CsvFileInfo`
- `CsvFile`

`CsvFileInfo` to publiczna klasa, która składa się z następujących właściwości __tylko do odczytu__:

- `FullDirectoryPath` (string) - przechowuje pełną ścieżkę do katalogu, w którym wczytywany plik się znajduje
- `FileName` (string) - przechowuje nazwę wczytanego pliku
- `Headers` (string[]) - przechowuje nazwy kolumn z wczytanego pliku csv
- `RowsNumbers` (int) - przechowuje liczbę wierszy z danymi, (pierwsza linia pliku to nagłówki kolumn)
- `Data` - słownik (Dictionary), który jako klucz bierze nazwę kolumny (string),
           a jako wartość przechowuje tablicę wartości z danej kolumny (string[])

`CsvFile` to publiczna statyczna klasa, która posiada metodę `Read`, która zwraca instancję klasy `CsvFileInfo`,
a przyjmuje jako argument ścieżkę do pliku.

Metoda `Read` powinna zwrócić instancję `CsvFileInfo` odpowiednio uzupełnioną danymi o wskazanym pliku.
W przypadku, jeżeli plik o podanej ścieżce nie istnieje należy zwrócić `null`.

- Do sprawdzenia czy dany plik istnieje nie można użyć `try`/`catch`.
- Do wczytania zawartości pliku należy wykorzystać klasę `StreamReader` odpowiednio używając go w bloku `using`.
- Do wczytania zawartości pliku nie można wykorzystać metody `File.ReadAllLines` (ani podobnych metod z klasy `File`),
  korzystamy tylko ze `StreamReadera`
- Wejściowy plik csv zawiera wartości tekstowe w cudzysłowiach, należy zadbać o to, aby wartości zwrócone za pomocą `Data`
  nie posiadały tych cudzysłowów (po prostu zwrócić wartość pomiędzy pierwszym a ostatnim cudzysłowiem), np.

    ```csv
    5.1,3.5,1.4,.2,"Setosa"
    ```
    dla `"Setosa"` chcemy otrzymać wartość `"Setosa"`, a nie `"\"Setosa\""`.

Do obu klas można też dodać inne składowe (np. konstruktor)


## Etap 2 (0.5 pkt)

Stwórz plik `Iris.cs`, a w nim stwórz:

- publiczne wyliczenie `IrisSpecies` zawierające wartości:
  - `Setosa`
  - `Virginica`
  - `Versicolor`
- publiczną klasę `Iris` składające się z następujących właściwości:
  - `SepalLength` (liczba)
  - `SepalWidth` (liczba)
  - `PetalLength` (liczba)
  - `PetalWidth` (liczba)
  - `Species` (`IrisSpecies`)

## Etap 3 (1 pkt)

Stwórz plik `DataFrame.cs`, a w nim utwórz klasy:

- `DataFrame<T>` - generyczna klasa, która posiada:
  - pole `Data` przechowujące tablicę obiektów o typie `T`
  - indeksator tylko do odczytu, który zwraca element powyższej tablicy
  - publiczny konstruktor przyjmujący `IEnumerable<T>`
  - klasa nie może posiadać publicznego bezparametrowego konstruktora

- `DataFrame` - statyczna klasa, która posiada statyczną generyczną metodę `FromCsv<T>`, która zwraca `DataFrame<T>`,
   a przyjmuje dwa argumenty:
  - `filepath` - ścieżka do pliku CSV
  - `mapper` - argument typu `Func<Dictionary<string, string>, T>`.  
    `mapper` pozwala użytkownikowi na podstawie słownika stworzyć instację klasy `T`.  
     Kluczami słownika są nazwy kolumn z pliku csv, a wartościami w słowniku są wartości z pliku csv
      (dla danej kolumny odpowiadająca jej wartość)

Do wczytania pliku CSV należy użyć klasy `CsvFile`.  

`mapper` dla zadanego poniżej fragmentu pliku csv

```csv
"sepal.length","sepal.width","petal.length","petal.width","variety"
5.1,3.5,1.4,.2,"Setosa"
```

powinien udostępnić użytkownikowi słownik na podstawie, którego użytkownik sam będzie w stanie utworzyć docelowy obiekt.

```json
{
    "sepal.length": "5.1",
    "sepal.width": "3.5",
    "petal.length": "1.4",
    "petal.length": ".2",
    "variety": "Setosa"
}
```

Przykładowy `mapper` jest już napisany w kodzie testującym, więc można spojrzeć i zobaczyć na jakiej zasadzie
powinno być możliwe zmapowanie obiektu z prostych wartości tekstowych.

---

Etap 4 i 5 można wykonywać w dowolnej kolejności.
Za wykonanie obu jest 1.5 pkt, a za wykonianie jednego (dowolnego) 1 pkt. 

## Etap 4

Etap polega na napisaniu serializacji `DataFrame<T>` do pliku binarnego. Do tego celu wykorzystaj `BinaryFormatter`

Do klasy `DataFrame<T>` dodaj i zaimplementuj metodę `ToBin`, która przyjmuje ścieżkę informującą gdzie zapisać plik.
Metoda powinna dokonać serializacji binarnej na obecnej instancji `DataFrame<T>`.
Jeżeli plik istnieje, to plik jest nadpisywany. Metoda niczego nie zwraca.

Do statycznej klasy `DataFrame` dodaj i zaimplementuj metodę `FromBin<T>`, która przyjmuje jako argument ścieżkę do pliku,
a zwraca instancję `DataFrame<T>`. Metoda ta dokonuje deserializacji binarnej na wskazanym pliku.

Trzeba będzie za pewne dodać odpowiednie rzeczy do istniejących klas.

## Etap 5

Etap polega na napisaniu serializacji `DataFrame<T>` do pliku Xml. Do tego celu wykorzystaj klasę `XmlSerializer`
z przestrzeni nazw `System.Xml.Serialization`.

Do klasy `DataFrame<T>` dodaj i zaimplementuj metodę `ToXml`, która przyjmuje ścieżkę do zapisania pliku.
Metoda powinna dokonać serializacji do pliku Xml na obecnej instancji `DataFrame<T>`.
Jeżeli plik istnieje, to plik jest nadpisywany. Metoda niczego nie zwraca.

Do statycznej klasy `DataFrame` dodaj i zaimplementuj metodę `FromXml<T>`, która przyjmuje jako argument ścieżkę do pliku Xml,
a zwraca instancję `DataFrame<T>`. Metoda ta dokonuje deserializacji na podstawie wskazanego pliku xml.

Trzeba będzie za pewne dodać odpowiednie rzeczy do istniejących klas.

Otwórz wcześniej wgrany plik `__iris.xml` (notepad, notepad++, vscode) spójrz na nazwy zastosowane w pliku.
Dostosuj odpowiednio klasę `Iris` wykorzystując attrybuty `XmlEnum` oraz `XmlElement` z przestrzeni nazw `System.Xml.Serialization`.
