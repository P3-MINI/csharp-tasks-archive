# Zadanie laboratoryjne: Strumienie sieciowe, łącza i pliki mapowane w pamięci

**Rola:** Główny Inżynier Sieciowy (Lead Network Engineer)  
**Termin:** 2 godziny do premiery  
**Status:** Krytyczna porażka

## Sytuacja
Gratulacje! Objąłeś stanowisko głównego programisty sieciowego długo wyczekiwanego **Quack III Arena**. Oprawa wizualna zachwyca, fizyka działa bez zarzutu, a kaczki są zwarte i gotowe do walki.

Niestety, poprzedni programista (stażysta, który zdążył już "wyemigrować" do innej firmy) pozostawił kod sieciowy w opłakanym stanie. Klienci nie są w stanie nawiązać połączenia, wiadomości gubią się lub są błędnie przesyłane, a cała warstwa komunikacyjna jest niespójna, co skutkuje losowymi rozłączeniami i brakiem stabilności.

**Do premiery zostały 2 godziny.** Twoim zadaniem jest zaimplementowanie warstwy komunikacji sieciowej tak, aby klienci mogli stabilnie łączyć się z serwerem, przesyłać sterowanie i odbierać aktualizacje stanu gry w sposób niezawodny i wydajny.

## Protokół
Gra wykorzystuje niestandardowy protokół oparty na TCP, który łączy przesyłanie danych w formacie JSON oraz binarnym. Każda wiadomość przesyłana przez sieć musi być opatrzona ścisłym, 5-bajtowym nagłówkiem.

### Format wiadomości
```text
[  Długość ] [  Typ   ] [      Dane (Payload)      ]
| 4 Bajty  | | 1 Bajt | |      N Bajtów ...        |
|  Int32   | |  Enum  | |    Dane JSON/Binarne     |
```

1.  **Rozmiar Danych (4 bajty):** 32-bitowa liczba całkowita (`Int32`) określająca liczbę bajtów danych następujących po nagłówku.
2.  **Typ Wiadomości (1 bajt):** Bajt reprezentujący wartość wyliczeniową `MessageType` (np. `Join`, `Input`, `UpdateState`).
3.  **Dane (N bajtów):** Zserializowana treść wiadomości.

**Ważne:** Zarówno klient, jak i serwer muszą rygorystycznie przestrzegać tego formatu. Odczytanie zbyt małej lub zbyt dużej liczby bajtów doprowadzi do desynchronizacji połączenia i krytycznych błędów komunikacji.

## Twoja misja

W kodzie źródłowym (projekty `Quack.Messages` oraz `Quack.Client`) znajdziesz komentarze `// TODO`. Wykonaj poniższe etapy, aby uratować premierę gry.

**Uwaga:** Każdy etap można zweryfikować za pomocą zautomatyzowanych testów znajdujących się w projekcie `Quack.Tests`.

### Etap 1: Serializacja wiadomości (3 punkty)
**Projekt:** `Quack.Messages`  
**Pliki:** `JsonMessages.cs`, `BinaryMessages.cs`

> Aby komunikacja była możliwa, obiekty gry muszą zostać przekształcone w ciąg bajtów (serializacja), a odebrane bajty z powrotem w obiekty (deserializacja). Jest to podstawa wymiany danych w sieci.

*   **Zadania:**
    1.  **Wiadomości JSON (`JsonMessages.cs`):** Zaimplementuj metody `Serialize()` oraz `Deserialize()`.
    2.  **Wiadomości Binarne (`BinaryMessages.cs`):** Zaimplementuj metody `Serialize()` oraz `Deserialize()` dla niestandardowego formatu binarnego.

#### Specyfikacja formatu binarnego
Aby zapewnić kompatybilność z serwerem, musisz ściśle przestrzegać poniższego układu bajtów dla wiadomości binarnych:

**1. JoinMessage**
Wiadomość wysyłana w momencie dołączania gracza do areny.
*   **Rozmiar:** 4 bajty na Długość Nazwy + N bajtów na Dane Nazwy.
*   **Układ:**
    *   **Długość Nazwy (4 bajty):** Standardowy `Int32` reprezentujący liczbę bajtów (UTF8) w ciągu znaków `Name`.
    *   **Dane Nazwy (N bajtów):** Nazwa gracza (`Name`) zakodowana w formacie UTF8.

**2. InputMessage**
Wiadomość przesyłająca sterowanie gracza. Zaprojektowana z myślą o minimalnym zużyciu pasma.
*   **Rozmiar:** Dokładnie 1 bajt.
*   **Układ:** Pojedynczy bajt, w którym poszczególne bity odpowiadają stanom klawiszy:
    *   **Bit 0:** `Up` (true jeśli wciśnięty, w przeciwnym razie false)
    *   **Bit 1:** `Down` (true jeśli wciśnięty, w przeciwnym razie false)
    *   **Bit 2:** `Left` (true jeśli wciśnięty, w przeciwnym razie false)
    *   **Bit 3:** `Right` (true jeśli wciśnięty, w przeciwnym razie false)
    *   **Bit 4:** `Sprint` (true jeśli wciśnięty, w przeciwnym razie false)
    *   *Bity 5-7 pozostają nieużywane.*

### Etap 2: Połączenie klienta (2 punkty)
**Projekt:** `Quack.Client`  
**Plik:** `GameClient.cs`

> Gdy mechanizm konwersji wiadomości jest gotowy, kolejnym kluczowym krokiem jest nawiązanie połączenia z serwerem. Bez tego Twoja kaczka pozostanie wiecznie samotna.

*   **Zadanie:** Zaimplementuj metodę `ConnectAsync`.
*   **Wymagania:**
    1.  Rozwiąż hosta (np. `localhost`, `192.168.1.111`, `pw.mini.edu.pl`) na adres IP. Jeżeli jest to już adres IP, to go po prostu sparsuj.
    2.  Nawiąż połączenie ze wskazanym adresem IP i portem serwera.

### Etap 3: Pętla odczytu (4 punkty)
**Projekt:** `Quack.Client`  
**Plik:** `NetworkConnection.cs`

> Po nawiązaniu połączenia klient musi nieprzerwanie nasłuchiwać aktualizacji stanu gry z serwera, ściśle trzymając się zdefiniowanego protokołu. Błąd na tym etapie doprowadzi do desynchronizacji i zdezorientowania kaczek.

*   **Zadanie:** Zaimplementuj metodę `StartReadingAsync`.
*   **Wymagania:**
    1.  Odczytuj wiadomości w nieskończonej pętli ze strumienia `NetworkStream` tak długo, jak klient pozostaje połączony.
    2.  **Odczyt Nagłówka:** Pobierz dokładnie 5 bajtów ze strumienia. Wyodrębnij z nich 4-bajtową długość danych (`PayloadLength`) oraz 1-bajtowy typ wiadomości (`MessageType`).
    3.  **Odczyt Danych:** Pobierz dokładnie tyle bajtów, ile wskazuje `PayloadLength`, zapisując je do bufora.
    4.  **Deserializacja:** Przekształć pobrane bajty na obiekt `IJsonMessage` (wykorzystując logikę z Etapu 1).
    5.  **Zdarzenie:** Wywołaj zdarzenie `MessageReceived`, przekazując zdeserializowaną wiadomość.

### Etap 4: Wysyłanie wiadomości (3 punkty)
**Projekt:** `Quack.Client`  
**Plik:** `NetworkConnection.cs`

> Ostatni krok to umożliwienie klientowi komunikacji zwrotnej.

*   **Zadanie:** Zaimplementuj metodę `SendAsync`.
*   **Wymagania:**
    1.  **Serializacja Wiadomości:** Przekształć obiekt `IMessage` na jego reprezentację binarną (używając logiki z Etapu 1).
    2.  **Konstrukcja Nagłówka:** Utwórz 5-bajtowy nagłówek zawierający długość danych (`PayloadLength` – na podstawie zserializowanej wiadomości) oraz typ wiadomości (`MessageType`).
    3.  **Wysłanie Danych:** Zapisz do strumienia `NetworkStream` 5-bajtowy nagłówek, a bezpośrednio po nim zserializowaną treść wiadomości.
*   **Wskazówka:** Rozważ użycie `ArrayPool` do wydajnego zarządzania tymczasowymi buforami pamięci.
