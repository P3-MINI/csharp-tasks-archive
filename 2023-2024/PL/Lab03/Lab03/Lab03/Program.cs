#define STAGE_1
#define STAGE_2
#define STAGE_3
#define STAGE_4

// Opisać jak działają #if etc i zaznaczyć, że mozemy tego używać podczas semestru.

using System;

namespace Lab03
{
    internal class Program
    {
        private static void Print(object message)
        {
#if DEBUG
            Console.WriteLine(message);
#endif
        }

        private static void Main(string[] args)
        {
#if STAGE_1
            // Zwracamy uwagę jak przekazywana jest struktura - przez wartość.
            // Żeby tak nie było, można dodać słowo ref, wtedy mamy referencję.
            Point2 point = new Point2(3.0f, 1.0f);
            Print(point);
            FunctionWithStruct(point);
            Print(point);
            FunctionWithStruct(ref point);
            Print(point);
#endif
#if STAGE_2
            // Zwracamy uwagę jak przekazywana jest klasa - przez referencję.
            Figure rectangle = new Rectangle(point, 3.0f, 2.0f);
            Print(rectangle);
            FunctionWithClass(rectangle as Rectangle);
            Print(rectangle);
#endif
#if STAGE_3
            // Opisać jeszcze raz switch pattern matching.
            // Zaznaczyć, że when nie koniecznie musi być związane ze zmienną z case.
            // Oraz to, że to continue tyczy się pętli i tylko wtedy jest poprawne.
            for (int i = 0; i < 3; i++)
                switch (rectangle)
                {
                    case Rectangle: Console.WriteLine("Switch Rectangle"); break;
                    case Circle c when c.Radius > 5.0f: Console.WriteLine("Switch Circle > 5.0f"); break;
                    case Circle c when c.Radius < 10.0f: Console.WriteLine("Switch Circle < 10.0f"); break;
                    case var f when i < 2: Console.WriteLine("Switch Figure"); continue;
                }
#endif
#if STAGE_4
            // Jakieś proste rzeczy na tablicach, można pokazać na przykład Aggregate 
            int[] array_1 = { 1, 2, 3, 4 };

            foreach (int i in array_1)
                Console.Write(i);

            int[,] array_2 = new int[3, 4];

            for (int i = 0; i < array_2.GetLength(0); i++)
                for (int k = 0; k < array_2.GetLength(1); k++)
                {
                    array_2[i, k] = array_1[k];
                }

            // Tutaj zwrócić uwagę, że musimy mieć wystarczająco miejsca.
            // int[] array_3 = new int[1]; Array.Copy(array_1, array_3, array_1.Length);
#endif
        }

        private static void FunctionWithClass(Rectangle rectangle)
        {
            rectangle.Scale(2.0f, 2.0f);
            rectangle.Move(new Point2(1.0f, 2.0f));
        }

        private static void FunctionWithStruct(Point2 point)
        {
            point.X += 2.0f;
        }

        private static void FunctionWithStruct(ref Point2 point)
        {
            point.X += 2.0f;
        }
    }
}
