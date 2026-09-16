# Lab05 - Programowanie Asynchroniczne i Równoległe

## Symulacja _[Gry w życie](https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life)_ – Responsywne UI i Obliczenia Równoległe

Celem zadania jest uzupełnienie fragmentów logiki aplikacji desktopowej, która symuluje automat komórkowy _"Gry w życie"_.
Głównym założeniem naszej aplikacji nie jest sama grafika, lecz wydajność i responsywność aplikacji.

> Opis elementów, z których składa się aplikacja desktopowa.
> Wszystko, o czym tutaj mowa, jest już zaimplementowane.
> Twoja "część" zaczyna się w **Etap 1**.

Aplikację można podzielić na 3 części:

- Lewy panel: Ten obszar służy do sterowania danymi wejściowymi.
  - Przycisk `Choose directory...`: Otwiera systemowe okno wyboru folderu.
  - Pasek postępu: Pokazuje postęp asynchronicznego skanowania plików.
  - Lista plików: Wyświetla pliki znalezione w folderze. Wybór elementu z tej listy natychmiast przerywa obecną symulację i startuje nową dla wybranego pliku.
- Obszar symulacji: Główna część ekranu, gdzie renderowana jest gra
- Dolny pasek statusu: Ten obszar służy do monitorowania wydajności i sterowania przebiegiem symulacji w czasie rzeczywistym.
  - Status: Wyświetla komunikaty: `Choose directory...`, `Scanning directory...`, `Done.` oraz `Simulation: <filename>`.
  - Suwak Prędkości: Pozwala dynamicznie zmieniać opóźnienie pomiędzy generacjami bez restartowania symulacji.
- Statystyki:
  - `Generation`: Numer aktualnego kroku symulacji.
  - `Living cells`: Liczba żywych komórek (test poprawności algorytmu).
  - `Elapsed`: Czas obliczenia ostatniej generacji w milisekundach.

## Przydatne linki:

- [Microsoft Learn: Directory.EnumerateFiles Method](https://learn.microsoft.com/en-us/dotnet/api/system.io.directory.enumeratefiles?view=net-10.0)
- [Microsoft Learn: CancellationTokenSource.IsCancellationRequested Property](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtokensource.iscancellationrequested?view=net-10.0)
- [Microsoft Learn: File.ReadAllLinesAsync Method](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.readalllinesasync?view=net-9.0)
- [Microsoft Learn: Task cancellation](https://learn.microsoft.com/en-us/dotnet/standard/parallel-programming/task-cancellation)
- [Microsoft Learn: Parallel.For Method](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.parallel.for?view=net-10.0)
- [Microsoft Learn: Interlocked.Add](https://learn.microsoft.com/en-us/dotnet/api/system.threading.interlocked.add?view=net-9.0)
- [Microsoft Learn: Stopwatch Class](https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.stopwatch?view=net-10.0)
- [Microsoft Learn: Progress<T> Class](https://learn.microsoft.com/en-us/dotnet/api/system.progress-1?view=net-9.0)
- [Microsoft Learn: Task.Run Method](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.run?view=net-9.0)
- [Microsoft Learn: TaskCanceledException Class](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.taskcanceledexception?view=net-9.0)

## Etap 1: Asynchroniczne wczytanie przykładów

Aplikacja musi wczytywać pliki z planszami bez "zamrażania" interfejsu użytkownika.

Otwórz plik `Services/FileService.cs`.

1. Metoda `EnumerateFilesAsync`: (**2 pkt.**)
   - Zwróć ścieżki do plików tekstowych znajdujących się bezpośrednio w folderze `folderPath` w postaci `IAsyncEnumerable<string>`.
   - Użyj `Directory.EnumerateFiles`, aby pobrać listę plików `.txt`.
   - Obsłuż `CancellationToken` – jeśli zażądano anulowania, przerwij pętlę.
   - Przed zwróceniem każdej ścieżki dodaj opóźnienie `100` milisekund.
2. Metoda `LoadBoardAsync`: (**1 pkt.**)
   - Wczytaj asynchronicznie wszystkie linie w pliku `filePath`.
   - Zwróć wypełnioną tablicę dwywymiarową `bool[,]` o rozmiarze `rows × cols`.
   - Znak `0` w linijce zawartości pliku odpowiada żywej komórce (wartość `true` w tablicy). Znak nowej linii `\n` rozpoczyna nowy wiersz.
   - Każdy inny znak odpowiada martwej komórce (wartość `false w tablicy).
   - **Uwaga:** Plik może zawierać mniej lub więcej linijek/znaków w linijce niż wynosi `rows` lub `cols`. Należy wypełniać tablicę od lewego górnego rogu. Przyjmujemy, że cała reszta komórek jest martwa.

Przykładowa zawartość pliku wejściowego:

```txt
.....
...0.
.0.0.
..00.
.....
```

### Testowanie:

Kliknij przycisk `Choose directory...` i wybierz folder z przykładami.
Lista po lewej stronie okna powinna się wypełnić zawartością wybranego folderu.

## Etap 2: Silnik Gry i Równoległość

Twoim zadaniem jest obliczenie stanu planszy w kolejnej generacji.
Gra toczy się na dwuwymiarowej planszy składającej się z komórek, które mogą być żywe (`true`) lub martwe (`false`).

Otwórz plik `Models/LifeEngine.cs`.

1. Metoda `CalculateNextGeneration`: (**3 pkt.**)
   - Zwróć obiekt `SimulationStepResult`, który zawiera informację o całkowitej liczbie żywych komórek oraz czasie, w którym nowy stan został obliczony.
   - Stan komórki w następnej turze zależy od liczby jej żywych sąsiadów, którą zwraca gotowa funkcja `int CountLiveNeighbors(int y, int x)`.
   - Zastosuj zasady gry:
     - Przeżycie: Żywa komórka z 2 lub 3 sąsiadami żyje dalej.
     - Narodziny: Martwa komórka z dokładnie 3 sąsiadami ożywa.
     - Śmierć: W każdym innym przypadku komórka staje się/pozostaje martwa.
   - Użyj `Parallel.For`, aby obliczać wiersze planszy równolegle.
   - **Podpowiedź:** Pamiętaj, że w trakcie obliczeń nie możesz modyfikować tablicy `Grid` (stan obecny), z której czytasz dane.
   - **Podpowiedź:** Do sumowania całkowitej liczby żywych komórek w pętli równoległej użyj `Interlocked.Add`.
   - **Podpowiedź:** Wykorzystaj pętlę `Parallel.For` oraz `ParallelOptions` w celu przekazania parametru `token` do pętli.

## Etap 3: Połączenie UI z logiką

To najważniejszy etap, w którym połączysz UI z logiką, dbając o to, by aplikacja nie "wisiała" i reagowała na zmiany.

Otwórz plik `ViewModels/MainWindowViewModel.cs`. Znajduje się w nim część definicji klasy (`partial class`), którą będziesz rozwijać.
Pozostała część (mocno związana z wykorzystanym frameworkiem graficznym Avalonia) znajduje się w `ViewModels/UI/MainWindowViewModel.UI.cs` (jej nie musisz edytować).

Wszystkie zmienne zapisane `specjalną czcionką` są albo parametrami funkcji albo istnieją w pliku `ViewModels/UI/MainWindowViewModel.UI.cs` i nie musisz ich deklarować.

1. Metoda `SimulationLoop`: (**2 pkt.**)
   - W pętli oblicza nowy stan gry.
   - W każdej iteracji:
     - Raportuje progres przy pomocy argumentu `progress`. Obiekt typu `SimulationFrame` powinien zawierać sklonowaną (`.Clone()`) wewnętrzną tablicę silnika `_engine`.
     - Opóźnia swoje wykonanie o `SimulationDelay` milisekund.
   - Kończy działanie w zależności od stanu parametru `token`.
2. Metoda `StartSimulationFromFileAsync`: (**4 pkt.**)
   - Przerywa wykonanie poprzedniej symulacji (użyj `_simulationCancellationTokenSource`).
   - Jeżeli poprzednia symulacja była w toku, to `await`-uje `_currentSimulationTask`.
   - Ładuje zawartość planszy z pliku `filePath` przy pomocy `_fileService`. Jej rozmiar to `BoardSize × BoardSize`.
   - Ustawia stan silnika gry `_engine` (przy pomocy metody `LoadState` w klasie `LifeEngine`) oraz początkowe wartości dla paska statusu (statystyki: `Generation`, `StatusText`).
   - Uruchamia metodę `SimulationLoop` bez oczekiwania (bez `await`) przy pomocy `Task.Run`. Obiekt typu `Task` zwrócony przez metodę należy przypisać do `_currentSimulationTask`.
   - Definiuje logikę raportowania progresu (tworzy obiekt typu `Progress<SimulationFrame>`).
   - Progres aktualizuje:
     - Liczbę żywych komórek `LiveCells`.
     - Ostatni czas obliczeń `LastCalculationTimeMs`.
     - Generację `Generation`
     - Odświeża planszę poprzez przypisanie nowego stanu planszy do zmiennej `CurrentGrid`.

### Testowanie całości:

1. Kliknij `Choose directory...` – pliki powinny pojawiać się na liście pojedynczo (dzięki `Task.Delay` w serwisie).
2. Kliknij plik na liście – symulacja powinna ruszyć.
3. Zmień suwak prędkości – symulacja powinna przyspieszyć/zwolnić natychmiast.
