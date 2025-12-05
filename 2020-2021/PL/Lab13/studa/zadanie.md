# Zadanie serializacja/deserializacja

Zadanie będzie polegało na opearacjach związanych z czytaniem i zapisywaniem plików.  
UWAGA: w do czytania i zapisu plików nie można korzystać ze statycznych metod z klasy `File`
```dotnetcli
File.WriteAllText
File.ReadAllText
```

Jako punkt startowy otrzymałeś eksport (testowych) danych w pliku `import.csv`  
Separatorem wartości w tym pliku jest `;`.  
Skopiuj go do folderu w którym znajduje się Twój projekt. 
Następnie w pliku projektu (plik `csproj`) wstaw tekst (plik będzie kopiowany do folderu wynikowego kompilacji)

```xml
  <ItemGroup>
    <Content Include="import.csv">
      <CopyToOutputDirectory>Always</CopyToOutputDirectory>
    </Content>
  </ItemGroup>
```

## --- Etap 1 --- (1.5 pkt)
W pliku `import.csv` znajduje się lista obiektów typu `Book`. 
Twoim zadaniem jest stworzenie klasy `Book` (oraz `PublicationDetails`) aby można było do nich zdeserializować dane z pliku import.
Klasa `Book` posiada właściwości `Id`, `Title`, `Author`, `PagesNumber`, `BookType` (jest typem wyliczeniowym z wartościami `Hardcover` oraz `Ebook`)
oraz właściwość `PublicationDetails`. `PublicationDetails` posiada właściwości `PublicationDate` (typu `DateTime`), `PublicationPlace` i `Publisher`

Napisz klasę `LibraryDb` która w konstruktorze otrzyma ścieżkę (`rootPath`).  
Jeśli ścieżka `rootPath` nie istnieje powinna zostać stworzona.  

Dodaj metodę `ImportFromCsv` do `LibraryDb` która przyjmuje jeden parametr `filePath` a zwraca sekwencję `IEnumerable<Book>`.
Jeśli `filePath` wskazuje na plik który nie istnieje metoda `ImportFromCsv` powinna zgłosić wyjątek `FileNotFoundException`.

Uwaga: w pliku `Program.cs` należy odkomentować klasę `PrintExtensions`

## --- Etap 2 --- (1 pkt)
Dodaj do klasy `LibraryDb` metodę `Add` która przyjmie obiekt typu `Book`. Wywołanie metody powinno powinno zserializować obiekt do formatu `xml` i zapisać go w pliku w katalogu `_rootPath`.  
Nazwa pliku powinna mieć format `Title.xml`
Wywołanie metody `Add` gdy plik o podanej nazwie już istnieje powinno zgłosić wyjątek `InvalidOperationException`

Dodaj również metodę `Get` która przyjmuje parametr: `Title`. Metoda wczytuje i deserializuje plik `Title.xml` i zwraca zdeserializowaną zawartość pliku `Book`
Próba wywołania metody `Get` dla nie istniejącego obiektu ma zwrócić `null`.

## --- Etap 3 --- (1.5 pkt)

Rozszerz `LibraryDb` o możliwość modyfikacji, usunięcia i pobrania wszystkich obiektów.

- metodę `AddOrUpdate` która przyjmuje obiekt `Book`, serializuje go i zapisuje (lub nadpisuje) jego aktualną wersję.  
- metodę `Delete` która przyjmuje `Title` i usuwa plik z zawartością jeśli istnieje.  
Wywołanie metody `Delete` gdy plik o podanej nazwie nie istnieje powinno zgłosić wyjątek `InvalidOperationException`
- metodę `List` która czyta wszystkie obiekty zapisane w bazie i zwraca sekwencję `IEnumerable<Book>`.

## --- Etap 4 ---  (1pkt)
Zmodyfikuj kod tak aby pole `BookType` było zserializowane jako atrybut.  
Zmodyfikuj również aby nazwy pól dla `Id` i `PagesNumber` serializowały się do `Identifier` i `PrintLength`

Dokumentacja:
- https://docs.microsoft.com/en-us/dotnet/standard/serialization/introducing-xml-serialization
- https://docs.microsoft.com/en-us/dotnet/api/system.xml.serialization.xmlelementattribute?view=net-5.0
- https://docs.microsoft.com/en-us/dotnet/api/system.xml.serialization.xmlattributeattribute?view=net-5.0