# Lab07:

Jesteś głównym inżynierem odpowiedzialnym za zarządzanie infrastrukturą serwerową w dużym centrum danych. Twoim zadaniem jest stworzenie systemu, który pozwoli na zarządzanie serwerami, śledzenie zmian w ich właściwościach oraz powiadamianie administratorów o wszelkich istotnych aktualizacjach.

## Etap01: Implementacja klasy `Server`.

Stwórz klasę `Server`, implementującą interfejsy `IAddressable` oraz `INotifyPropertyChanged`, zgodnie z następującą specyfikacją:

| Property | Type   |
| -------- | ------ |
| Address  | string |
| Name     | string |
| Status   | Status |
| Load     | double |

Wymagania:

- `Status` może przyjmować następujące wartości: `Failed` , `OverLoaded`, `Running` oraz `Stopped`.
- Klasa posiada konstruktor ustawiający wszystkie właściwości. Domyślnymi wartościami dla `Status` oraz `Load` są odpowiednio `Stopped` oraz `0.0`.
- Wartość `Address` może być ustawiona tylko i wyłącznie w ciele konstruktora.
- Zmiana wartości właściwości `Name`, `Load` oraz `Status` powoduje wywołanie zdarzenia narzuconego przez interfejs `INotifyPropertyChanged`.
- Wywołanie `ToString` zwraca łańcuch znaków postaci: `Name [Address]`.

W `Program.cs` dodaj do obiektu `server` funkcjonalność, która spowoduje, że zmiana pól obiektu wypisze na konsoli wiadomość w postaci: `[Address]: PropertyName => Value`.

### Punktacja:

- `1.5 Pts` - implementacja klasy `Server` oraz modyfikacja `Program.cs`.

## Etap02: Implementacja kolekcji powiadamiającej o zmianach.

Stwórz generyczny interfejs `INotifyingCollection<T>`, implementujący interfejs `IEnumerable<T>` oraz posiadający ograniczenie na typ `T` implementujący `IAddressable` oraz `INotifyPropertyChanged`. Interfejs udostępnia:

- Metodę `Add` wstawiającą element do kolekcji.
- Metodę `Remove` usuwającą element odpowiadający podanemu adresowi z kolekcji.
- Każda z metod zwraca informację o tym, czy operacja się powiodła.
- Zdarzenia `ElementAdded` oraz `ElementRemoved` typu `EventHandler<CollectionChangedEventArgs<T>>?`, gdzie `CollectionChangedEventArgs<T>` dziedziczy po `EventArgs` i posiada referencję do obiektu, który uczestniczył w operacji na kolekcji.
- Zdarzenie `ElementPropertyChanged` typu `EventHandler<ElementPropertyChangedEventArgs<T>`, które przechwytuje każdą zmianę właściwości elementu należącego do kolekcji. `ElementPropertyChangedEventArgs<T>` dziedziczy po `EventArgs` i zawiera informację o nazwie właściwości, której dotyczyła zmiana, a także referencję do samego elementu.

Stwórz klasę `ServerSystem` implementującą interfejs `INotifyingCollection<Server>`, która przechowuje obiekty klasy `Server` w prywatnej kolekcji typu `Dictionary<string, Server>`. Rolę unikalnego klucza pełni adres serwera.

Wymagania:

- Klasa posiada metodę `OnServerPropertyChanged`, która subskrybuje zdarzenia `PropertyChanged` każdego serwera podczas operacji dodawania do systemu oraz przestaje subskrybować te zdarzenia podczas usuwania serwerów z systemu.

W `Program.cs` dla obiektu `system` zasubskrybuj zdarzenia:

- `ElementAdded` - wypisuje w konsoli `Added [Element]`, a następnie ustawia status serwera na `Running` oraz losuje wartość `Load` z przedziału `[0, 50)`, używając obiektu `random`.
- `ElementRemoved` - wypisuje w konsoli `Removed [Element]`.
- `ElementPropertyChanged` - wypisuje w konsoli `[Address]: PropertyName => Value` dla każdej zmiany właściwości serwera należącego do systemu.

Następnie, dodaj wszystkie obiekty z kolekcji `servers` do systemu, używając metody `ForEach` oraz wyrażenia lambda.

### Punktacja:

- `1.0 Pts` - Stworzenie interfejsu `INotifyingCollection<T>`.
- `2.0 Pts` - Implementacja klasy `ServerSystem`.
- `0.5 Pts` - Modyfikacja `Program.cs`.

## Etap03: Rozszerzenie funkcjonalności systemu.

W klasie `Extensions` zaimplementuj następujące metody:

- `Shutdown`, która rozszerza `ServerSystem` i powoduje zmianę statusu na `Stopped` oraz obciążenia na `0.0` w każdym serwerze należącym do systemu.
- `AddLoadRule`, która rozszerza `ServerSystem` i przyjmuje dodatkowe parametry: `threshold` typu `double` oraz `rule` będący funkcją wywoływaną na obiekcie klasy `Server` i modyfikującą jego stan.
  - Jeżeli obciążenie dowolnego serwera w systemie przekroczy `threshold`, to na tym serwerze wykonywana jest operacja `rule`.
- `ClusterByStatus`, która po zgrupowaniu serwerów według statusu wykonuje operację `clusterOperation` na każdej grupie serwerów (przy określaniu typu parametru `clusterOperation` skorzystaj z generycznego interfejsu `IGrouping`).

W `Program.cs` dodaj regułę systemową, zgodnie z którą, jeżeli obciążenie serwera przekroczy wartość 75 i jego status nie jest `Overloaded`, to zostanie on zmieniony na `Overloaded`, a na konsoli zostanie wypisany komunikat: `Executing Rule75... on [server]`.

Dodaj operację na zgrupowanych serwerach, która wypisuje wspólny status grupy, a następnie wszystkie serwery, które do niej należą (format znajdziesz w pliku z oczekiwanymi rezultatami).

### Punktacja:

- `0.5 Pts` - Metoda `Shutdown`.
- `1.5 Pts` - Metoda `AddLoadRule` oraz modyfikacja `Program.cs`.
- `1.0 Pts` - Metoda `ClusterByStatus` oraz modyfikacja `Program.cs`.
