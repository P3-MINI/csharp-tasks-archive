# **Lab14: Synchronizacja**

## **Wstęp**

> _Kto z nas nie kocha pizzy? Ta włoska specjalność zdobyła serca na całym świecie, a jej popularność stawia przed pizzeriami nie lada wyzwania. Aby zaspokoić apetyt klientów, pizzerie muszą sprawnie realizować wiele zamówień równocześnie. Synchronizacja pracy jest tutaj kluczowa – w końcu nikt nie chce czekać zbyt długo na swój ulubiony placek! W dzisiejszym zadaniu stworzymy symulację funkcjonowania pizzerii, opartą na znanym problemie producenta i konsumenta._

<p align="center">
  <img src="../Common/img/pizzeria.jpg" alt="pizzeria.jpg" style="width: 80%;"/>
</p>

## **Etap 1: (2 pkt)**

> _Wyobraź sobie, że pizzeria ma specjalną kolejkę, w której gotowe do dostarczenia pizze czekają na kierowców. Stworzymy taką strukturę danych, aby działała wydajnie i bezpiecznie w środowisku wielowątkowym._

Twoim zadaniem jest stworzenie klasy `ParallelQueue<T>` oraz zaimplementowanie interfejsu `IParallelQueue<T>`, który znajdziesz w pliku `IParallelQueue.cs`.

Struktura danych ma pełnić rolę kolejki o maksymalnej pojemności określonej w konstruktorze. Kolejka powinna umożliwiać wstawianie i usuwanie elementów w sposób bezpieczny w środowisku wielowątkowym.

**Uwaga:** Do przechowywania elementów w kolejce nie korzystaj z gotowych klas, takich jak `ConcurrentQueue<T>` z przestrzeni nazw `System.Collections.Concurrent` ani `Queue<T>` z `System.Collections.Generic`.

### **Wskazówki**:

- Do synchronizacji operacji wstawiania i usuwania elementów wykorzystaj dwa [semafory](https://learn.microsoft.com/en-us/dotnet/api/system.threading.semaphoreslim?view=net-9.0&devlangs=csharp).
- Pamiętaj o odpowiednim [zwalnianiu](https://learn.microsoft.com/en-us/dotnet/api/system.idisposable?view=net-9.0) zasobów wykorzystywanych do synchronizacji.

## **Etap 2: (2 pkt)**

> _W każdej pizzerii mogą zdarzyć się przestoje – czasem brakuje składników, a czasem dostawcy mają chwilowe problemy z logistyką. Naszym zadaniem będzie umożliwienie tymczasowego wstrzymywania i wznawiania pracy kolejki._

Rozszerz implementację klasy `ParallelQueue<T>` o funkcjonalności zdefiniowane w interfejsie `IPausableQueue<T>` (plik `PausableQueue.cs`).

Metody zaczynające się od `Pause` powinny wstrzymywać możliwość wstawiania lub usuwania elementów przez wszystkie wątki korzystające z kolejki. Metody `Resume` pozwalają na wznowienie uprzednio wstrzymanych operacji. Nowo utworzona kolejka powinna domyślnie umożliwiać wykonywanie wszystkich operacji.

### **Wskazówki**:

- Do implementacji funkcjonalności wstrzymywania i wznawiania operacji wykorzystaj [ManualResetEventSlim](https://learn.microsoft.com/en-us/dotnet/api/system.threading.manualreseteventslim?view=net-9.0&devlangs=csharp).

## **Etap 3: (2 pkt)**

> _Zapach świeżo upieczonej pizzy już unosi się w powietrzu. Nadszedł czas, aby kucharze i dostawcy wzięli się do pracy!_

Twoim zadaniem jest rozbudowanie symulacji w klasie `Pizzeria`, aby odzwierciedlała pracę pizzerii. Metoda `StartSimulationAsync` została zaimplementowana i nie wymaga zmian.
W metodach `StartChefs` oraz `StartDeliverers` utwórz dwie grupy `Task`-ów, które będą reprezentować kucharzy przygotowujących pizzę oraz dostawców rozwożących zamówienia.

#### **Wątki kucharzy**:

- Liczba kucharzy: `ChefsCount`.
- Każdy kucharz w pętli generuje pizzę za pomocą metody `GeneratePizzaOrder()`.
- Po przygotowaniu pizzy kucharz czeka losową liczbę milisekund z zakresu `[1000, 2000]` (pieczenie ciasta wymaga czasu!).
- Gotową pizzę kucharz dodaje do kolejki za pomocą metody `TryEnqueueAsync`. Jeśli nie uda się tego zrobić w ciągu jednej sekundy, porzuca zamówienie (zimna pizza to złe doświadczenie dla klienta).

#### **Wątki dostawców**:

- Liczba dostawców: `DeliverersCount`.
- Dostawcy w pętli próbują pobrać pizzę z kolejki za pomocą metody `TryDequeueAsync`. Podobnie jak kucharze, czekają maksymalnie jedną sekundę.
- Jeśli uda się pobrać pizzę, dostawca "dostarcza" ją, czekając losową liczbę milisekund z zakresu `[1000, 2000]`.

## **Etap 4: (2 pkt)**

> _W pizzerii praca wre, ale każda zmiana ma swój koniec. Teraz zajmiemy się obsługą zmianowego systemu pracy oraz podsumowaniem dziennych wyników._

#### **Praca zmianowa**:

- Każdy pracownik pizzerii realizuje w ciągu dnia `_dailyPizzaOrders` zamówień.
- Po zakończeniu zmiany pracownik czeka na pozostałych, aby wspólnie podsumować dzienny przychód.
- Do synchronizacji oczekiwania użyj (oraz zmodyfikuj sposób tworzenia w konstruktorze) obiektu `_barrier` (będącego prywatnym polem klasy `Pizzeria`).
- Wszyscy pracownicy pizzerii, przed przystąpieniem do kolejnego dnia pracy, czekają na wciśnięcie klawisza `b` przez użytkownika sterującego symulacją (zobacz _Sterowanie symulacją_).
- Przechowuj informacje o przychodach w `_dailyIncomeDictionary`. Kluczem w słowniku może być nazwa pizzy, a wartością – uzyskany przychód.
- Po zakończeniu dnia przychody są wypisywane na konsolę.

#### **Kończenie symulacji**:

- Dodaj logikę przerywania pracy wszystkich pracowników pizzerii za pomocą `cancellationToken` (będącego parametrem wywołania metod `StartChefs` oraz `StartDeliverers`).

### **Wskazówki**:

- [ConcurrentDictionary<TKey,TValue> Class](https://learn.microsoft.com/en-us/dotnet/api/system.collections.concurrent.concurrentdictionary-2?view=net-9.0)
- [How to: Synchronize Concurrent Operations with a Barrier](https://learn.microsoft.com/en-us/dotnet/standard/threading/how-to-synchronize-concurrent-operations-with-a-barrier)

## **Sterowanie symulacją**

Symulacją można sterować za pomocą następujących klawiszy (logika odpowiadająca za sterowanie jest już gotowa w metodzie `StartSimulationAsync`):

```
Control:
  p - Pause pizza preparation
  r - Resume pizza preparation
  k - Pause delivery
  l - Resume delivery
  b - Sum up daily income
  q - Exit
```
