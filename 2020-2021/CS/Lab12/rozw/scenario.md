## 

materiału jest sporo, studenci będą mieli coś do zrobienia tylko w pierwszej części - pliki
resztę trzeba będzie pokazać i opowiedzieć, będzie trochę live-codingu
przeczytajcie proszę przed labami nie idźcie na żywioł, bo może być ciężko

## 0 - files, streams, serialization (40 minut)

Zadanie zaczynamy od omówienia System.IO - wspominamy, że csharp udostępnia gotowe klasy do manipulacji na plikach/katalogach.
Prosimy studentów, aby skorzystali z klasy Directory do wyświetlania obecnej ścieżki programu,
oraz aby wyświetlili listę plików w katalogu projektu. Czekamy z 2 minuty, następnie pokazujemy rozwiązanie.
W miedzyczasie możemy opowiedzieć jakie operacje możemy robić na plikach - zmiany atrybutów, usuwanie, kopiowanie,
przenoszenie, a że zawartością plików zazwyczaj manipulujemy za pomocą strumieni albo po prostu serializacji.

Następnie przypominamy syntax `using` (IDisposable) i prosimy, aby wczytali osoby z pliku persons.csv korzystając z StreamReadera.
Możemy powiedzieć, że plik będzie skopiowany podczas budowania projektu do tego samego folderu, gdzie jest exe
i wystarczy nazwę pliku podać jako "./persons.csv".

Dajemy tutaj z 4 minut na zrobienie tej części. 

W międzyczasie możemy powiedzieć, że w csharpie jest wiele klas pomocnych do czytania strumieni i pisania do strumienia,
np. FileStream, StreamReader, StreamWriter czy operacji na nich: GZipStream, MemoryStream.

Krótko przypominamy czym jest serializacja, że jest to utrwalenie obiektu, tak, aby można było, np. go gdzieś przesłać,
albo zapisać i wczytać przy następnym uruchomieniu programu. Proces wczytywania obiektu do pamięci to deserializacja.
Mówimy, że csharp dostarcza kilka standardowych mechanizmów do serializacji, np. serializację binarną,
serializacja do XML oraz wraz z dotnet corem wbudowaną do JSON'a.

Tutaj możemy dać 3 minuty na zaimplementowanie binarnej serializacji. 

W międzyczasie możemy opowiedzieć, że zazwyczaj mamy dwa podejścia do zarządzania sposobem serializacji
- atrybuty oraz przekazywanie argumentów podczas serializacji/deserializacji.
Np. za pomocą atrybutów możemy kontrolować czy dane pole powinno być zserializowane albo jak dane pole powinno się nazywać
- może być tak, że plik, który chcemy wczytać mają nazwy, których nie można nadać zmiennym, np. myślniki. 

Po tym czasie piszemy kod do serializacji binarnej, wspominamy, że BinaryFormatter wymaga do działania
atrybutu [Serializable] - dodajemy [Serializable].
Wywołujemy jeszcze metodę, która porównuje dwie listy osób, aby pokazać, że wszystko ładnie się wczytało.

Pokazujemy, że użycie XmlSerializer'a jest praktycznie takie samo jak BinaryFormatter, różni się sposobem inicjalizacji
- musimy podać typ docelowy oraz zdefiniować konstruktor bezparametrowy. Reszta pod względem serializacji wygląda praktycznie tak samo.

Możemy dodatkowo pokazać przykład atrybutów do modyfikacji serializacji, np. [XmlIgnore], [XmlElement]
i pokazać, jak zmienia się docelowy plik. 

Następnie pokazujemy, że strumienie są przystosowane do tego, aby je ze sobą łączyć
- można w ten sposób dodać dodatkowe warstwy, np. kompresji czy szyfrowania.
Prosimy, aby użyli GZipStreama do odczytania pliku "./compressed.txt.gzip" i wyświetlili dwie pierwsze linie na konsolę.

Następnie pokazujemy częste limitacje serializerów - cykliczne odwołania (circular depedenency) oraz referencje.
W tym celu posłużą nam dwie klasy Parent i Child. Jeżeli czujemy, że nie zdążymy to sami możemy pokazać przeklejając kod
i omawiając. Niż czekać na studentów.

Możemy pokazać, że BinarySerializer nie ma tych limitacji, po deserializacji referencje są poprawnie zachowane.
Wspominamy tutaj, że BinarySerializer nie należy używać do deserializacji danych, którym nie ufamy,
potężny serializer ale ma luki bezpieczeństwa, niestety również mało przenośny.

Potem pokazujemy, że XML już sobie nie radzi z na pozór prostymi rzeczami.

Dodajemy, że .NET udostępnia również DataContractSerializer, który ma mniej limitacji, np. pozwala zachować referencje,
ale z drugiej strony używa niestandardowych mechanizmów, co powoduje, że jest mało przenośny.

Następnie pokazujemy JSON serializer, który został wprowadzony do dotneta niedawno wraz z dotnet corem. 
Tutaj warto się skupić na elastyczności jaką na serializator zapewnia, docelowe typy podajemy tylko w momencie deserializacji.

Warto też wspomnieć, o tym, że użycie serializatora zazwyczaj zapewni, że dane zostaną zapisane w formacie, 
który umożliwia łatwą wymianę informacji pomiędzy aplikacjami oraz nie powoduje problemów podczas parsowania oraz zapewni 
takie samo znaczenie danych.
Jako przykład można podać datę i różniący się zapis daty dziennej w Ameryce i w Europie. 
Czy problem z różnymi strefami czasowymi.

Dodatkowo wspominamy, że kolejnymi problemami podczas serializacji/deserializacji może być hierarchia klas
albo po prostu zmiany modeli wraz z kolejnymi aktualizacjami aplikacji.

Wspominamy, że nie należy serializować danych z niezaufanych źródeł - i praktycznie podatności istnieją na każdy serializer.
OWASP TOP 10 listuje 'insecure deserialization' na siódmym miejscu.


## 1 - events (15 min)

Przypominamy model sterowany zdarzeniami. 

Trochę narzekamy, że aplikacja konsolowa to trochę nie ten typ aplikacji do używania eventów.
I eventy lepiej się sprawdzają w aplikacjach z interfejsem graficznym, wówczas możemy reagować na akcje użytkownika.
Koniec końców pokazujemy jak można użyć eventy do zaimplementowania prostego wyścigu 1D.

Opisujemy MoveEventArgs jako klasę, za pomocą której każdy uczestnik będzie informał o ile się poruszył.

Opisujemy interface IParticipant, a szczególnie `event EventHandler<MoveEventArgs> Moved` - będziemy nasłuchiwać
na przemieszczenia się uczestnika, oraz dwie metody służące do poinformowania uczestnika o starcie oraz końcu wyścigu
- każdy będzie startować w tym samym czasie, ale nie koniecznie tak samo skończą.

Omawiamy implementację klasy psa. Zaznaczamy, że metoda Run jest synchroniczna i ma potencjalnie nieskończoną pętlę
- będziemy biec tak długo, dopóki nie skończymy wyścigu - o czym się dowiemy jak ktoś zawoła metodę 'OnFinishLineCrossed'. 
Omawiamy event Moved - za pomocą niego będziemy informować zewnętrzny świat o zmianie naszego położenia.
Tutaj mówimy, że wykorzystujemy domyślną implementację eventu - każdy może się pod niego subskrybować,
ale tylko z wewnątrz klasy możemy podnosić (raised) zdarzenie.

Przechodzimy do omówienia klasy Race. Skupiamy się na implementację eventu. Wskazujemy, że w przeciwieństwie do wcześniejszego eventu
sami napisaliśmy logikę subskrypcji i jej anulowanie - pozwoliło nam to na osiągnąć, że do wyścigu nie można się zapisać wiele razy.

Przechodzimy do omówienia metody `OnParticipantMove` za jej pomocą będziemy reagować na przemieszczenia się zawodników.
Wspominamy, że też w tym miejscu będziemy informować uczestnika o tym, że przekroczył linię mety
- wówczas uczestnik powinien przestać biec, ale nawet jeśli nie to w ramach zabezpieczenia przestajemy nasłuchiwać na zdarzenie.

Na końcu omawiamy metodę startu wyścigu. Mówimy, że pobieramy wszystkich uczestników z listy subskrybentów eventu
- ustawiamy ich na linii startu (przygotowanie słownika), zaczynamy nasłuchiwać na ich zmiany położenia
- tutaj możemy wspomnieć, że podczepiając się wcześniej, moglibyśmy dostawać zdarzenia,
nawet jak wyścig się jeszcze nie zaczął. Tutaj też wykorzystujemy Task.Run, aby równolegle wystartować uczestników 
móc śledzić ich postęp, a na końcu zwracamy Taska, który zakończy się w momencie ukończenia wyścigu przez wszystkich uczestników.

Krótko omawiamy Maina, najciekawszy punkt tutaj to subskrybcja pod event oraz wspomnienie, że nawet gdyby pieski się poruszały
przed wystarowanie a po zapisaniu się do wyścigu, to byśmy nie dostawali tych powiadomień w klasie Race z powodu tego
jak podczepiamy się pod zdarzenie. 
Również warto wspomnieć, że pieski są iterowane w takiej samej kolejności w jakiej się podczepiały pod zdarzenie,
ale kolejność wywołania handlerów już nie jest zapewniona przez to, że stosujemy Task.Run, który kolejkuje zadania.

Możemy wspomnieć, że sam mechanizm eventów powinien być stosowany tylko do przekazywania informacji,
ponieważ wykonywanie czasochłonnych reakcji jako wynik zdarzenia blokuje zgłaszającego zdarzenie
- w takich sytuacjach lepiej jest przyjąć informację o zdarzeniu i uruchomić reakcję w oddzielnym wątku.

## 2 - parallel  (20 min)

Wspominamy, że czasem mamy zadania, które wymagają przetworzenia wielu elementów w taki sam sposób.

Mówimy, że C# udostępnia klasy, które pomagają w stosunkowo prosty sposób zrównoleglić wykonanie naszego programu.
Np. klasa Parallel, która zrównolegla wiele typowych zadań - np. zrównoleglony for/foreach, 
oraz różne prymitywy do synchronizacji - Monitor (a.k.a lock), Barrier, Semaphore.

Pokazujemy, nasz przykład - sprawdzanie, które liczby pierwsze. Mówimy, że jest to zadanie, które bardzo łatwo zrównoleglić.

Wymieniamy różnice między obiema wersjami, mówimy, że z punktu widzenia programisty zmiana jest znikoma.
Fora musieliśmy zastąpić wywołaniem statycznej metody oraz zamienić ciało funkcji na lambdę.
Wspomnieć o argumentach lambdy, że pierwszy to element z kolekcji, drugi to stan wykonania zrównoleglonej pętli, a trzeci to indeks elementu.
Możemy wspomnieć, że drugi argument pozwala na manipulację stanem wykonania zrównoleglonej pętli, 
możemy np. przerwać wykonanie pętli jak w normalnym forze - odpowiednikiem 'break'a jest metoda Break.
Jest to przydatne, jeżeli wykorzystujemy zrównoleglenie do znalezienia jakiegoś rozwiązania.

Dodatkowo pokazujemy jakie mogą być konsekwencje używania równoległości bez zapewnienia synchronizacji
i pokazujemy alterantywne metody do wykonanie danej akcji poprawnie w csharpie. 

Mówimy, że ogólnie równoległość stosujemy do zadań, które wymagają dużej ilości obliczeń
i pisząc kod równoległy musimy pamiętać o większej liczbie rzeczy niż pisząc kod jednowątkowy.

Dodatkowo przy okazji pokazujemy StringBuildera. Możemy uruchomić normalnie i poczekać na wyniki,
albo również za pomocą profilera (Alt + F2) i zaznaczyć opcje CPU Usage oraz .NET Counters (opcjonalnie).
Mówimy przyczynę, czemu StringBuilder jest szybszy - stringi są immutable

## 3 - async (25 min)

Zaczynamy od tego, że czasem mamy dużo zadań, które długo trwają, ale nie są powiązane z czasem obliczeń,
a czekaniem na zewnętrzne urządzenie, np. dysku, strumień sieciowy - ogólnie operacje IO.

Przypominamy, że koncepcyjnie model asynchroniczności polega na tym, że oznaczamy w kodzie miejsca,
które mogą potencjalnie długo zająć i wówczas przekazujemy sterowanie komuś innemu
- (nie potrzebujemy czasu CPU, bo i tak czekamy), a sterowanie wraca do nas kiedy operacja na którą czekaliśmy się wykonała. 

Dodatkowo podkreślamy, że asynchroniczność nie jest tym samym co równoległość.
W przypadku maszyny z jednym rdzeniem nie jesteśmy w stanie osiągnąć równoległości, ale jesteśmy w stanie osiągnąć asynchroniczności. 

Przypominamy, że w csharpie stosujemy Parallel/Thread do przyspieszania zadań, które są limitowane kosztem obliczeń (CPU-bound),
a Task/async do uzyskania lepszej skalowalności dla zadań IO-bound (latency-hiding).

Mówimy, że csharp udostępnia nam słówka kluczowe async i await, które pozwalają w 'prosty' sposób pisać programy w sposób asynchroniczny,
czyli oznaczenie fragmentów w kodzie, na których 'czekamy' na wynik, oraz klasę 'Task' do przekazywania sterowania/wyników
oraz statyczne metody do oczekiwanie na wykonanie wszystkich zadań albo poczekanie na pierwsze zadanie z wielu.

Wprowadzamy do naszego zadania. 
Mamy aplikację kliencką, która chce zsumować wyniki jakichś operacji. Aplikacja nie wykonuje obliczeń, a jedynie je zleca
i czeka na wynik wyliczony na serwerze. Implementacja serwera jest już dostarczona i nie będziemy się nią zajmować. 
Jedyne co jest ważne, to że serwer działa w sposób asynchroniczny.

Chcemy napisać kod klienta, który w sposób asynchroniczny:
- wyśle operacje na serwer
- zbierze wyniki
- zaggreguje je

Mówimy, że zadanie będzie miało 2 etapy:
- server nie ma limitu aktywnych zadań oraz nie rzuca wyjątkami
- server nie ma limitu aktywnych zadań oraz czasem rzuca wyjątkami

Tutaj możemy szybko pokazać, do czego mamy dostęp z poziomu klienta - szybkie pokazanie interfejsu IServer oraz klasy SimpleResponse.
Chcemy polepszyć implementację metody SumResponses, która początkowo nie jest asynchroniczna.

Zaczynamy od omówienia wersji sekwencyjnej (wersja z .Result) i będziemy przechodzić, kolejne etapy
pokazując jak można w miarę prosty sposób przekształcić synchroniczną wersję na asynchroniczną. 

W przypadku wersji sekwencyjnej pokazujemy, że sterowanie jest zwracane do maina jest dopiero po zsumowaniu wszystkiego.
> Console.WriteLine("Stable server - simple requests - flow returned to main");
I że nasza metoda nie działa w sposób asynchroniczny. 
Wspomninamy, że czasem musimy skorzystać z .Result, przykładowo kiedy chcemy zawołać asynchroniczną metodę z kodu synchronicznego, ale zawsze gdzie mamy możliwość
powinniśmy stosować await, jeżeli zależy nam na skalowalności.

(SEQUENTIAL_ASYNC)
Odkomentowujemy #define, pokazujemy różnice - dodane słówko async do sygnatury metody oraz zamiast Result użyty await.
Mówimy, że wciąż żądania są wysyłane w sposób sekwencyjny, ale czekanie już jest w sposób asynchroniczny.
Możemy pokazać, że sterowanie jest zwracane natychmiast do maina i drukowany na konsole jest poniższy napis, zanim wszystkie requesty zakończą swoje działanie.
> Console.WriteLine("Stable server - simple requests - flow returned to main");

(PARALLEL_NAIVE)
W dalszej części mówimy, że serwer jest w stanie obsłużyć, więcej niż jedno żądanie jednocześnie, więc chcemy doprowadzić,
że wysyłamy wszystkie żądania naraz i czekamy asynchronicznie, aż się wszystkie skończą, a następnie agregujemy wyniki.
Tutaj mówimy, że csharp udostępnia nam metodę Task.WhenAll, która ułatwia pisanie kodu pod takie scenariusze. 

Mówimy, że jednak takie podejście ma wady, np. nie kontrolujemy ile zapytań wysyłamy, a nie chcemy dos'ować serwera, 
i że zamiast naiwnie wołać zapytania, można byłoby napisać jakiś system kolejkowania zapytań, aby po stronie klienta trzymać
X aktywnych zapytań.

(ERROR_HANDLING)
Pozostaje nam pokazać, jeszcze jak obsługiwać wyjątki, które są rzucane podczas oczekiwania na wynik operacji,
i że robimy to w taki sam sposób jak normalne łapanie wyjątków. Pokazujemy, nasze rozwiązanie, w którym dodaliśmy metodę, 
która skonsumuje ewentualny wyjątek i zwróci sztuczny response z wartością 0, ale ogólnie mówimy, że nie sposób łapania wyjątków nie różni się od normalnego sposobu.




