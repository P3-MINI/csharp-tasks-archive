# Lab15: Synchronizacja - OMEGALIDL

Trwa błyskawiczna wyprzedaż nowego "Lidlomix 3000". Tłumy gromadzą się przed sklepem od świtu. Aby zachować porządek, kierownictwo zdecydowało się wprowadzić rygorystyczne środki bezpieczeństwa.

Program przyjmuje trzy argumenty:
* `M` - liczba klientów (`20 <= M <= 100`)
* `N` - liczba pracowników uzupełniających towar (`3 <= N <= 12`, `N % 3 = 0`)
* `K` - liczba kasjerów (`2 <= K <= 5`)

Twoim zadaniem jest symulacja działania sklepu przy użyciu zadań (`Tasks`) i synchronizacji asynchronicznej.

### Etapy:

1. **[3p]** Utwórz `M` zadań klientów. Pojemność sklepu jest ograniczona do `8` klientów. Użyj `SemaphoreSlim` do kontrolowania dostępu. Po wejściu klient wypisuje: `CUSTOMER <ID>: Entered the shop`, czeka 200ms używając `Task.Delay`, a następnie opuszcza sklep (zwalniając semafor).

2. **[4p]** Utwórz `N` zadań pracowników do uzupełniania zapasów. Przeniesienie urządzenia wymaga zespołu 3 pracowników. Pracownicy pracują w stałych 3-osobowych zespołach w pętli, dopóki nie zostanie dostarczonych co najmniej `M` dostaw łącznie przez wszystkie zespoły. Użyj bariery do synchronizacji zespołów. Kiedy zbierze się 3 pracowników, wypisują: `TEAM <TEAM_ID>: Delivering stock`, czekają 400ms i zwiększają stan magazynu. Klienci czekają na towar, jeśli stan wynosi 0. Gdy towar jest dostępny, zmniejszają licznik i wypisują: `CUSTOMER <ID>: Picked up Lidlomix`. Możesz użyć semafora do zliczania towaru.

3. **[3p]** Zaimplementuj kolejkę do kasy. Zamiast natychmiastowego wyjścia, klienci muszą dołączyć do kolekcji blokującej FIFO o pojemności 5. Jeśli jest pełna, klient czeka. Utwórz `K` zadań kasjerów. Kasjer pobiera klienta z kolejki, wypisuje `CASHIER <CashID>: Serving Customer <CustID>` i czeka 300ms. Klient musi poczekać, aż kasjer skończy (możesz użyć np. `SemaphoreSlim`). Po zakończeniu klient wypisuje `CUSTOMER <CustID>: Paid and leaving`.

4. **[2p]** Obsłuż `SIGINT`. Wszystkie zadania muszą zostać powiadomione o zakończeniu poprzez `CancellationToken`. Upewnij się, że żadne zadanie nie zawiesiło się na żadnym prymitywie synchronizacyjnym (bariera, semafor lub kolekcja). Posprzątaj wszystkie zasoby.
