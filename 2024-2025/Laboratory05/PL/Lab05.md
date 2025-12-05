# Programowanie 3 - Zaawansowane

## Laboratorium 5 - Yield, Interfejsy, Generyki, IEnumerable

### Etap01 (1 punkt)

Stwórz generyczny interfejs `IMyCollection<T>`, który rozszerza interfejs `IEnumerable<T>`.
Powinien on mieć dostępną tylko do odczytu właściwość `Count` typu `int` oraz metodę `Add`, która pozwala na dodanie nowego obiektu do kolekcji.

### Etap02 (3 punkty)

Stwórz generyczną klasę `MyCircularBuffer`, która implementuje utworzony interfejs `IMyCollection`.
Dane przechowywane są w tablicy o rozmiarze zdefiniowanym jako jedyny parametr konstruktora tej klasy.
Bufor cykliczny (ang. Circular Buffer) to struktura danych, która przechowuje dane w sposób cykliczny, nadpisując najstarsze, gdy osiągnie maksymalny rozmiar.
Można ją zaimplementować za pomocą dwóch indeksów: jeden wskazuje początek (najstarsze dane), a drugi miejsce, gdzie można dodać nowe dane.

- Jeśli bufor jest pełny to enumerator zwraca nieskończony ciąg elementów, w kolejności ich wstawiania.
  Jeśli nie jest pełny to enumerator zwraca tylko tyle elementów ile jest w buforze, w kolejności ich wstawiania.
- Metoda `GetItems` zwraca zawsze enumerator elementów bufora, w kolejności ich wstawiania.
- Właściwość `IsFull` zwraca `True` jeśli bufor jest pełny, wpp. `False`.
- Właściwość `IsEmpty` zwraca `True` jeśli bufor jest pusty, wpp. `False`.

### Etap03 (4 punkty)

Stwórz generyczną klasę `MySortedLinkedList`, która przechowuje w węzłach posortowane wartości danego typu. Użyj interfejsu `IComparable` aby być pewnym, że wartości mogą być porównywalne.  
Wskazówka: Stwórz odpowiednią klasę zagnieżdżoną dla węzłów listy.
- Metoda `Add`, która dodaje dany element do listy.
- Właściwość `Count` (tylko do odczytu), która zwraca liczbę elementów listy.
- Metoda `Contains`, która sprawdza czy dany element znajduje się na liście.
- Metoda `PopFront`, która zwraca pierwszy (najmniejszy) element z listy i usuwa go.
  Jeśli lista jest pusta rzuć wyjątek `IndexOutOfRangeException`.
- Dodaj możliwość enumeracji obiektów listy.