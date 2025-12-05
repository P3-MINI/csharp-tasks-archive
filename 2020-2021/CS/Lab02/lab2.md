# Lab 2 - (tutorial o VS, proste przyklady, nie punktowane)

## Wstęp (5 -a 10 minut)

Przedstawiamy się, dajemy jakiś kontakt do siebie (pewnie maila).
Krótki opis zajęć, że będziemy się uczyć csharpa i jak będą wyglądać laboratoria. Wspominamy, że regulamin przedmiotu wisi na stronie.
Zajęcia zaczynają się o 14:15 i trwają 1.5h do 15:45.
Mianowicie mówimy, że część zadań będzie tutorialowa. Część zadań będzie punktowana; wówczas do zdobycia będzie od 0-5 punktów, w zależności od ukończonych etapów. 
W przypadku punktowanych zadań student wysyła rozwiązanie prowadzącemu na maila; nie później do 16:00. Wspominamy, że te 15 minut jest na to, aby doprowadzić swoje rozwiązanie do postaci, które się kompiluje i nie rzuca wyjątkami podczas runtime'u. Za rozwiązanie, które nie spełnia warunków jest 0.

## Tutorial z visuala 

### Tworzenie projektu (5 - 10 minut)

Wspominamy, że Visual Studio jako IDE jest w stanie wspierać wiele różnych projektów i języków.
Klikamy Create a new project i wskazujemy, że jest dużo szablonów projektów, i że jest ważne, aby wybrać odpowiedni. 
Rozwijamy 'All project types' i mówimy, że w VS możemy tworzyć wiele różnych typów projektów, ale nas będzie interesować tylko aplikacje konsolowe. Wybieramy opcję Console. 
Mówimy, że sam .NET jest platformą, w której możemy pisać w wielu językach, np. w csharpie czy w visual basicu. Więc wybieramy zmieniamy 'All languages' na C#. Powinny zostać nam dwa wyniki, mówimy, że są dwie platformy .NET - Framework i Core. Wspominamy, że .NET Core jest propozycją microsoftu, aby w csharpie móc pisać pod wszystkie platformy (linux, macOS), Framework jest przeznaczony tylko na windowsa, ale docelowo Microsoft, chce wypuścić .NET 5, który połączy oba frameworki w jeden, który ma być  opensource'owy i crossplatformy. Mówimy, aby wybrali .NET Core i kliknęli next.

Mówimy, żeby wybrali gdzie stworzyć dany projekt, wybrali jakąś nazwę i pozostali resztę opcji domyślnie. 

### Omówienie domyślnego projektu (15 - 20 minut)

Mówimy, że powinni zobaczyć trochę wygenerowanego domyślnie kodu i żeby go uruchomili, klikajac CTRL + F5. 
Powinni zobaczyć "Hello world". (Taki check, że wszystko jest okey)

Pokazujemy okienko Solution explorer i mówimy, że tutaj mamy przedstawioną strukturę naszego projektu.
Mówimy, że w prawdziwej aplikacji byśmy mieli wiele różnych projektów, np. projekt bazodanowy, projekt z logiką biznesową, projekt z warstwą prezentacji; ale u nas będziemy mieli tylko jeden projekt. Rozwijamy Dependencies i pokazujemy, że nasz program ma domyślnie załączonych całkiem sporo zależności. Możemy wspomnieć, że platforma .NET zapewnia nam całkiem sporo rzeczy, aby programowało się nam szybciej i przyjemniej, jak. np. wbudowane rzeczy do obsługi plików (System.IO), do obsługi sieci (System.Net, System.Net.Http), do tekstu (System.Text), wielowątkowości (System.Threading) i asynchroniczności (System.Threading.Tasks).

Wracamy do naszego Program.cs i mówimy o różnicach pomiędzy C# a C/C++ w przykładowym pliku.

Zaczynając od samej góry możemy wskazać using System; i że to jest odpowiednik z c++ using namespace.
Wspominamy, że C# jest językiem w pełni obiektowym, i np. każda klasa ma swoją nazwę oraz namespace, z którego pochodzi. 
W C++ można było załączyć math.h i wołać metody, np. floor bez żadnego specyfikowania o jaką tak naprawdę funkcję floor nam chodzi.
W csharpie trzeba jawnie to wskazać, i do tego służą namespace'y.
Jedną z nich jest przykładowo statyczna klasa Console, której używamy do wypisania tekstu na ekran.
Komentujemy pierwszą linię i pokazujemy, że dostajemy błąd, gdyż obecnie VS nie wie czym jest Console.WriteLine. Wchodzimy w linijkę, tam gdzie jest Console.
WriteLine i dopisujemy na początku System. Problem znika, mówimy, że to przez to, że jawnie wskazaliśmy lokalizację klasy, z której chcemy skorzystać.
Możemy skasować System, powinno znowu podświetlić błąd, następnie klikamy (Ctrl + .), mówiąc, że visual udostępnia nam taki pomocnik, co możemy zrobić w danej sytuacji, który pomaga w wielu często występujących problemach albo przyspiesza pisanie programów; pokazujemy, że w naszym przypadku mamy dwie podpowiedzi, aby dodać using na początku programu albo jawnie wskazać namespace.

Idąc dalej natrafiamy na namespace ConsoleApp1, mówimy, że tutaj definiujemy nasz własny namespace, i że jeżeli, ktoś chciałby skorzystać z naszego kodu to musiałby odwołania do naszych klas/method poprzedzać właśnie tym namespacem albo użyć usinga na początku pliku. 

Następnie mówimy, że jest definicja klasy (bo C# jest językiem obiektowym) oraz statycznej metody Main, która służy jako punkt wejściowy programu i pewnie tak jak dobrze znamy z c++ przyjmuje listę argumentów, aczkolwiek nie jest to jak w c++, liczba argumentów i wskaźnik na tablicę charów; tylko tablica napisów, gdyż tablice w C# wiedzą już o tym, ile elementów przechowują.

Tutaj możemy szybko dopisać kilka linii kodu, które wyświetlą zawartość przekazanych argumentów oraz uruchomić program z konsoli, przekazując kilka dodatkowych parametrów aby pokazać, że faktycznie to działa.

### Prosty przykład - liczenie wartości wielomianu za pomocą schematu Hornera (15 minut)

Mówimy, że chcemy napisać prosty przykład, który będzie liczyć wartość wielomianu za pomocą schematu Hornera. 
Będziemy prosić użytkownika o podanie liczby wspołczynników, następnie wartości współczynników oraz wartość punktu w którym chcemy wyliczyć wartość.

Mówimy, że program będzie przypominał program z wykładu i wykorzystamy te same rzeczy. 

### Factorial - korzystanie z debuggera

Napiszemy rekurencyjną metodę liczenia silni. Będzie to nam służyć do pokazania debuggowania.

Pokazujemy, że dla 0,1,2,3 działa. Pokazujemy wynik dla 20tu (powinien wyjść ujemny). Możemy się spytać się studentów czemu wyszedł wynik ujemny jak przemnażamy liczby dodatnie. Tutaj mówimy, że wystąpił overflow i mówimy, że C# udostępnia nam metody, które pozwalają wykryć takie błędy na bardzo wczesnym etapie testowania aplikacji.

Odpalamy program z debuggerem za pomocą F5. Powinien program przejść od początku do końca bez zatrzymywania się. Tutaj powiemy, że domyślnie overflow nie jest traktowany jako błąd w sensie wyjątku i przykładem, gdzie nie przeszkadza nam overflow jest przykładowo kiedy liczymy hash danego obiektu; i jest odpowiedni syntax do włączania poprawności obliczeń, ale, że go jeszcze nie znamy, to zmieniamy ustawienia projektu (Project properties -> Build -> Advanced -> Check for arithmetic overflow).

Odpalamy jeszcze raz program za pomocą F5, powinniśmy się zatrzymać na wyjątku. 

Pokazujemy i omawiamy okienka Locals, Watch, Call Stack, Exception Settings.

Locals - zmienne, które są w naszym obecnym scopie.
    powinniśmy widzieć rzucony exception, możemy pokazać, że widzimy również wartości pól/propertiesów danego obiektu, które możemy 'rozwijać',
    wartości tekstowe możemy również podglądać lupką, np stacktrace  
Watch - możemy tutaj wpisywać swoje własne wyrażenia, które mogą nam pomóc w debugowaniu
    możemy tutaj dla przykładow wywołać jakąś statyczną funkcję lub properties, np. Environment.CurrentDirectory  
Call stack - mówimy, że mamy podgląd całego wykonania programu do momentu rzucenia wyjątku; możemy zmieniać ramki, aby podejrzeć stan poprzedniej funkcji.
    możemy kliknąć również prawym i zaznaczyć opcję 'Show parameters value'  
Exception Settings - możemy powiedzieć, że domyślnie debugger zatrzymuje się na wyjątku, który nie jest przez nas obsłużony; 
    możemy owrapować wywołanie do Factorial(20) try/catchem i pokazać, że teraz nie zatrzymamy się w funkcji Factorial, a w mainie, i że przydatne może być zatrzymanie się na źródle zgłoszenia wyjątku, aby wykryć problemy (zaznaczamy wszystkie opcje w CLR Exceptions) i uruchamiamy program jeszcze raz


Możemy też uruchomić debugger w trybie krokowym (ctrl + F10) i pokazać na przykładzie schematu hornera przechodzenie krok po kroku po instrukcjach (F10)). 
Pokazujemy również, że możemy skakać do wyrażeń za pomocą myszki (najechać na jakieś wyrażenie i po chwili powinno się pojawić po lewej stronie zielona strzałka)
Pokazujemy również, że istnieje coś takiego jak breakpoint, i że można ustawiać w nich warunki.


### Przygotowanie paczki do wysłania

Tutaj omówimy też pliki generowane na dysku. 

.sln - plik solucji, który zawiera informacje z jakich projektów składa się nasza aplikacja  
folder z projektem, wewnątrz którego są:  
bin - binarki, tam też znajdziemy nasz skompilowany program  
obj - pośrednie pliki  
csproj - definicja projektu  

Klikamy Build -> Clean Solution, wówczas pliki bin/obj powinny zostać wyczyszczona. Zaznaczamy folder z projektem i solucję i pakujemy; pokazujemy, że plik powinien ważyć bardzo mało.