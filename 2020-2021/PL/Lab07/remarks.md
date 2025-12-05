//PW: This is shallow copy. Reference to tab will be the same in both objects
//PW: What with negative indexes
//PW: For odd length first vector should be longer than second
//PW: Good - but even without this check IndexOutOfRangeException would be raised -> this is default exception

// PW: If constructor doesn't make a copy of `tab` this would be shallow copy
// Also there is no need to run logic to remove duplicates while we know that they are unique

//PW: Here is returned reference to internal array.
//It's possible that somebody will change value in array(e.g. to non unique) and UniqueVector will also change
// Preferable copy of array should be returned

//PW: base implementation is inefficient (can use Reflection)  and sometimes
// gives you bad results for ValueTypes example below
// where only first field is compared
//struct Number
//{
//    public string a;
//    public string b;

//    public Number(string a, string b)
//    {
//        this.a = a;
//        this.b = b;
//    }
//}
//var a = new Number("1", "0").GetHashCode();
//var b = new Number("1", "1").GetHashCode();

 //PW: While splitting unique set we are sure that they are also unique
// there is no need to run constructor uniqueness logic
//PW: No need to run uniqueness logic again, while tab is already unique. Why not:
//eSet = new UniqueSet
//{
//    tab = even
//};

// PW:
// https://docs.microsoft.com/en-us/dotnet/api/system.object.gethashcode?view=net-5.0#notes-to-inheritors
// For classes derived from Object, the GetHashCode method can delegate to the base class GetHashCode()
// implementation only if the derived class defines equality to be reference equality
// This is not true in our case



// PW:
// Etap 1 (2.0p)
// Constructors, Property(readonly) Size, Clone(deep), Deconstruct, Indexer
// Etap 2 (1.5p)
// ==, int[]->UniqueSet, UniqueSet->int[], int -> UniqueVector
// Etap 3 (1.5p)
// +, ^(disjunctive union), --
// TOTAL: 0pkt