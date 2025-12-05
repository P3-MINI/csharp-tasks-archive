## Group a

//PW: There are inbuilt delegates - no need to create it by yourself
// Action<T> == public delegate void func<T>(T a);
// Func<Tout> == public delegate T func<T>();
// Predicate<T> public delegate bool pred<T>(T a);
// Func<Tin, Tout> public delegate T1 func<T, T1>(T a);

// PW:  Accumulate -> It lacks default addition for int parameter
// PW: FindFirstIfOrDefault -> Not implemented situation to return first element when Predicate was not provided

// Etap 1 (1p)
// - void Foreach<T> -> arg Action<T> func)
// - void Print<T> -> a1;a2;a3; using (Foreach)
// - IEnumerable<T> GenerateN<T>(int count, , Func<T> func):
//   result_1a, result_1b, result_1c

// Etap 2 (1p)
// - IEnumerable<T> Where<T> -> arg Predicate<T> pred
// - IEnumerable<Tout> Transform<Tin, Tout> -> args Func<Tin, Tout> func
//   Main -> result_2a, result_2b

// Etap 3 (1.5p)
//  - T2 Accumulate<Tin,Tout>(this IEnumerable<Tin> source, Tout initValue, Func<Tout, Tin, Tout> func) => suma
//  - domyślnie funkcja dodawania - dla int
//   int Accumulate(this IEnumerable<int> source, int initValue, Func<int, int, int> func = null)
// - T FindFirstIfOrDefault<T>(this IEnumerable<T> source, Predicate<T> pred = null)
//  - T[] ToArray<T>(this IEnumerable<T> source)
//  - Main -> result_3b, result_3c

// Etap 4 (1.5p)
//  - IEnumerable<T> Unique<T>(this IEnumerable<T> source, Func<T, T, bool> func = null) where T: IComparable<T> -> tylko sąsiadujące -> O(n)
// - Func<T,T> MinFunc<T>(params Func<T, T>[] func) where T : IComparable<T>
//   x-> min(fi(x)), gdzie i=0..n
//   default x->x
// - Main -> result_4a, result_4b

## Group b

//PW: There are inbuilt delegates - no need to create it by yourself
// Action<T> == public delegate void func<T>(T a);
// Func<Tout> == public delegate T func<T>();
// Predicate<T> public delegate bool pred<T>(T a);
// Func<Tin, Tout> public delegate T1 func<T, T1>(T a);

// PW:  Accumulate -> It lacks default addition for int parameter
// PW: FindFirstIfOrDefault -> Not implemented situation to return first element when Predicate was not provided

// Etap 1 (1p)
// - void Foreach<T> -> arg Action<T> func)
// - void Print<T> -> a1;a2;a3; using (Foreach)
// - IEnumerable<T> GenerateN<T>(int count, , Func<T> func):
//   result_1a, result_1b, result_1c

// Etap 2 (1p)
// - IEnumerable<T> RemoveIf<T> -> arg Predicate<T> pred
// - IEnumerable<Tout> Transform<Tin, Tout> -> args Func<Tin, Tout> func
//   Main -> result_2a, result_2b

// Etap 3 (1.5p)
//  - T2 Accumulate<Tin,Tout>(this IEnumerable<Tin> source, Tout initValue, Func<Tout, Tin, Tout> func) => suma
//  - domyślnie funkcja dodawania - dla int
//   double Accumulate(this IEnumerable<double> source, double initValue, Func<double, double, double> func = null)
// - T FindEndIfOrDefault<T>(this IEnumerable<T> source, Predicate<T> pred = null) => last for pred == null
//  - T[] ToArray<T>(this IEnumerable<T> source)
//  - Main -> result_3b, result_3c

// Etap 4 (1.5p)
//  - IEnumerable<T> Unique<T>(this IEnumerable<T> source, Func<T, T, bool> func = null) where T : IComparable<T> -> tylko sąsiadujące -> O(n)
// - Func<T,T> MaxFunc<T>(params Func<T, T>[] func) where T : IComparable<T>
//   x-> max(fi(x)), gdzie i=0..n
//   default x->x
// - Main -> result_4a, result_4b