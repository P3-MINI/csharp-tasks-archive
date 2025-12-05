using System;

namespace lab09_b
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintLine("-----------------------Stage 1 -------------------------");
            //var pq1 = new MaxPriorityQueue<int>();
            //PrintLine("3 <- 1 <- 4 <- 2");
            //pq1.Put(3);
            //pq1.Put(1);
            //pq1.Put(4);
            //pq1.Put(2);

            //PrintLine($"Count (4) test -> {(pq1.Count == 4 ? "OK" : "INVALID")}");
            //var peek1 = pq1.Peek;
            //PrintLine($"Peek test (Peek == 4) -> {(peek1 == 4 ? "OK" : "INVALID")}");
            //PrintLine($"Peek Count test (Count == 4) -> {(pq1.Count == 4 ? "OK" : "INVALID")}");

            //var get = pq1.Get();
            //PrintLine($"Get test (Get() == 4 -> {(get == 4 ? "OK" : "INVALID")}");
            //PrintLine($"Count (3) test -> {(pq1.Count == 3 ? "OK" : "INVALID")}");

            //get = pq1.Get();
            //PrintLine($"Get test (Get() == 3 -> {(get == 3 ? "OK" : "INVALID")}");
            //PrintLine($"Count (2) test {(pq1.Count == 2 ? "OK" : "INVALID")}");

            //get = pq1.Get();
            //PrintLine($"Get test (Get() == 2 -> {(get == 2 ? "OK" : "INVALID")}");
            //PrintLine($"Count (1) test -> {(pq1.Count == 1 ? "OK" : "INVALID")}");

            //get = pq1.Get();
            //PrintLine($"Get test (Get() == 1 -> {(get == 1 ? "OK" : "INVALID")}");
            //PrintLine($"Count (0) test -> {(pq1.Count == 0 ? "OK" : "INVALID")}");

            //try
            //{
            //    var item = pq1.Get();
            //    PrintLine($"Get item for empty queue should raise InvalidOperationException: INVALID");
            //}
            //catch (InvalidOperationException)
            //{
            //    PrintLine($"Get item for empty queue should raise InvalidOperationException: OK");
            //}
            //try
            //{
            //    var item = pq1.Peek;
            //    PrintLine($"Peek item for empty queue should raise InvalidOperationException: INVALID");
            //}
            //catch (InvalidOperationException)
            //{
            //    PrintLine($"Peek item for empty queue should raise InvalidOperationException: OK");
            //}


            PrintLine("-----------------------Stage 2 -------------------------");
            //var pq3 = new MaxPriorityQueue<char>();
            //pq3.Put('A');
            //pq3.Put('B');
            //pq3.Put('C');
            //pq3.Put('D');
            //pq3.Put('E');
            //pq3.Put('F');
            //pq3.Put('G');
            //pq3.Put('H');
            //pq3.Put('I');
            //pq3.Put('J');
            //PrintLine("Expected : J I H G F E D C B A");

            //Console.Write("Actual   : ");
            //foreach (var item in pq3)
            //{
            //    Console.Write($"{item} ");
            //}
            //PrintLine(string.Empty);


            PrintLine("-----------------------Stage 3 -------------------------");
            //var existingDate = new DateTime(2020, 12, 03);
            //var nonExistingDate = new DateTime(2020, 12, 31);
            //var minDate = new DateTime(2018, 01, 01);
            //var pq4 = new MaxPriorityQueue<DateTime>();
            //pq4.Put(new DateTime(2019, 12, 03));
            //pq4.Put(existingDate);
            //pq4.Put(new DateTime(2020, 01, 31));
            //pq4.Put(new DateTime(2020, 06, 01));
            //pq4.Put(new DateTime(2020, 11, 11));
            //pq4.Put(minDate);

            //var isExistingDatePresent = pq4.Exist(existingDate);
            //var isNonExistingDatePresent = pq4.Exist(nonExistingDate);
            //PrintLine($"Date {existingDate:yyyy-MM-dd} should exist in queue -> {(isExistingDatePresent ? "OK" : "INVALID")}");
            //PrintLine($"Date {nonExistingDate:yyyy-MM-dd} should not exist in queue -> {(!isNonExistingDatePresent ? "OK" : "INVALID")}");

            //var minDateResult = pq4.MinItem();
            //PrintLine($"Date {minDate:yyyy-MM-dd} should be min date -> {(minDate == minDateResult ? "OK" : "INVALID")}");
        }

        static void PrintLine(string line)
        {
            Console.WriteLine(line);
        }
    }
}
