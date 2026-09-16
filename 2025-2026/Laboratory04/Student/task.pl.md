# Lab04 - Assembly, Refleksja

## Wprowadzenie

Twoim zadaniem jest zaimplementowanie biblioteki `SharpArgs` - biblioteki do parsowania argumentów z wiersza poleceń z wykorzystaniem atrybutów oraz refleksji.

Sposób użytkowania biblioteki jest następujący:

- Użytkownik tworzy klasę dziedziczącą po abstrakcyjnej klasie `SharpOptions`.
- Oznacza wybrane właściwości pochodzącymi z biblioteki atrybutami: `Flag` oraz `Option` (patrz **Etap 1**).
- Klasa `SharpParser` (patrz **Etap 4**) jest odpowiedzialna za przetworzenie tablicy argumentów `string[] args` oraz zwrócenie instancji modelu wraz z uzupełnionymi właściwościami.

## Punktacja

- **Etap 1**: `1` pkt za każdy atrybut.
- **Etap 2**: `2` pkt za każdą zaimplementowaną metodę.
- **Etap 3**: `2.5` pkt za implementację skanowania `assembly`, `0.5` pkt za prawidłowe wywołanie w metodzie `Main`.
- **Etap 4**: `3` pkt za prawidłowe parsowanie, przypisywanie wartości do modelu i tworzenie błędów.

## Wskazówki

- [Microsoft Learn: Attributes](https://learn.microsoft.com/en-us/dotnet/csharp/advanced-topics/reflection-and-attributes/)
- [Microsoft Learn: Retrieving Information Stored in Attributes](https://learn.microsoft.com/en-us/dotnet/standard/attributes/retrieving-information-stored-in-attributes)
- [Microsoft Learn: Retrieving Attributes from Class Members](https://learn.microsoft.com/en-us/dotnet/standard/attributes/retrieving-information-stored-in-attributes#retrieving-attributes-from-class-members)
- [Microsoft Learn: Assembly Class](https://learn.microsoft.com/en-us/dotnet/api/system.reflection.assembly?view=net-9.0)
- [Microsoft Learn: AggregateException Class](https://learn.microsoft.com/en-us/dotnet/api/system.aggregateexception?view=net-9.0)

## Etap 1 (bez testów jednostkowych, wystarczy pokazać prowadzącemu)

Zaprojektuj następujące atrybuty:

1. `[Flag]` dla wartości logicznych (flag, np. `--verbose`). Posiada parametry pozycyjne: `string Id`, `char Short` oraz opcjonalne: `string? Long`, `string? Help`.

2. `[Option]` dla argumentów nazwanych. Posiada parametry pozycyjne: `string Id`, `char Short` oraz opcjonalne: `string? Long`, `string? Default`, `bool Required`, `string? Help`.

### Opis właściwości

- `Id` – unikalny w ramach klasy identyfikator atrybutu.
- `Short` – krótka forma flagi (jednoznakowa, np. `v` odpowiada `-v`).
- `Long` – długa forma flagi (np. `verbose` odpowiada `--verbose`).
- `Help` – tekst pomocy/opisu argumentu.
- `Default` – wartość domyślna, używana jeśli argument nie został podany.
- `Required` – wymagalność argumentu, domyślnie `true`.

## Etap 2 (testy jednostkowe, plik `SharpOptionsTests.cs`)

Zaimplementuj następujące metody w dostarczonej abstrakcyjnej klasie bazowej `SharpOptions` (znajduje się w pliku `SharpOptions.cs`). Jeżeli w opisie metody mowa o zgłaszaniu wyjątków, to są to już zaimplementowane wyjątki, które można znaleźć w folderze `Exceptions/`. Metody walidują poprawność użycia atrybutów z pierwszego etapu dla typu dziedziczącego po `SharpOptions`:

- `ValidateFlags`:

  - Metoda powinna sprawdzać, czy każda właściwość oznaczona atrybutem `[Flag]` ma typ `bool` oraz czy krótkie formy flag nie zawierają duplikatów.
  - W przeciwnym przypadku zgłasza wyjątki `InvalidTypeException` oraz `DuplicateValuesException<string>`.

- `ValidateOptions`:
  - Metoda powinna sprawdzać, czy typ każdej właściwości oznaczonej atrybutem `[Option]` implementuje interfejs `IParsable<>` z przestrzeni nazw `System` lub jest równy typowi `string`.
  - W przeciwnym przypadku zgłasza wyjątek `InvalidTypeException`.

W celu znajdowania duplikatów w generycznej kolekcji `IEnumerable<T>` skorzystaj z gotowej metody `FindDuplicates<T>` w pliku `EnumerableExtensions.cs`.

## Etap 3 (testowanie poprzez wywołanie w pliku `Program.cs`)

Celem tego etapu jest umożliwienie automatycznego wykrywania wszystkich klas w danym `assembly`, które dziedziczą po `SharpOptions`, oraz walidacji ich konfiguracji. Wszystkie błędy walidacji mają być zbierane w jednym wyjątku `AggregateException`.

Walidacja powinna być realizowana za pomocą statycznej klasy `SharpOptionsAssemblyValidator` (plik `SharpOptionsAssemblyValidator.cs`):

- Walidator powinien przeszukać `assembly` i znaleźć wszystkie typy dziedziczące po `SharpOptions`.
- Dla każdego typu powinna zostać wywołana metoda `ValidateModel()` w celu sprawdzenia konfiguracji flag i opcji.
- Wszystkie wyjątki walidacyjne (pochodne `SharpArgsException`) należy zebrać i zgłosić w postaci pojedynczego `AggregateException`.

Użyj zaimplementowanego walidatora dla bieżącego `assembly` w metodzie `Main` w klasie `Program` (plik `Program.cs`).

## Etap 4 (testy jednostkowe, plik `SharpParserTests.cs`)

Celem tego etapu jest rozpoznanie w modelu wszystkich właściwości oznaczonych atrybutem `[Flag]`, a następnie sparsowanie oryginalnej tablicy argumentów `string[] args` zgodnie z tym modelem.

- Logika związana z parsowaniem powinna się znaleźć w klasie `SharpParser` (plik `SharpParser.cs`).
- Krótkie formy flag rozpoczynają się od pojedynczego myślnika (`-`), a długie formy od dwóch (`--`).
- Metoda powinna tworzyć instancję modelu (przy pomocy `Activator.CreateInstance`) i ustawiać jego flagi zgodnie z podaną tablicą argumentów.
- Metoda powinna zwracać obiekt klasy `ParseResult<T>` (klasa jest już gotowa w pliku `ParseResult.cs`), zawierający uzupełniony model lub kolekcję błędów parsowania.
- Pojawienie się w tablicy `string[] args` nieznanej flagi (lub elementu niezaczynającego się od `-` lub `--`) powoduje dodanie do kolekcji błędów wpisu: `"Unknown option: {arg}."`.
