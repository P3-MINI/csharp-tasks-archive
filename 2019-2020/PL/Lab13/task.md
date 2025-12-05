# Laboratorium 13 - Programowanie współbieżne, asynchroniczne

Uwaga:

- w pliku Program.cs wolno jedynie odkomentować/zakomentować odpowiednie dyrektywy #define 
- w pliku SimpleServer.cs nie wolno nic zmieniać
- plik SimpleClient.cs należy samodzielnie stworzyć (cześć 2 zadania)
- w pliku PasswordRecoveryTool.cs należy dopisać rozwiazanie części 1 zadania.
- testy są czasochłonne, w przypadku rozwiązania jednej z części zadania, można wyłączyć testy dotyczącej drugiej części
- w pliku output.txt jest przykładowy wynik działania programu, działanie programu nie jest deterministyczne,
  więc kolejność wydruków w poszczególnych etapach musi się różnić, chodzi bardziej o przedstawioną koncepcję działania programu

Zadanie poświęcone jest `Task Parallel Library` (TPL) i składa się z dwóch niezależnych części.
Pierwsza część (etapy 1 i 2) dotyczy programowania współbieżnego przy wykorzystaniu klasy `Parallel`.
Druga część (pozostałe etapy) dotyczy programowania asynchronicznego.

## Etap 1 - Wstęp

Celem tego etapu jest napisanie narzędzia do odzyskiwania hasła na podstawie zadanego hasha hasła.  
Czym jest hash? Hash jest jednokierunkową funkcją, czyli funkcją, dla której łatwo jest wyliczyć wynik na podstawie zadanego argumentu,
natomiast trudne jest znalezienie funkcji odwrotnej (funkcji, która na podstawie wyniku zwracałaby argument wejściowy).  

Przykładowo w bazach danych przechowywane są login oraz hash hasła. Użytkownik podczas logowania podaje swój login oraz hasło.
Na podstawie loginu znajdowany jest wpis w bazie, jeżeli użytkownik istnieje w bazie to wyliczany jest hash z podanego przez użytkownika hasła
oraz porównywany z istniejącym hashem w bazie. Jeżeli są różne, to nie możemy uznać podanych danych za poprawne.  
W przypadku wycieku danych z takiej bazy danych, atakujący posiada tylko hash oraz login.
Jak trudne jest odzyskanie hasła z hasha? Zobaczymy to rozwiązując pierwszy etap, ale głównie zależy od długości hasła oraz znaków,
które użytkownik wykorzystał przy tworzeniu hasła. W przypadku łatwego hasła oraz loginu, którym może być adres email,
atakujący jest w stanie dostać sie do innych serwisów, jeżeli użytkownik wykorzystuje te same hasło pomiędzy różnymi serwisami.
Dlatego warto stosować różne hasła do różnych serwisów korzystać z menadżerów haseł czy wielostopniowej autoryzacji (np. hasło + SMS).  
Ciekawostka: Z tego też powodu serwisy w przypadku opcji 'Zapomniałem hasła' oferują reset hasła zamiast przypomnienia hasła
w postaci wysłania obecnego hasła SMSem czy mailem (serwis nie przechowuje naszego hasła w jawnej postaci).

W przypadku pierwszego etapu w klasie `PasswordRecoveryTool` dostępna jest wersja, która szuka hasła w sposób sekwencyjny,  
to jest: dla zadanych długości hasła oraz alfabetu (zestawu znaków) tworzy wszystkie możliwe słowa (potencjalne hasła)  
i dla każdego potencjonalnego hasła wylicza hash. Jeżeli wyliczony hash jest szukanym hashem to znaleźliśmy zaginione hasło.  
Jeżeli dla żadnego słowa nie znaleźliśmy pasującego hasła to znaczy, że się nie udało. 
Niestety ta wersja jest sekwencyjna i nie działa zbyt szybko, a można byłoby ją przyspieszyć sprawdzając hasła równolegle.
W tym celu...

## Etap 1 - (1.5 pkt)

Stwórz publiczną statyczną metodę `RecoverPasswordParallel` (plik PasswordRecoveryTool.cs), która przyjmuje te same
argumenty co metoda `RecoverPasswordSequential`.
Metoda ta powinna znaleźć pasujące hasło do podanego hasha oraz szukać haseł w sposób równoległy przy wykorzystaniu klasy `Parallel`.
Do generowania potencjalnych haseł możesz skorzystać z metody `GenerateWords`.
Wszystkie testy do łatwych oraz średnich haseł powinny przechodzić bez żadnych zastrzeżeń.

Zwróć uwagę, że instancja `HashAlgorithm` nie jest thread-safe, czyli należy zadbać o to, aby każdy pracujący wątek
posiadał swoją własną instancję. Wyjątek jaki może wystąpić podczas współdzielenia instancji pomiędzy wątkami to:
`System.Security.Cryptography.CryptographicException: 'Hash not valid for use in specified state.'`

## Etap 2 - (0.5 pkt)

Zestaw testujący składa się z trudniejszych haseł do złamania.
Metoda zrównoleglona powinna pokonywać limity czasowe. Metoda sekwencyjna nie powinna przechodzić testu haseł bardzo trudnych. 

Wskazówka:
Co można zrobić, aby ograniczyć potrzebę synchronizacji obliczeń?
Czyli nie chcemy zrownoleglać iterowanie po bardzo dużej liczbie możliwych haseł.
Chcemy zrównoleglić pracę tak, aby ciało równoległego for/foreach wykonywało się stosunkowo długo,
a jednocześnie zadań było wystarczająco dużo, aby móc rozdzielić je na osobne wątki.

## Etap 3 - Wstęp

Celem tego etapu jest zobaczenie jak programowanie asynchroniczne pozwala na zwiększenie wydajności (głownie skalowalności) programów,
które często muszą czekać na dane, np. operacje sieciowe, operacje na dysku.
W tym celu symulować będziemy komunikację sieciową pomiędzy serwerem a klientem.
Serwer został już napisany, do napisania został tylko klient.

Serwer jest prototypem serwera obliczeniowego, czyli posiada jedną asynchroniczną metodę `Get`,
która przyjmuje żądanie zadania obliczeniowego. Serwer nie jest za bardzo wydajny i potrzebuje trochę czasu na obliczenia,
dodatkowo, jeżeli własność serwera `MaxConcurrentRequests` jest większa niż `0` to serwer nie jest w stanie obsłużyć
większej liczby równoległych żądań niż `MaxConcurrentRequests`.
W przypadku przekroczenia wskazanej wartości serwer zgłosi wyjątek, nie należy go łapać! Obsługa wyjątków (konkretnego typu)
będzie dopiero potrzebna w etapie piątym.

Komunikacja między klientem, a serwerem jest asynchroniczna i polega na protokole:
Klient -> żądanie -> Server -> odpowiedź -> Klient
Czyli klient inicjuje żądanie, po pewnym czasie serwer zwraca odpowiedź.

## Etap 3 - (1.5 pkt)

Napisać klasę `SimpleClient` w pliku `SimpleClient.cs`, która:

Posiada publiczny konstruktor, który przyjmuje serwer, z którym będzie się komunikować, czyli argument o typie `ISimpleServer`.
Należy zapisać serwer w polu klasy do wykorzystania na potem.

Posiada publiczną asynchroniczną metodę `SumResponsesAsync`, która przyjmuje kolekcję zadań obliczeniowych(`IEnumerable<SimpleRequest>`),
a zwraca sumę wyników obliczeń. Obliczenia wykonuje serwer, czyli metoda powinna wysłać wszystkie zadania na serwer obliczeniowy
(wywołać metodę `Get` na podanym w konstruktorze serwerze), poczekać na odpowiedzi, a następuje je zsumować i zwrócić wyliczoną sumę.

Należy wziąć pod uwagę, że serwer jest w stanie przyjąć wiele żądań równolegle. W tym etapie nie ma limitu żądań jakie serwer może przyjąć.

Wskazówka:
Klasa `Task` posiada 4 metody postaci: `WhenAll`, `WhenAny`, `WaitAny`, `WaitAll`, które mogą ułatwić (umożliwić) rozwiązanie tego etapu.
Preferowaną metodą synchronizacji jest metoda z wykorzystaniem słowa kluczowego `await`. Metoda z wykorzystaniem właściwości `Result`
też niestety zadziała, ale różnice pomiędzy tymi metodami synchronizacji są subtelne i wykraczają poza wykłady.

Uwaga: Interfejs `ISimpleServer` oraz pomocnicze klasy `SimpleServer`, `SimpleRequest`, `SimpleResponse` i `SimpleException`
są zdefiniowane w pliku `SimpleServer.cs` (nie wolno nic w nim zmieniać!).

## Etap 4 - (1 pkt)

W tym etapie należy wziąć pod uwagę, jak dużo serwer jest w stanie obsłużyć żądań równolegle,
czyli chcemy napisać mechanizm kolejkowania żądań, który zabezpieczy serwer przez DoS'em. (Denial of Service).
W tym celu należy wziąć pod uwagę właściwość serwera `MaxConcurrentRequests`, aby w danym czasie nie było więcej aktywnych żądan
niż podana wartość. Jeżeli wartość jest mniejsza lub równa 0, to znaczy, że serwer nie posiada limitu na liczbę równoległych żądań.

Wskazówka:
Do implementacji kolekcji aktywnych zadań można wykorzystać listę `Task`ów oraz umiejętnie sprawdzać,
które zadanie się już zakończyło, w takim wypadku można dodać kolejne (o ile jest) zadanie do kolekcji aktywnych zadań.

## Etap 5 - (0.5 pkt)

Jako, że serwer jest wersją protypową to nie każde obliczenie wykonuje się poprawnie.
W tym etapie chcemy dodać mechanizm, który uchroni nas przed jeszcze nie obsługiwanymi obliczeniami po stronie serwera.
W przypadku niewspieranego zadania obliczeniowego serwer zgłosi wyjątek typu `SimpleException`, który należy odebrać po stronie klienta.
W przypadku zadania, które zgłosiło dany wyjątek należy po prostu pominąć dane zadanie, czyli w przypadku takiego zadania
nie zmieniamy obliczonej do tej pory sumy.
