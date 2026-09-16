# Interoperacyjność z Kodem Natywnym

**Suma punktów:** 8

## 1. Przegląd
W tym zadaniu zaimplementujesz wysokowydajną warstwę komunikacji pomiędzy natywnym silnikiem Ray Tracingu napisanym w C++ a zarządzaną aplikacją w C#. Otrzymujesz gotową logikę matematyczną i fizyczną dla algorytmu "Ray Tracing in One Weekend" (w tym progresywny renderer w pliku `camera.h`) oraz gotową bibliotekę okienkową opartą na Avalonia UI (`Windowing`).

Twoim celem jest zbudowanie biblioteki `RayTracing` (zawierającej zarówno natywny kod C++, jak i wiązania C#) oraz aplikacji konsolowej `RayTracingDemo`, która będzie sterować procesem renderowania i wizualizować wyniki w czasie rzeczywistym.

## 2. Wymagania

### Część 1: Warstwa Eksportu Natywnego (C++) [2 Punkty]
**Plik:** `RayTracing/native/export.cpp`

Musisz stworzyć ABI (Application Binary Interface) zgodne z językiem C, aby udostępnić dostarczone klasy i narzędzia C++.

1.  **Nieprzezroczyste Uchwyty (Opaque Handles):** Stwórz funkcje do instancjonowania obiektów (`CreateScene`, `CreateMaterial`, itp.) i zwracania wskaźników do nich. Nie ujawniaj układu klas C++ bezpośrednio w kodzie C#.
2.  **Zarządzanie Pamięcią:** Zaimplementuj odpowiadające funkcje `Destroy...` dla każdej funkcji tworzącej, aby poprawnie zwalniać pamięć.
3.  **Funkcja Renderująca:** Zaimplementuj funkcję `RenderScene`, która:
    *   Przyjmuje strukturę `CameraConfig` (przekazywaną przez wartość) zawierającą wszystkie parametry kamery.
    *   Tworzy lokalny obiekt `camera` i wypełnia go danymi konfiguracyjnymi.
    *   Wywołuje wbudowaną w kamerę metodę `render`, przekazując świat sceny, wskaźnik do bufora wyjściowego oraz funkcję zwrotną (callback).
4.  **Specyfikacja Callbacku:**
    *   Funkcja renderująca musi przyjmować wskaźnik na funkcję o następującej sygnaturze:
        `typedef void (*RenderCallback)(int samples, uint8_t* buffer);`
    *   Przekaż ten callback dalej do metody renderującej kamery.
5.  **Zapis Obrazu:** Udostępnij funkcję `SavePng`, która wykorzystuje załączoną bibliotekę `stb_image_write` do zapisania bufora RGBA do pliku.

### Część 2: Niskopoziomowe Wiązania (C#) [2 Punkty]
**Plik:** `RayTracing/NativeMethods.cs`

Wykonaj wiązanie wyeksportowanych funkcji C do .NET.

1.  **P/Invoke:** Zalecane jest użycie generatora kodu `[LibraryImport]`.
2.  **SafeHandles:** Użyj `SafeHandle` dla wszystkich zasobów natywnych wymagających zwolnienia.
    *   Zaimplementuj klasy `SceneSafeHandle` oraz `MaterialSafeHandle`.
    *   Środowisko uruchomieniowe musi automatycznie wywoływać funkcje `Destroy` z C++ poprzez te uchwyty.
3.  **Delegaty:** Zdefiniuj delegat `RenderCallback` dokładnie pasujący do sygnatury w C++.
4.  **Marshaling Ciągów Znaków:** Zapewnij, że wiązanie funkcji `SavePng` poprawnie obsługuje marshaling ścieżki pliku.

### Część 3: Wysokopoziomowe Bezpieczne API (C#) [2 Punkty]
**Pliki:** `RayTracing/*.cs`

Stwórz idiomatyczny, obiektowy wrapper w C#, który zapewnia bezpieczeństwo typów i pamięci.

1.  **Bezpieczeństwo:** Publiczne API nigdy nie może eksponować typów `IntPtr`/`nint` ani wskaźników użytkownikowi końcowemu.
2.  **Własność Zasobów:** Zaimplementuj wzorzec `IDisposable`, aby zarządzać czasem życia natywnych uchwytów sceny.

### Część 4: Demo i Integracja [2 Punkty]
**Plik:** `RayTracingDemo/Program.cs`

Zaimplementuj główną aplikację demonstracyjną, wykorzystując swoją bibliotekę oraz dostarczoną bibliotekę `Windowing`.

1.  **Konfiguracja Sceny:** Wygeneruj proceduralnie kultową scenę z **okładki pierwszej książki** (*Ray Tracing in One Weekend*).
2.  **Integracja Wizualna:** Użyj `Windowing.Viewer.Show`, aby uruchomić okno i rozpocząć pętlę renderowania.
3.  **Podgląd na Żywo:**
    *   Aktualizuj tekst statusu okna postępem renderowania (liczba próbek/czas).
    *   Aktualizuj obraz w oknie w czasie rzeczywistym, używając danych dostarczonych w callbacku.
4.  **Wynik Końcowy:** Po zakończeniu renderowania użyj metody `SavePng`, aby zapisać finalny obraz jako `output.png`.

## 3. Budowanie

### Budowanie

Projekt wykorzystuje system CMake zintegrowany z MSBuild. Zbudowanie rozwiązania C# automatycznie uruchomi budowanie kodu C++.

```bash
dotnet build -c Release
dotnet run -c Release --project RayTracingDemo/RayTracingDemo.csproj
```
