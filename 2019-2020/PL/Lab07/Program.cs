
using System;
using System.Collections;

namespace lab7yield
{

    class MainClass
    {

        public static void PrintSequence(ISequence sequence, int limit = 0)
        {
            Console.WriteLine(sequence.GetSignature());
            PrintSequence((IEnumerable)sequence, limit);
        }

        public static void PrintSequence(IEnumerable sequence, int limit = 0)
        {
            int k = 1;
            foreach (object el in sequence)
                if (k++ <= limit || limit == 0)
                    Console.Write(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:#0.#### }", el));
                else break;
            Console.WriteLine();
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("Hello Yield!");

            int limit = 10;

            Console.WriteLine(Environment.NewLine + "======= Etap 1 ======= ISequence i PrintSequence =======" + Environment.NewLine);

            PrintSequence(new ArraySequence(new int[]{3,5,7,11,13,17,19}), limit);

            Console.WriteLine();
            PrintSequence(new ArithmeticSequence(0, 1), limit);
            PrintSequence(new ArithmeticSequence(3, 2), limit);
            PrintSequence(new ArithmeticSequence(0, -1), limit);
            PrintSequence(new ArithmeticSequence(1, 3), limit);

            Console.WriteLine();
            PrintSequence(new NaturalNumbers(), limit);

            Console.WriteLine();
            PrintSequence(new RandomSequence(7, 7), limit);
            PrintSequence(new RandomSequence(3, 25), limit);
            PrintSequence(new RandomSequence(11, 100), limit);

            Console.WriteLine();
            PrintSequence(new Tribonacci(), limit);

            Console.WriteLine(Environment.NewLine + "======= Etap 2 ======= ISingleSequenceProcessor =======" + Environment.NewLine);

            ISequence sequence = new NaturalNumbers();

            ISingleSequenceProcessor processor = new SequenceSum();
            Console.WriteLine("{0} : {1}",processor.GetSignature(),sequence.GetSignature());
            PrintSequence(processor.Process(sequence), limit);

            Console.WriteLine();
            processor = new SequenceRemainderer(2);
            Console.WriteLine("{0} : {1}",processor.GetSignature(),sequence.GetSignature());
            PrintSequence(processor.Process(sequence), limit);

            processor = new SequenceRemainderer(5);
            Console.WriteLine("{0} : {1}",processor.GetSignature(),sequence.GetSignature());
            PrintSequence(processor.Process(sequence), limit);

            Console.WriteLine();
            processor = new EveryNFilter(2, 3);
            Console.WriteLine("{0} : {1}",processor.GetSignature(),sequence.GetSignature());
            PrintSequence(processor.Process(sequence), limit);

            processor = new EveryNFilter(2, 3, true);
            Console.WriteLine("{0} : {1}",processor.GetSignature(),sequence.GetSignature());
            PrintSequence(processor.Process(sequence), limit);

            Console.WriteLine();
            processor = new EveryNSumMFilter(2, 3, 2);
            Console.WriteLine("{0} : {1}",processor.GetSignature(),sequence.GetSignature());
            PrintSequence(processor.Process(sequence), limit);

            processor = new EveryNSumMFilter(1, 1, 1);
            Console.WriteLine("{0} : {1}",processor.GetSignature(),sequence.GetSignature());
            PrintSequence(processor.Process(sequence), limit);


            Console.WriteLine(Environment.NewLine + "======= Etap 3 ======= IDoubleSequenceProcessor =======" + Environment.NewLine);

            ISequence sequence1 = new RandomSequence(7, 100);
            ISequence sequence2 = new RandomSequence(25, 100);
            PrintSequence(sequence1, limit);
            PrintSequence(sequence2, limit);

            processor = new SequenceRemainderer(2);
            Console.WriteLine("{0} : {1}",processor.GetSignature(),sequence1.GetSignature());
            PrintSequence(processor.Process(sequence1), limit);
            Console.WriteLine("{0} : {1}",processor.GetSignature(),sequence2.GetSignature());
            PrintSequence(processor.Process(sequence2), limit);

            ISequence arrSequence1 = new ArraySequence(new int[]{1,1,0,1,0,1});
            ISequence arrSequence2 = new ArraySequence(new int[]{1,0,1});
            PrintSequence(arrSequence1, limit);
            PrintSequence(arrSequence2, limit);

            Console.WriteLine();

            IDoubleSequenceProcessor doubleProcessor = new LogicGateAND();
            Console.WriteLine(doubleProcessor.GetSignature());
            PrintSequence(doubleProcessor.Process(processor.Process(sequence1),processor.Process(sequence2)), limit);
            PrintSequence(doubleProcessor.Process(processor.Process(arrSequence1),processor.Process(arrSequence2)), limit);
            PrintSequence(doubleProcessor.Process(processor.Process(arrSequence2),processor.Process(arrSequence1)), limit);

            doubleProcessor = new LogicGateOR();
            Console.WriteLine(doubleProcessor.GetSignature());
            PrintSequence(doubleProcessor.Process(processor.Process(sequence1),processor.Process(sequence2)), limit);
            PrintSequence(doubleProcessor.Process(processor.Process(arrSequence1),processor.Process(arrSequence2)), limit);
            PrintSequence(doubleProcessor.Process(processor.Process(arrSequence2),processor.Process(arrSequence1)), limit);

        Console.WriteLine();
        }

    }

}
