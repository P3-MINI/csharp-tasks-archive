## Lab_11 - Unit Tests

### Tworzenie Projektu w Visual Studio:
- Stwórz projekt typu *Class Library* o nazwie *Lab11*.
- Zwracamy szczególną uwagę na typ projektu.
- Krótko omawiamy pozostałe dostępne typy projektów.

### Omawiamy krótko plik Array.cs:
- Włacz plik *Arrays.cs* do powyższego projektu.
- Krótko opowiedź o zawartości pliku.
- Kliknij PPM na nazwie klasy *ArrayExtension* i stwórz projekt testowy (omów okienko i zmień nazwę projektu na Lab11Tests).
- Wklej zawartość pliku *ArrayTests.cs* i omów widoczne metody testowe.
    - Omów inne dostępne atrybuty (patrz *Unit tests attributes* w UnitTests.pdf).
    - Omów *DynamicData*, który w lepszy sposób definiuje przypadki testowe.
  
### Omawiamy plik Bank_1.cs:
- Włącz plik *Bank_1.cs* do powyższego projektu.
- Omów krótko zawartość pliku (na tym etapie tylko klasa *BankAccount*).
- Kliknij PPM na nazwę klasy *BankAccount* i stwórz nowy zestaw testów.
- Kopiujemy zawartość *BankTests* i omawiamy zasadę Arrange/Act/Assert.
    - Patrz *Designing single test* w UnitTests.pdf.
    - Wspominamy jeszcze raz atrybuty testów.
    - Omawiamy dostępne możliwości w klasie Assert (mówimy o innych dostępnych bibliotekach typu *FluentAssertion*).
    - Wracamy na sekundę do *ArrayTests.cs* i pokazujemy, że istnieje wersja Assert dla kolekcji.
- Pokazujemy jak uruchomić testy (Test -> Run All Tests).
- Omawiamy okienko testów (możliwość debugowania testów, hierarchia testów, uruchamianie pojedynczych testów, czy testów które ostatnio nie przeszły, pokazujemy logowanie dla testów, które nie przeszły, playlisty, wiele testów pochodzących z przypadków testowych w *Array*).
- Analizujemy dlaczego test nie przeszedł. Wchodzimy do metody Debit i znajdujemy błąd, naprawiamy i uruchamiamy testy ponownie, działa.

### Omawiamy plik Bank_2.cs:
- Kopiujemy zawartość pliku *Bank_2.cs* i *BankTests_2.cs* do istniejących plików w projekcie.
- Pokazujemy im NuGet i do projektu testowego dodajemy NSubstitute oraz FluentAssertions (będziemy z tego korzystać).
- Omawiamy zmiany w pliku (w szczególności interfejsy IMoneyExchange i IAccountHistory, i mówimy dlaczego tak, czyli mockowanie danych).
- Zaczynamy implementować ze studentami zmiany w pliku testowym banku.
    - Dodajemy prywatne pola w klasie oraz metodę *Initialize*, zaznaczamy *TestInitializeAttribute*.
    - Dokładnie omawiamy czym jest mockowanie i do czego w ogóle służy.
    - Piszemy ze studentami dwa/trzy testy, żeby jeszcze raz pokazać im na czym to polega.
    - Dajemy im czas na własną implementację pozostałych testów.
