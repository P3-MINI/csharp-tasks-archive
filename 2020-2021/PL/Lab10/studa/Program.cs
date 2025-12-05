using System;
using System.Collections.Generic;

namespace Lab10a
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
            int[] sorted_numbers = new int[] { -10, -8, -5, -5, -5, -2, 1, 1, 3, 4, 6, 8, 8, 11, 13, 19 };

            Console.WriteLine("//---------------------Etap1(1p)---------------------//");

            IEnumerable<int> result_1a = null;
            IEnumerable<int> result_1b = null;
            IEnumerable<double> result_1c = null;
            Random rand = new Random(1234);

            //TODO: Wygenerować pierwsze 10 liczb naturalnych
            result_1a = null; //Uzupełnić

            //TODO: Wygenerować 10 losowych liczb całkowitych z przedziału [-10; 10). Wykorzystać zmienną rand
            result_1b = null; //Uzupełnić

            //TODO: Wygenerować 5 losowych liczb rzeczywistych z przedziału [-1; 1). Wykorzystać zmienną rand
            result_1c = null; //Uzupełnić

            result_1a?.Print();
            result_1b?.Print();
            result_1c?.Print();

            Console.WriteLine("\n//---------------------Etap2(1p)---------------------//");

            IEnumerable<int> result_2a = null;
            IEnumerable<string> result_2b = null;

            //TODO: Do zmiennej result_2a zapisać wszystkie liczby nieujemne podniesione do kwadratu. Wykorzystać dane ze zmiennej numbers  
            result_2a = null; //Uzupełnić

            //TODO: Do zmiennej result_2b przypisać imiona studentów urodzonych przed 2000 rokiem ze średnią co najmniej 4.2. Wykorzystać dane ze zmiennej students
            result_2b = null; //Uzupełnić

            result_2a?.Print();
            result_2b?.Print();

            Console.WriteLine("\n//---------------------Etap3(1.5p)---------------------//");

            int result_3a = numbers.Accumulate(0);
            Console.WriteLine($"Suma elementów ciągu: {result_3a}");

            //TODO: Znaleźć pierwszego studenta urodzonego po 2002 roku. Wykorzystać dane ze zmiennej students
            Student result_3b = null; //Uzupełnić

            Console.WriteLine($"Czy jest student urodzony po 2002 roku: {result_3b != null}");

            //TODO: Wyznaczyć średni wiek studentów ze średnią mniejszą niż 4.4. Wykorzystać dane ze zmiennej students
            double result_3c = 0.0; //Uzupełnić
            Console.WriteLine($"Średnia wieku studentów ze średnią mniejszą niż 4.4: {result_3c}");

            Console.WriteLine("\n//---------------------Etap4(1.5p)---------------------//");

            IEnumerable<int> result_4a = null;
            IEnumerable<int> result_4b = null;

            //TODO: Zmodyfikować ciąg sorted_number w taki sposób, aby na zmianę występowała liczba parzysta i liczba nieparzysta
            // (np. mając zbiór 1,1,3,4,6,8,8,11,13,19 należy po liczbie 1 usunąć ciąg liczb nieparzystych dochodząc do liczby 4, gdzie zmienia się parzystość). Wynik zapisać do zmiennej result_4a 
            result_4a = null; //Uzupełnić

            //TODO: Dla ciągu sorted_number zwrócić ciąg najmniejszych wartości spośród funkcji: x->x, x->4, x->x^2-5x, x->-|x|. Wynik zapisać do zmiennej result_4b
            result_4b = null; //Uzupełnić

            Func<int,int> result_4c = EnumerableExtender.MinFunc<int>();

            result_4a?.Print();
            result_4b?.Print();

            Console.WriteLine($"Funkcja x->x: 10->{result_4c(10)}");
        }
    }
}
