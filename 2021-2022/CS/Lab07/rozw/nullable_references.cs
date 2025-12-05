
#nullable enable

using System;

class Program
    {

    static void fun_nn(string s)
        {
        Console.WriteLine($"Do something with string: {s}");
        }

    static void fun_n(string? s)
        {
        Console.WriteLine($"Do something with string: {s}");
        }

    static void Main()
        {

        string snn1 = "non-empty non-nullable string";    // OK
        string snn2 = null;                               // warning - null assigned to non-nullable string variable

        string? sn1 = "non-empty nullable string";        // OK
        string? sn2 = null;                               // OK - null assigned to nullable string variable

        Console.WriteLine("----------");
        Console.WriteLine(snn1);                          // OK
        Console.WriteLine(snn2);                          // no warnings because WriteLine method can handle null value
        Console.WriteLine(sn1);                           // OK
        Console.WriteLine(sn2);                           // no warnings because WriteLine method can handle null value
        Console.WriteLine("----------");
        fun_nn(snn1);                                     // OK
        fun_nn(snn2);                                     // warning - parameter shouldn't be null
        fun_nn(sn1);                                      // OK
        fun_nn(sn2);                                      // warning - parameter shouldn't be null
        Console.WriteLine("----------");
        fun_n(snn1);                                      // OK
        fun_n(snn2);                                      // OK -- parameter can be null
        fun_n(sn1);                                       // OK
        fun_n(sn2);                                       // OK -- parameter can be null
        Console.WriteLine("----------");
        if ( snn2!=null )
            fun_nn(snn2);                                 // no warning because compiler finds that method will be invoked only if parameter isn't null
        if ( sn2!=null )
            fun_nn(sn2);                                  // no warning because compiler finds that method will be invoked only if parameter isn't null
        Console.WriteLine("----------");
        fun_nn(snn2!);                                    // no warning because of using ! null-forgiving operator
        fun_nn(sn2!);                                     // no warning because of using ! null-forgiving operator
        Console.WriteLine("----------");
        }

    }
