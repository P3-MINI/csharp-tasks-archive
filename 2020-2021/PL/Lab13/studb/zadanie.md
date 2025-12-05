# Zadanie serializacja/deserializacja

Zadanie będzie polegało na implementacji funkcjonalności związanych z serializacja i deserializacją obiektów z plików.  
UWAGA: do czytania i zapisu plików nie można korzystać ze statycznych metod z klasy `File`
```dotnetcli
File.WriteAllText
File.ReadAllText
```

Jako punkt startowy otrzymałeś eksport (testowych) danych w pliku `import.csv`
Separatorem wartości w tym pliku jest `;`.  
Skopiuj go do folderu w którym znajduje się Twój projekt. 
Następnie w pliku projektu (plik `csproj`) wstaw tekst poniżej (dzięki temu plik `import.csv` będzie kopiowany do folderu wynikowego kompilacji)

```xml
  <ItemGroup>
    <Content Include="import.csv">
      <CopyToOutputDirectory>Always</CopyToOutputDirectory>
    </Content>
  </ItemGroup>
```

## --- Etap 1 --- (1.5 pkt)

W pliku `import.csv` znajduje się lista obiektów typu `Student`. 
Twoim zadaniem jest stworzenie klasy `Student` (oraz `Address`) aby można było do nich zdeserializować dane z pliku importu.  
Klasa `Student` posiada właściwości `Id`, `FirstName`, `LastName`, `BirthDate` (typu DateTime), `StudentType` (jest typem wyliczeniowym z wartościami `FullTime` oraz `External`)
oraz właściwość `Address`. Klasa `Address` posiada właściwości `City`, `StreetName` i `StreetNo`

Napisz klasę `StudentDb` która w konstruktorze otrzymuje ścieżkę (`rootPath`).  
Jeśli ścieżka `rootPath` nie istnieje powinna zostać stworzona.  

Dodaj metodę `ImportFromCsv` do `StudentDb` która przyjmuje jeden parametr `filePath` a zwraca sekwencję `IEnumerable<Student>`.
Jeśli `filePath` wskazuje na plik który nie istnieje metoda `ImportFromCsv` powinna zgłosić wyjątek `FileNotFoundException`

Uwaga: w pliku `Program.cs` należy odkomentować klasę `PrintExtensions`

## --- Etap 2 --- (1 pkt)

Dodaj do klasy `StudentDb` metodę `Add` która przyjmie obiekt typu `Student`. Wywołanie metody powinno powinno zserializować obiekt do formatu `json` i zapisać go w pliku w katalogu `rootPath`.  
Nazwa pliku powinna mieć format `FirstName-LastName.json`
Wywołanie metody `Add` gdy plik o podanej nazwie już istnieje powinno zgłosić wyjątek `InvalidOperationException`

Dodaj również metodę `Get` która przyjmuje dwa parametry: `FirstName` i `LastName`. Metoda wczytuje i deserializuje plik `FirstName-LastName.json` i zwraca zawartość typu `Student`.  
Próba wywołania metody `Get` dla nie istniejącego obiektu ma zwrócić `null`.

## --- Etap 3 --- (1.5 pkt)

Rozszerz `StudentDb` o możliwość modyfikacji, usunięcia i pobrania wszystkich obiektów.

- metodę `AddOrUpdate` która przyjmuje obiekt `Student`, serializuje go i zapisuje (lub nadpisuje) jego aktualną wersję.  
- metodę `Delete` która przyjmuje `FirstName` i `LastName` i usuwa plik z zawartością jeśli istnieje.  
Wywołanie metody `Delete` gdy plik o podanej nazwie nie istnieje powinno zgłosić wyjątek `InvalidOperationException`
- metodę `List` która czyta wszystkie obiekty zapisane w bazie i zwraca sekwencję `IEnumerable<Student>`.

## --- Etap 4 ---  (1pkt)

Zmodyfikuj kod tak aby serializacja pola `StudentType` powodowała zapis do pliku wartości: `FullTime` oraz `External` zamiast `0`, `1`.  
Zmodyfikuj również aby nazwy pól dla `FirstName` i `LastName` serializowały się do `First name` i `Last name`

Dokumentacja:
- https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-overview
- https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-how-to?pivots=dotnet-5-0
- https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-customize-properties
- https://docs.microsoft.com/en-us/dotnet/api/system.text.json.serialization.jsonconverterattribute?view=net-5.0
- https://docs.microsoft.com/en-us/dotnet/api/system.text.json.serialization.jsonstringenumconverter?view=net-5.0
- https://docs.microsoft.com/en-us/dotnet/api/system.text.json.serialization.jsonpropertynameattribute?view=net-5.0