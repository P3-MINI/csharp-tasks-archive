using System;
using System.Collections.Generic;

namespace Lab10b
{
    class Student
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public double GradeAverage { get; set; }

        public override string ToString()
        {
            return $"[{Name};{Surname};{Age};{GradeAverage:N1}]";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = new int[] { 10, 5, -4, 3, 25, -23, 17, 0, -5, 1, -7 };
            Student[] students = new Student[]
            {
                new Student() { Name = "Jan", Surname = "Kowalski", Age = 21, GradeAverage = 5.0 },
                new Student() { Name = "Roman", Surname = "Wiśniewski", Age = 25, GradeAverage = 3.0 },
                new Student() { Name = "Anna", Surname = "Nowak", Age = 23, GradeAverage = 4.3 },
                new Student() { Name = "Katarzyna", Surname = "Malanowska", Age = 19, GradeAverage = 3.5 },
                new Student() { Name = "Albert", Surname = "Rydz", Age = 22, GradeAverage = 4.3 },
                new Student() { Name = "Jolanta", Surname = "Rogala", Age = 19, GradeAverage = 5.0 },
            };
            double[] real_numbers = new double[] { 1.5, 2.3, -4.1, 3.0, 0.9, -1.1, 2.5, 0, -5.0, 1.2, -4.5 };
            int[] sorted_numbers = new int[] { -10, -8, -5, -5, -5, -2, 1, 1, 3, 4, 6, 8, 8, 11, 13, 19 };

            Console.WriteLine("//---------------------Etap1(1p)---------------------//");

            IEnumerable<int> result_1a = null;
            IEnumerable<int> result_1b = null;
            IEnumerable<double> result_1c = null;
            Random rand = new Random(1234);

            //TODO: Wygenerować 12 liczb parzystych rozpoczynając od 2
            int x_1a = 0;
            result_1a = EnumerableExtender.GenerateN(12, () => { x_1a += 2; return x_1a; });

            //TODO: Wygenerować 9 losowych liczb całkowitych z przedziału [-5; 15). Wykorzystać zmienną rand
            result_1b = EnumerableExtender.GenerateN(9, () => rand.Next(-5, 15));

            //TODO: Wygenerować 6 losowych liczb rzeczywistych z przedziału [-10; 10). Wykorzystać zmienną rand
            result_1c = EnumerableExtender.GenerateN(6, () => 20 * rand.NextDouble() - 10);

            result_1a?.Print();
            result_1b?.Print();
            result_1c?.Print();

            Console.WriteLine("\n//---------------------Etap2(1p)---------------------//");
            

            IEnumerable<int> result_2a = null;
            IEnumerable<string> result_2b = null;

            //TODO: do zmiennej result_2a zapisać część całkowitą pierwiastka wartości bezwzględnej (sqrt(|x|) liczb nieparzystych. Wykorzystać dane ze zmiennej numbers  
            result_2a = numbers.RemoveIf(x => x % 2 == 0).Transform(x => (int)Math.Sqrt(Math.Abs(x)));

            //TODO: do zmiennej result_2b przypisać inicjały studentek. Wykorzystać dane ze zmiennej students. Podpowiedź: kobiece imie kończy się literą "a"
            result_2b = students.RemoveIf(x => x.Name[x.Name.Length-1] != 'a').Transform(x => $"{x.Name[0]}{x.Surname[0]}");

            result_2a?.Print();
            result_2b?.Print();

            Console.WriteLine("\n//---------------------Etap3(1.5p)---------------------//");

            double result_3a = real_numbers.Accumulate(0);
            Console.WriteLine($"Suma elementów ciągu: {result_3a:N1}");

            //TODO: Znaleźć ostatnią liczbę całkowitą. Wykorzystać dane ze zmiennej real_numbers
            double result_3b = real_numbers.FindLastIfOrDefault(x => Math.Round(x) - x == 0.0);

            Console.WriteLine($"Ostatnia liczba całkowita: {result_3b:N1}");

            //TODO: Wyznaczyć średnią ocen wszystkich osób urodzonych przed 2000. Wykorzystać dane ze zmiennej students
            Student[] temp_3c = students.RemoveIf(x => 20 - x.Age >= 0).ToArray();
            double result_3c = temp_3c.Length == 0 ? 0.0 : temp_3c.Accumulate(0.0, (sum, st) => sum + st.GradeAverage) / temp_3c.Length;
            Console.WriteLine($"Średnia ocena studentów urodzonych przed 2000: {result_3c:N2}");

            Console.WriteLine("\n//---------------------Etap4(1.5p)---------------------//");

            IEnumerable<int> result_4a = null;
            IEnumerable<int> result_4b = null;

            //TODO: Zmodyfikować ciąg sorted_number w taki sposób, aby nie występowały obok siebie liczby parzyste
            // (np. mając zbiór 1,1,3,4,6,8,8,11,13,19 należy po liczbie 4 usunąć ciąg liczb parzystych dochodząc do liczby 11). Wynik zapisać do zmiennej result_4a 
            result_4a = sorted_numbers.Unique((x, y) => x % 2 == 0 && y % 2 == 0);

            //TODO: Dla ciągu sorted_number zwrócić ciąg największych wartości spośród funkcji: x->-x, x->10, x->-x^2+10*x, x->|x|. Wynik zapisać do zmiennej result_4b
            result_4b = sorted_numbers.Transform(EnumerableExtender.MaxFunc<int>(x => -x, x => 10, x => -x * x + 10 * x, x => x > 0 ? x : -x));

            Func<int,int> result_4c = EnumerableExtender.MaxFunc<int>();

            result_4a?.Print();
            result_4b?.Print();

            Console.WriteLine($"Funkcja x->x: 10->{result_4c(10)}");
        }
    }
}
