# Remarks during verification

## Group a




//Etap 1(3pkt)
//- implementacja interfejsu z ograniczeniami na T -> `MinPriorityQueue<T> : IPriorityQueue<T> where T: struct, IComparable <T>`
//-metoda `Put` O(1)
//- właściwość `Count` O(1)
//- właściwość `Peek` min, O(n)
//- metoda `Get` min + usunięcie, O(n)
//- `Get` i `Peek` dla pustych `InvalidOperationException`
//-------------------------------------------------------------------------
//Etap 2 (1pkt)
//-impelementacja interfejsu `IEnumerable <T>` -wszystkie elementy
//------------------------------------------------------------------------ -
//Etap 3(1pkt)
//- `PriorityQueueExtensions`
//-metoda rozszerzająca `bool Exist(T)`
//-metodę rozszerzającą `MaxItem` bez usuwania


## Group b

//PW: It would be better to implement IComparable<T> interface, It would avoid boxing to object
// PW: fields should always be private it's anti pattern to have public fields. Instead use properties to expose values

//PW: It would be beneficial to have in constructor parameter, `Node<T> next = null`
//PW: Default constructor with default values not needed. It will work without it

//-------------------------------------------------------------------------
//Etap 1(3pkt)
//- zaimplementować `MaxPriorityQueue<T>: IPriorityQueue<T> where T: struct, IComparable <T>`
//-metoda `Put` -maksymalny element pierwszy O(n)
//-właściwość `Count` -złożoność O(1)
//- właściwość / metoda `Peek` return maks O(1)
//- metoda `Get` return +remove maks O(1)
//- `Get`+`Peek` dla pustej `InvalidOperationException`
//-------------------------------------------------------------------------
//Etap 2 (1pkt)
//- `IEnumerable < T >` wszystkie elementy
//-------------------------------------------------------------------------
//Etap 3 (1pkt)
//-metoda rozszerzająca `bool Exist(T)`
//-metodę rozszerzającą `MinItem` bez usuwania