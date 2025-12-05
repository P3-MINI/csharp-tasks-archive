
using System;

namespace DynamicTest
{

    class C
    {
        public int p;

        public C(int pp) { p = pp; }

        public void fun()
        {
            Console.WriteLine("Invocation of C class method");
        }
    }

    struct S
    {
        public int p;

        public S(int pp) { p = pp; }

        public void fun()
        {
            Console.WriteLine("Invocation of S struct method");
        }
    }

    class Test
    {

        public static void Main()
        {
            object oc, os;
            dynamic dc, ds;
            C c1, c2;
            S s1, s2;

            c1 = new C(1);
            s1 = new S(1);

            /*----------------------------*/

            oc = c1;
            os = s1;

            dc = c1;
            ds = s1;

            /*----------------------------*/

            //      c2 = oc;        // compilation error - implicit conversion is impossible
            //      s2 = oc;        // compilation error - implicit conversion is impossible
            c2 = (C)oc;     // correct           - explicit conversion
            s2 = (S)os;     // correct           - explicit conversion

            c2 = dc;        // correct - decision is delayed to runtime (and then it is clear that covnersion isn't needed)
            s2 = ds;        // correct - decision is delayed to runtime (and then it is clear that covnersion isn't needed)

            /*----------------------------*/

            //      oc.fun();       // compilation error - only methods of object class can be invoked
            //      os.fun();       // compilation error - only methods of object class can be invoked
            ((C)oc).fun();  // correct           - explicit conversion
            ((S)os).fun();  // correct           - explicit conversion

            dc.fun();       // correct - decision is delayed to runtime (and then it is clear that method can be invoked)
            ds.fun();       // correct - decision is delayed to runtime (and then it is clear that method can be invoked)

            /*----------------------------*/

            //      oc.p=5;         // compilation error - only fields of object class can be referred to
            //      os.p=5;         // compilation error - only fields of object class can be referred to
            ((C)oc).p = 5;    // correct           - explicit conversion (reference conversion)
                              //      ((S)os).p=5;    // compilation error - result of unboxing isn't l-value

            dc.p = 7;         // correct - decision is delayed to runtime (and then it is clear that field can be referred to)
            ds.p = 7;         // correct - decision is delayed to runtime (and then it is clear that field can be referred to)

            /*----------------------------*/

            Console.WriteLine("{0}  {1}  {2}", c1.p, ((C)oc).p, dc.p);
            Console.WriteLine("{0}  {1}  {2}", s1.p, ((S)os).p, ds.p);

            c1.p = 3;
            s1.p = 3;
            Console.WriteLine("{0}  {1}  {2}", c1.p, ((C)oc).p, dc.p);
            Console.WriteLine("{0}  {1}  {2}", s1.p, ((S)os).p, ds.p);

            /*----------------------------*/

            double x;
            int n;
            dynamic dn, dx;

            dx = 3.5;      // double type value
            dn = 4;        // int type value

            x = dx + 1.2;  // correct - in runtime it turns out that covnersion isn't needed
            n = dn + 2;    // correct - in runtime it turns out that covnersion isn't needed

            x = dn;        // correct - in runtime it turns out that covnersion from int to double is needed (and this conversion is implicit)
                           //      n = dx;        // compilation correct, exception in runtime
                           //         - in runtime it turns out that covnersion from double to int is needed (but this conversion must be explicit)
            n = (int)dx;   // correct - explicit conversion

            /*----------------------------*/

            dynamic dd, dd2;
            int i = 1;
            string s = "Peekaboo";
            bool b = false;
            dd = i;
            Console.WriteLine("\n !!!   Exception will be thrown   !!!\n");
            try
            {
                dd2 = dd / s - b;   //  compilation will be succesful !!!
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);  //  Operator '/' cannot be applied to operands of type 'int' and 'string'
            }

        }

    }

}
