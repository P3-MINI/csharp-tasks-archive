# Lab3: Events, Files, Filesystem, Streams
Celem tego laboratorium jest dokończenie implementacji aplikacji do czytania notatek. Na szczęście dostępny jest gotowy szkielet takiej właśnie aplikacji. Musisz jedynie zaimplementować części bezpośrednio współpracujące z systemem plików. **Etapy muszą być wykonane po kolei.**
## Etap 1: Narzędzia do systemu plików
Przygotuj następujące funkcje w `FileSystemUtils.cs`. Mają one przygotowane testy, dzięki którym możesz wstępnie zweryfikować poprawność rozwiązania przed oddaniem etapu.
### 1.1. PrepareDirectory – 2 pkt
Nasza aplikacja do czytania notatek będzie otwierać katalog i go przeglądać. Funkcja `string PrepareDirectory(string path)` ma przygotować katalog do pracy z aplikacją. Jeśli więc `path` wskazuje na katalog, nie trzeba nic robić — funkcja zwraca ten sam ciąg.

Jeśli `path` wskazuje na plik z rozszerzeniem `.zip`, jest to archiwum i należy je rozpakować do tymczasowego środowiska przeglądania. Utwórz w katalogu tymczasowym folder o takiej samej nazwie jak archiwum, ale bez rozszerzenia. Rozpakuj zawartość archiwum zip do tego folderu i zwróć ścieżkę do tego katalogu.

Przykład: wywołanie z `"library.zip"` (o ile plik istnieje) powinno zwrócić `/tmp/library/`, `C:\Users\...\AppData\Local\Temp\library\` lub inną ścieżkę wskazywaną przez system jako katalog tymczasowy.

W innym przypadku, funkcja powinna rzucić wyjątek `FileSystemException` z odpowiednim komunikatem.

### 1.2. CountFiles – 1 pkt
Użytkownik może chcieć wiedzieć, ile notatek zgromadził. Funkcja `int CountFiles(string path, string suffix)` rekurencyjnie zlicza wszystkie pliki w katalogu `path`, których nazwy kończą się na `suffix`.

## Etap 2: Obserwator katalogu
Główną siłą naszej aplikacji do czytania notatek jest responsywność. Musi reagować na zmiany w przeglądanym katalogu. Gdy plik lub katalog zostanie dodany, usunięty lub zmieni się jego nazwa, program musi odpowiednio zareagować, aby interfejs odpowiadał temu, co rzeczywiście znajduje się w systemie plików. Ten etap także ma przygotowane testy.
### 2.1. Implementacja klasy `DirectoryWatcher` – 4 pkt
Możesz pobrać całą zawartość obserwowanego katalogu tylko raz — przy inicjalizacji. Później należy dodawać, usuwać i zmieniać nazwy katalogów oraz plików systematycznie w reakcji na zmiany. Klasa udostępnia:

- konstruktor z jednym argumentem `string`, wskazującym katalog do obserwacji;
- zdarzenie `EventHandler? DirectoryChanged`, wywoływane za każdym razem, gdy plik lub katalog wewnątrz obserwowanego katalogu zostanie utworzony, usunięty lub zmieni nazwę;
- właściwość tylko do odczytu `string[] Files`, zwracającą nazwy plików bezpośrednio w obserwowanym katalogu w nowej tablicy;
- właściwość tylko do odczytu `string[] Directories`, zwracającą nazwy katalogów bezpośrednio w obserwowanym katalogu w nowej tablicy.

Klasa implementuje także interfejs `IDisposable` i powinna zwalniać wszystkie zasoby w funkcji `Dispose`.

*Wskazówka: użyj `FileSystemWatcher`. Należy uważać - w niektórych systemach operacyjnych generuje on zdarzenia **przed** zakończeniem operacji. Oznacza to, że dla właśnie utworzonego katalogu `src`, `Directory.Exists("path_to/src")` może zwrócić `false`. Użyj `NotifyFilter`, aby obserwować wyłącznie pliki lub katalogi.*

### 2.2. WatchDirectory – 1 pkt
W `FileSystemUtils.cs` utwórz `DirectoryWatcher` dla katalogu `path`. Następnie zasubskrybuj `DirectoryChanged` funkcją, która wypisze katalogi w formacie "d:<nazwa_katalogu>", a potem pliki w formacie "f:<nazwa_pliku>". Umożliwia Ci to to śledzenie zmian i testowanie ich aż do momentu wciśnięcia enter. Zachowaj kod tworzący `Log` i dbający o jego zamknięcie. Możesz używać `Log` do debugowania. Upewnij się, że wszystkie zasoby będące `IDisposable` są prawidłowo zwalniane.

## Etap 3: Note Reader
Czas połączyć funkcjonalność z zewnętrznym API. Implementacja istniejącego interfejsu pozwoli nam dostarczyć funkcjonalność klasie `NoteReader` korzystającej z `ConsolePainter`, bez konieczności bezpośredniego kontaktu z którymkolwiek z nich. Twoim zadaniem jest implementacja dwóch źródeł danych. Po tym będziesz w stanie czytać notatki za pomocą interaktywnej aplikacji konsolowej. Aby uruchomić aplikację, odkomentuj `#define` w `Program.cs` oraz `NoteReader.cs`. Sterowanie: strzałki w górę i dół, enter oraz escape.

### 3.1 Źródła i przetwarzanie zdarzeń wejścia – 1 pkt
Zaimplementuj poprawne interfejsy i właściwości dla `DirectorySource` oraz `FileSource`, tak aby `NoteReader` się kompilował. Nie muszą jeszcze działać poprawnie.

Na końcu konstruktora `NoteReader` zasubskrybuj `inputEventGenerator.Input` funkcją lambda, tak aby jego zdarzenia - naciskane klawisze - były przetwarzane przez `HandleInput`. Nie zmieniaj funkcji `HandleInput`.

### 3.2. DirectorySource – 2 pkt
Umożliwia przeglądanie i wybieranie katalogów oraz plików w podanym katalogu. Implementuje interfejsy `IDataSource<string>` i `IDisposable`. Powinno być zawsze aktualne względem stanu systemu plików.

- Właściwość `Name` to ścieżka do obserwowanego katalogu.
- `Data` to `IEnumerable<string>` zbudowane ze znaku wskazującego, czy to plik czy katalog i czy jest wybrany, oraz nazwy tego obiektu. Użyj znaczników podanych w komentarzu nad klasą. Wszystkie katalogi powinny mieć również liczbę plików z końcówką `pl.md` zawartych w nim i jego podkatalogach dopisaną po nazwie.
  - Przykład: wybrany katalog `Lab06`, zawierający `index.pl.md`, będzie reprezentowany jako `»Lab06 (1)`.
- Właściwość `Count` zwraca łączną liczbę elementów.
- `DataChanged` to zdarzenie wywoływane przy każdej zmianie danych — zarówno zmianie plików lub katalogów jak i zmianie wyboru.
- `Dispose` zwalnia wszystkie zasoby wymagające zwolnienia.

Klasa udostępnia także funkcjonalność zarządzania wyborem:
- właściwość `int Select` jest publicznie możliwa do odczytu, ale możliwa do ustawienia tylko prywatnie. Po zmianie `Data`, właściwość ta powinna wskazywać na poprawny element, choć niekoniecznie ten sam. Jeśli nie ma dostępnych elementów, powinna być równa -1.
- `Selected` zwraca nazwę wybranego pliku lub katalogu, bez znaczników.
- `SelectUp` i `SelectDown` odpowiednio zmniejszają i zwiększają `Select` o jeden, z zabezpieczeniem zakresu wyboru do poprawnych wartości.
### 3.3. FileSource – 1 pkt
To źródło reprezentuje plik tekstowy w systemie plików. Implementuje te same interfejsy co `DirectorySource`. Odczytuje linie z pliku i udostępnia je pod `Data`. Zawartość powinna zostać zastąpiona nową wtedy i tylko wtedy, gdy plik zostanie w jakikolwiek sposób zmodyfikowany (wykryte poprzez zmianę czasu ostatniego zapisu). Niektóre systemy operacyjne mogą wymagać busy waitingu ( :( ), aż `Guard.FileReadAvailable` potwierdzi, że plik jest dostępny do odczytu.
