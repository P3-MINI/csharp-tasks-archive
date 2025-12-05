# Laboratorium 10 - Strumienie i Serializacja
## Wstęp
> *Jako student informatyki na specjalności **KACZ/KAM**, jesteś częścią zespołu projektowego, który wspólnie pracuje nad stworzeniem gry na zaliczenie projektu zespołowego. Po długich dyskusjach i burzy mózgów wybraliście gatunek RPG, w którym gracz wcieli się w kaczkę przemierzającą bezkresny ocean ze swoim stadem, próbując odnaleźć drogę do domu. Jako członek zespołu, to właśnie Tobie powierzono kluczowe zadanie: zaimplementowanie mechanizmu wczytywania sceny oraz funkcji szybkiego zapisu i odczytu gry. To od Ciebie zależy, czy gracz będzie mógł swobodnie kontynuować swoją przygodę po zamknięciu gry. Wyzwanie jest spore, ale rozwiązania tych problemów to klucz do sukcesu całej produkcji.*

## Struktura rozwiązania

W rozwiązaniu znajdują się 2 projekty. 
* **Duck** - projekt z grą, w którym dane będzie powierzone Tobie zadanie,
* **OpenGL** - projekt odpowiedzialny za niskopoziomową komunikację z kartą graficzną.

Większość Twojej pracy skupi się na klasie `Scene`. To tam będziesz implementować kluczowe funkcje związane ze swoim zadaniem. Dodatkowo będzie trzeba wprowadzić drobne zmiany w klasach podlegających serializacji, tam wystarczy dopisać kilka potrzebnych atrybutów sterujących serializacją.

## Etap 1 (2 punkty)
> *Czym byłaby kaczka bez swojego wiernego stada? Twoim pierwszym wyzwaniem będzie zaimplementowanie mechanizmu wczytywania sceny, który określi początkowe rozmieszczenie kaczek w stadzie. To kluczowy element, który pozwoli graczowi od razu poczuć więź z grupą i zanurzyć się w świecie gry.*

W pliku `Scene.cs` skoncentruj się na konstruktorze, który przyjmuje ścieżkę do pliku z zasobami wbudowanymi w plik wykonywalny. To właśnie tam zaimplementujesz logikę odpowiedzialną za wczytanie danych o grupie kaczek. Informacje o kaczkach przechowywane są w pliku `Resources/ducks.csv`, który zawiera dane w przejrzystym formacie tekstowym, gdzie wartości oddzielone są średnikami. Każdy kolejny wiersz definiuje jedną kaczkę wraz z jej parametrami, które odpowiadają:

1. Imieniu kaczki – ułatwia identyfikację poszczególnych postaci.
2. Pozycji w świecie – współrzędne X i Z, określające lokalizację na płaszczyźnie w przestrzeni 3D.
3. Rotacji – orientacji kaczki w stopniach, pomagającej odwzorować jej ustawienie.
4. Skali – proporcjom kaczki, które zadecydują o jej rozmiarze w świecie gry.

Parametry wczytane z pliku `ducks.csv` dokładnie odpowiadają argumentom wymaganym przez konstruktor obiektu kaczki. Pamiętaj tylko o zamianie stopni na radiany. Nowo utworzone obiekty kaczek powinny być dodawane do listy Ducks, która pełni rolę kontenera przechowującego wszystkie kaczki obecne w danej scenie. To właśnie ta lista, `Ducks`, jest kluczowym elementem, który powiązany z silnikiem gry sprawi, że Twoje kaczki automatycznie pojawią się w wirtualnym świecie. Gdy tylko zostaną dodane do listy, staną się widoczne na scenie, gotowe do interakcji. Dzięki temu mechanizmowi gracz będzie mógł od razu zanurzyć się w swojej kaczej przygodzie, w otoczeniu starannie rozmieszczonych towarzyszy swojego stada.

Przykładowy fragment zawartości pliku ducks.csv wygląda następująco:

```
Shell;127.75,-36.55;274.13;0.93
Rainbow;-56.35,181.51;5.12;1.13
Sprinkle;175.79,129.00;353.64;1.02
```

## Etap 2 (3 punkty)
> *Kolejnym istotnym krokiem będzie zaimplementowanie opcji szybkiego zapisu gry. Co jeśli gracz podejmie złe decyzje i zechce cofnąć się do wcześniejszego stanu gry? A co, jeśli będzie musiał przerwać rozgrywkę i wrócić do niej później? Twoim zadaniem będzie stworzenie systemu, który zapisze kluczowe elementy gry, takie jak aktualne ustawienia kaczek – ich pozycje, rotacje czy skalę – umożliwiając graczom wygodne kontynuowanie swojej przygody w dowolnym momencie.*

W tym etapie Twoim zadaniem będzie zaimplementowanie funkcji `QuackSave` w pliku `Scene.cs`. Funkcja ta będzie odpowiedzialna za serializację całej sceny do formatu JSON. Aby dodatkowo zoptymalizować przestrzeń zajmowaną przez szybki zapis na dysku gracza, konieczne będzie zastosowanie mechanizmu kompresji - możesz wybrać dowolny mechanizm, taki jak GZip czy Deflate. W odpowiednich miejscach dodaj do klasy `Duck` odpowiednie atrybuty, umożliwiające poprawną serializację. **Nie wolno dodawać innych modyfikacji w pliku Duck.cs, niż dodawanie atrybutów.**

Funkcję szybkiego zapisu skonfiguruj tak, aby plik zapisywany był w katalogu dokumentów użytkownika, w dedykowanym podkatalogu `Duck`, pod nazwą `quack.save`.

Unikaj serializacji niepotrzebnych właściwości. Nie serializuj tekstury, siatki modelu oraz billboardu (Mesh, Texture oraz NameBillboard), ich serializacja byłaby nadmiarowa, gdyż jesteśmy w stanie odtworzyć te właściwości na podstawie innych.

**Podpowiedzi:**  
- Ścieżkę katalogu można uzyskać przez funkcję `Environment.GetFolderPath`, podając jej odpowiedni enum. https://learn.microsoft.com/en-us/dotnet/api/system.environment.specialfolder?view=net-9.0
- Serializacja typu będącego interfejsem jest kłopotliwa (IBehaviour). W momencie deserializacji deserializator musi wiedzieć jakie są możliwe implementacje interfejsu. W takim wypadku interfejs należy oznaczyć atrybutem JsonDerivedType, opisującym możliwe implementacje. https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/polymorphism
- Wygląda na to, że użyte typy `Vector` nie są w prosty sposób serializowalne. Użyj dostarczonych konwerterów Vector2Converter oraz Vector3Converter, które powinny załatwić problem konwersji. Dodaj je do obiektu `JsonSerializerOptions`. https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/converters-how-to#registration-sample---converters-collection

## Etap 3 (3 punkty)
> *Czym byłby szybki zapis bez możliwości szybkiego wczytywania? Ostatnim etapem Twojego zadania będzie implementacja systemu, który pozwoli przywrócić stan gry z wcześniej utworzonego pliku `quack.save`. Dzięki temu gracz będzie mógł wrócić do swojego kaczkowego świata w każdej chwili, kontynuując przygodę dokładnie tam, gdzie ją przerwał. Twoja praca połączy oba mechanizmy w spójny system, kluczowy dla komfortu rozgrywki.*

Twoim ostatnim zadaniem będzie implementacja funkcji `QuackLoad` w pliku `Scene.cs`. Funkcja ta będzie odpowiedzialna za wczytywanie zserializowanego i skompresowanego stanu gry z pliku `quack.save`. Korzystając z wbudowanego mechanizmu deserializacji, odtwórz zapisane obiekty i przywróć scenę do dokładnego stanu, w jakim gracz ją opuścił. Aby zadbać o stabilność działania, zaimplementuj obsługę sytuacji wyjątkowych – jeśli plik nie istnieje lub nie da się go poprawnie zdeserializować, funkcja powinna zwrócić wartość `null`, na podstawie której silnik gry pozna, że wczytywanie się nie powiodło.
