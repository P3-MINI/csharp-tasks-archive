
using System;

class A
{

    void fun(object k)
    { Console.WriteLine("object"); }
    void fun(int k1, int k2)
    { Console.WriteLine("int, int"); }
    void fun(int k1 = 1, int k2 = 2, int k3 = 3)
    { Console.WriteLine("int=, int=, int="); }
    void fun(params int[] tab)
    { Console.WriteLine("params int"); }
    void fun(params object[] tab)
    { Console.WriteLine("params object"); }

    void fun1(object a)
    { Console.WriteLine("object"); }
    void fun1(int a)
    { Console.WriteLine("int"); }

    void fun2(dynamic a)
    { Console.WriteLine("dynamic"); }
    void fun2(int a)
    { Console.WriteLine("int"); }

    void fun3(in object a)
    { Console.WriteLine("object"); }
    void fun3(in int a)
    { Console.WriteLine("int"); }

    static void Main()
    {
        A a = new A();

        short[] ts = new short[3];
        short s1 = 11, s2 = 12, s3 = 13, s4 = 14;

        Console.WriteLine();
        a.fun(s1);           // result:  int=, int=, int=
        a.fun(s1, s2);        // result:  int, int
        a.fun(s1, s2, s3);     // result:  int=, int=, int=
        a.fun(s1, s2, s3, s4);  // result:  params int
        a.fun(ts);           // result:  object

        short s = 1;
        long l = 2;
        object o = s;
        dynamic d = s;

        Console.WriteLine();
        a.fun1(s);   // result:  int
        a.fun1(l);   // result:  object
        a.fun1(o);   // result:  object
        a.fun1(d);   // result:  int

        Console.WriteLine();
        a.fun2(s);   // result:  int
        a.fun2(l);   // result:  dynamic
        a.fun2(o);   // result:  dynanic
        a.fun2(d);   // result:  int

        Console.WriteLine();
        //a.fun3(in s);   // result:  compilation errror!
        //a.fun3(in l);   // result:  compilation errror!
        a.fun3(in o);     // result:  object
        a.fun3(in d);     // result:  object

        Console.WriteLine();
        a.fun3(s);                                // result:  int
        a.fun3(l);                                // result:  object
        a.fun3(o);                                // result:  object
        try
        { a.fun3(d); }
        catch (Exception)
        { Console.WriteLine("exception!"); }  // result:  exception!

        Console.WriteLine();
    }
}

