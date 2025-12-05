Uwaga: kompilacja zadania nie powinna dawać błędów bądź ostrzeżeń np. CS0108
Aby upewnić się że tak jest można włączyć opcję:
Project properties -> Build -> Treat warnings as errors -> is ustawić set to All

-------------------------------------------------------------------------
Celem zadania jest zaimplementowanie klasy uogólnionej (generycznej) `MinPriorityQueue<T>`
czyli kolejki priorytetowej przechowujęcej elementy typu T (mniejsza wartość oznacza lepszy priorytet).
Wewnętrznie kolejka powinna (musi) być zaimplementowana jako lista wiązana tzn. element zawiera referencję
na kolejny element (potrzebna jest klasa pomocnicza defimiująca węzeł listy)
Typ T musi być typem bezpośrednim nie przyjmującym wartości null.
> ! UWAGA: nie można używać standardowych kolekcji C# np. LinkedList, List, tablic itp.

-------------------------------------------------------------------------
Etap 1 (3pkt)
Należy zaimplementować klasę `MinPriorityQueue<T>`
- klasa ma implementować intefejs `IPriorityQueue<T>` podany w pliku `PriorityQueue.cs`
- nałożyć na typ `T` ograniczenia - tylko nienulowalne typy bezpośrednie implementujace interfejs `IComparabe<T>`
- metoda `Put` - metoda wstawia podany element do kolejki (na koniec, początek wg. uznania) - złożoność wywołania metody O(1)
- właściwość `Count` zwraca liczbę elementów znajdujących się w kolejce. Złożoność wywołania metody O(1)
- właściwość `Peek` zwraca element o najmniejszym priorytecie znajdujący się w kolejce. Złożoność wywołania metody O(1)
- metoda `Get` element o najmniejszym priorytecie znajdujący się w kolejce i usuwa go z kolejki.
  Złożoność wywołania metody O(n)
- jeśli wywołamy `Get` bądź `Peek` dla pustej kolejki zgłaszany jest błąd `InvalidOperationException`

-------------------------------------------------------------------------
Etap 2 (1pkt)
Zaimplementować interfejs `IEnumerable<T>` dla  
- `GetEnumerator` ma wyliczać wszystkie elementy kolejki

Uwaga: w testach do etapu 2 ważne jest poprawne wyliczenie wszystkich elementów
(kolejność może być inna)

-------------------------------------------------------------------------
Etap 3 (1pkt)
Stówrz klasę `PriorityQueueExtensions`. Klasa powinna się znajdować również w pliku `PriorityQueue`
W tejże klasie należy zaimplementować metody rozszerzające klasę `MinPriorityQueue<T>`
- metodę rozszerzającą `bool Exist(T)`
  Metoda zwraca `true` jeśli element znajduje się w kolejce, lub `false` jeśli nie
- metodę rozszerzającą `MaxItem` która zwraca maksymalny element w kolejce. 
  metoda ta nie usuwa elementu z kolejki (tak jak i `Peek`)