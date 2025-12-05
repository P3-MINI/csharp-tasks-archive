
using System;

class B
{
    public virtual void fun_v1() { Console.WriteLine("B_V1"); }
    public virtual void fun_v2() { Console.WriteLine("B_V2"); }
    public void fun_nv() { Console.WriteLine("B_NV"); }
}

class D : B
{
    public override void fun_v1() { Console.WriteLine("D_V1"); }
    public override void fun_v2() { Console.WriteLine("D_V2"); }
    public void fun_nv() { Console.WriteLine("D_NV"); }            // warning - it can be suppressed by using new modifier
}

class E : D
{
    public override void fun_v1() { Console.WriteLine("E_V1"); }
    public virtual void fun_v2() { Console.WriteLine("E_V2"); }    // warning - it can be suppressed by using new modifier
    public void fun_nv() { Console.WriteLine("E_NV"); }            // warning - it can be suppressed by using new modifier

    static void Main()
    {
        E e = new E();
        B b = new E();

        Console.WriteLine();
        e.fun_v1();     //  E_V1
        e.fun_v2();     //  E_V2
        e.fun_nv();     //  E_NV

        Console.WriteLine();
        b.fun_v1();     //  E_V1
        b.fun_v2();     //  D_V2
        b.fun_nv();     //  B_NV

        Console.WriteLine();
    }
}
