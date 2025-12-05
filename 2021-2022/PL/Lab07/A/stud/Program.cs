using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab07A
{
    class Program
    {
        static void Main(string[] args)
        {
            //etap 1
            Console.WriteLine("Etap 1");
            MyLinkedList l1 = new MyLinkedList();
            Console.WriteLine(l1.IsEmpty);
            l1.PushFront(1);
            l1.PushFront(2);
            l1.PushFront(3);
            Console.WriteLine(l1.IsEmpty);
            Console.WriteLine(l1.Count);
            Console.WriteLine("");

            ////etap 2
            //Console.WriteLine("Etap 2");
            //Console.WriteLine(l1);
            //MyLinkedList l2 = l1.Clone();
            //l1.PushFront(4);
            //l2.PushFront(5);
            //Console.WriteLine(l1);
            //Console.WriteLine(l2);
            //Console.WriteLine("");

            ////etap 3
            //Console.WriteLine("Etap 3");
            //MyLinkedList l3 = l1 + l2;
            //MyLinkedList l4 = l1 - l2;

            //Console.WriteLine(l1);
            //Console.WriteLine(l2);
            //Console.WriteLine(l3);
            //Console.WriteLine(l4);

            //MyLinkedList l5 = new MyLinkedList();
            //l5.PushFront(1);
            //l5.PushFront(2);
            //l5.PushFront(3);
            //l5.PushFront(4);

            //Console.WriteLine(l1 == l5);
            //Console.WriteLine(l1 != l5);

            //Console.WriteLine("");

            ////etap 4
            //Console.WriteLine("Etap 4");

            //(MyLinkedList even, MyLinkedList odd) = l3;
            //Console.WriteLine(even);
            //Console.WriteLine(odd);

            //Console.WriteLine(l3[2]);
            //l3[2] = 997;
            //Console.WriteLine(l3[2]);
            //Console.WriteLine(l3);


        }
    }
}
