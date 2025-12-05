#define STAGE_1
#define STAGE_2
#define STAGE_3
using System.Diagnostics.Metrics;
using System.Text;
using System.Xml.Linq;

namespace Lab06_PL
{
    class Program
    {
        private static void PrintStage(int stage)
        {
            Console.WriteLine($"\n----------------------Stage {stage}-------------------------\n");
        }

        #if STAGE_1
        public static void PrintSet(string name, Set set)
        {
            Console.WriteLine($"{name}: {set}, capacity {set.GetElementsArrayCapacity()}");
        }
        #endif

        public static void Main(string[] args)
        {
            #if STAGE_1
            PrintStage(1);
            Console.WriteLine("//Tworzenie obiektów klasy Set, wypisywanie");
            Set setA = new Set(1,2,3,4,5,5,1);
            Set setB = new Set(2,3,4,4,3,5,6,7,7,4,8,9);
            PrintSet("A", setA);
            PrintSet("B", setB);
            #endif
            #if STAGE_2
            PrintStage(2);
            Console.WriteLine("//Suma zbiorów");
            var setSUM = setA + setB;
            PrintSet("A \u22c3 B", setSUM);

            Console.WriteLine("\n//Różnica zbiorów");
            var setDIFFAB = setA - setB;
            var setDIFFBA = setB - setA;

            PrintSet("A \\ B", setDIFFAB);
            PrintSet("B \\ A", setDIFFBA);
            #endif
            #if STAGE_3
            PrintStage(3);
            Set emptySet = new Set();
            PrintSet("Pusty zbiór", emptySet);
            if(emptySet && setA)
                Console.WriteLine("BŁĘDNY WYNIK!");
            else Console.WriteLine("Dobrze! Tylko jeden ze zbiorów jest niepusty.");
            if(setA && setB)
                Console.WriteLine("Dobrze! Oba zbiory są niepuste.");
            else Console.WriteLine("BŁĘDNY WYNIK!");
            if(emptySet || setB)
                Console.WriteLine("Dobrze! Jeden ze zbiorów jest pusty.");
            else Console.WriteLine("BŁĘDNY WYNIK!");

            Console.WriteLine("\n//Część wspólna zbiorów");
            var setIntAB = setA & setB;
            PrintSet("A \u22c2 B", setIntAB);

            Console.WriteLine("\n//Porównanie zbiorów");
            if(setA !=  setB) 
                Console.WriteLine("setA != setB");
            Set setADifferentButSame = new Set(4,5,2,1,1,3);
            PrintSet("setADifferentButSame", setADifferentButSame);

            if(setA == setADifferentButSame)
                Console.WriteLine("setA == setADifferentButSame");
            #endif
        }
    }
}
