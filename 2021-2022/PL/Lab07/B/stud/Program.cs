using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab07B
{
    class Program
    {
        static void Main(string[] args)
        {
            //etap 1
            Console.WriteLine("Etap 1");
            BST b1 = new BST();
            Console.WriteLine(b1.IsEmpty);
            b1.Insert(1);
            b1.Insert(2);
            b1.Insert(3);
            Console.WriteLine(b1.IsEmpty);
            Console.WriteLine(b1.Count);
            Console.WriteLine("");

            //etap 2
            //Console.WriteLine("Etap 2");
            //Console.WriteLine(b1);
            //BST b2 = b1.Clone();
            //b1.Insert(4);
            //b2.Insert(5);
            //Console.WriteLine(b1);
            //Console.WriteLine(b2);
            //Console.WriteLine("");

            ////etap 3
            //Console.WriteLine("Etap 3");
            //BST b3 = b1 + b2;
            //BST b4 = b1 * b2;

            //Console.WriteLine(b1);
            //Console.WriteLine(b2);
            //Console.WriteLine(b3);
            //Console.WriteLine(b4);

            //BST b5 = new BST();
            //b5.Insert(1);
            //b5.Insert(2);
            //b5.Insert(3);
            //b5.Insert(4);

            //Console.WriteLine(b1 == b5);
            //Console.WriteLine(b1 != b5);

            //Console.WriteLine("");

            ////etap 4
            //Console.WriteLine("Etap 4");

            //(BST smaller, BST greater) = b3;
            //Console.WriteLine(smaller);
            //Console.WriteLine(greater);

            //Console.WriteLine(b3[0]);
            //Console.WriteLine(b3[1]);
        }
    }
}
