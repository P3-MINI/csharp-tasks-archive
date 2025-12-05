# Zadanie serializacja/deserializacja

Firma w której pracujesz zawiera zbiór danych biznesowych (eventów). 
Do tej pory eventy te były przetrzymywane płasko w jednym zbiorze. 
W związku z rozporządzeniem RODO firma jest zmuszona aby wprowadzić usprawnienia. 
Chodzi o to aby łatwo można było pobrać sobie dane konkretnej osoby, oraz łatwo można było usunąć jej dane (event-y). Jesteś odpowiedzialny za zbudowanie prototypu do migracji danych do nowego sposobu ich przetrzymywania.
Jako punkt startowy otrzymałeś eksport (testowych) danych w pliku `import.xml`

## --- Etap 1 --- (1 pkt)
Stwórz klasę `Event` z takimi polami aby można było przeczytać dane z 
pliku `import.xml`. 
Zaimplementuj w niej interfejs `IEquatable<T>`. Dwa eventy są takie same jeśli wszystkie pola mają taką samą wartość.

Napisz klasę `EventManager` która w konstruktorze otrzyma ścieżkę (`rootPath`) w której będzie przechowywane eventy w nowej formie.
Jeśli ścieżka `rootPath` nie istnieje powinna zostać stworzona.

Dodaj metodę `Import` do `EventManager`-a która przyjmuje jeden parametr `filePath` a zwraca kolekcję zaimportowanych `Event`-ów.
Obsłuż sytuację gdy plik we wskazanej ścieżce nie istnieje - wyrzuć `FileNotFoundException`

## --- Etap 2 --- (2 pkt)
Dodaj do klasy `EventManager` metodę `AddEvent` która przyjmie obiekt typu `Event`. Metoda powinna utworzyć nowy plik lub dodać do już istniejącego zserializowany event. 
`Event`-y w pliku powinny być zserializowane jako json. Gdzie nowy zserializowany event zapisywany jest w nowej linii. [Line-delimited JSON](https://en.wikipedia.org/wiki/JSON_streaming)

Poniżej przykład:

```json
{"Id":"ad121fea-9703-4a88-bd0e-21df881334b9","Name":"ContactMessageSent","EventOccurredAt":"2020-01-07T18:10:00","Owner":"Iwona Kiwa\u0142a","Details":"ContactMessageId=123"}
{"Id":"5b541435-5363-4012-8688-701f3fe88ee3","Name":"ProgramChanged","EventOccurredAt":"2020-01-07T18:54:00","Owner":"Iwona Kiwa\u0142a","Details":"ProgramId=P1"}
```

Dodatkowo dane powinny być spartycjonowane (podzielone na pliki) w sposób zaprezentowany poniżej.
`/Owner/Year/Month/yyyy-MM-dd.json`
Jeśli nie istnieje którykolwiek z podkatalogów powinien zostać stworzony.
Dane `Owner`, `Year`, `Month`, `yyyy-MM-dd` są danymi z Event-u.

## --- Etap 3 --- (1 pkt)
Rozszerz `EventManager`-a o nowe możliwości związane z obsługą GDPR.
Dodaj nową metodę `GetOwnerEvents` która przyjmuje jako parametr identyfikator `owner`-a a zwraca kolekcję `Event` danego użytkownika.
Jeśli podano identyfikator który nie istnieje zwracana jest pusta kolekcja.

Dodaj drugą metodę `RemoveOwnerEvents` która usuwa wszystkie dane dla podanego `owner`-a.
Jako parametr przyjmuje identyfikator `owner`-a.
Zwraca wartość `true/false` w zależności od tego czy udało się usunąć dane. 
Jeśli nie ma danego użytkownika zwraca `false`.

## --- Etap 4 ---  (1pkt)
Dodaj metodę do `EventManager`-a która wyeksportuje wszystkie dane do jednego pliku.
`Export`. Metoda dostaje jako parametr `filePath` i eksportuje dane w tym samym formacie  w którym został dostarczony w pluku importu.