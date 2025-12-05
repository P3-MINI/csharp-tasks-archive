// #define STAGE_1
// #define STAGE_2
// #define STAGE_3
// #define STAGE_4
// #define STAGE_5

using System.Globalization;
using System.Numerics;
using System.Text;

namespace EN_Lab09
{
    internal class Program
    {
        static DateTime RandomDate(Random random)
        {
            DateTime start = new DateTime(2014, 9, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(random.Next(range));
        }

        static void Main(string[] args)
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

            Random random = new Random(22154);

#if STAGE_1
            Console.WriteLine();
            Console.WriteLine(" ----------------------- STAGE_1 (1.0 Pts) -----------------------");
            Console.WriteLine();

            int intGrade = new Grade<int>(1);
            float floatGrade = new Grade<float>(3.5f);
            double doubleGrade = new Grade<double>(4.0f);

            Presence presenceGrade = new Presence() { Grade = new Grade<int>(1) };
            Quiz quizGrade = new Quiz() { Grade = new Grade<float>(1.5f) };
            Task taskGrade = new Task() { Grade = new Grade<float>(3.5f) };

            Laboratory laboratory_1 = new Laboratory(presenceGrade, taskGrade);
            Laboratory laboratory_2 = new Laboratory(presenceGrade, taskGrade, quizGrade);

            SubjectInfo subjectInfoGrade = new SubjectInfo();

            for (int i = 0; i < 10; i++)
            {
                Presence presence = new Presence() { Grade = new Grade<int>(random.Next(2)) };
                Task task = new Task() { Grade = new Grade<float>(5.0f * (float)random.NextDouble()) };
                Quiz quiz = new Quiz() { Grade = new Grade<float>(5.0f * (float)random.NextDouble()) };

                subjectInfoGrade.Info.Add(RandomDate(random), new Laboratory(presence, task, quiz));
            }

            Console.WriteLine($"Presence: [{string.Join(" ", subjectInfoGrade.GetPresences<int>())}]");
            Console.WriteLine($"Task: [{string.Join(" ", subjectInfoGrade.GetTasks<float>().Select(x => string.Format("{0:0.00}", x)))}]");
            Console.WriteLine($"Quiz: [{string.Join(" ", subjectInfoGrade.GetQuizzes<float>().Select(x => string.Format("{0:0.00}", x)))}]");
#endif

#if STAGE_2
            Console.WriteLine();
            Console.WriteLine(" ----------------------- STAGE_2 (1.0 Pts) -----------------------");
            Console.WriteLine();

            string ConstructCollectionOutput<T>(IWeights<T> values, int limit) where T : INumber<T>
            {
                StringBuilder stringBuilder = new StringBuilder(4 * limit);

                stringBuilder.Append('[');

                foreach (T value in values)
                {
                    if (--limit < 0) break;

                    stringBuilder.Append($"{value:0.00} ");
                }

                stringBuilder.Replace(' ', ']', stringBuilder.Length - 1, 1);

                return stringBuilder.ToString();
            }

            UniformWeights<int> intUniformWeight = new UniformWeights<int>();
            UniformWeights<byte> byteUniformWeight = new UniformWeights<byte>();
            UniformWeights<double> doubleUniformWeight = new UniformWeights<double>();

            RandomWeights<int> intRandomWeights = new RandomWeights<int>(-100, 100, 12365);
            RandomWeights<float> floatRandomWeights = new RandomWeights<float>(5, 300, 55567);
            RandomWeights<double> doubleRandomWeights = new RandomWeights<double>(0, 1, 12068);

            Console.WriteLine($"UniformWeights<int>:    {ConstructCollectionOutput(intUniformWeight, 5)}");
            Console.WriteLine($"UniformWeights<byte>:   {ConstructCollectionOutput(byteUniformWeight, 5)}");
            Console.WriteLine($"UniformWeights<double>: {ConstructCollectionOutput(doubleUniformWeight, 5)}");
            Console.WriteLine();
            Console.WriteLine($"RandomWeights<int>:   {ConstructCollectionOutput(intRandomWeights, 13)}");
            Console.WriteLine($"RandomWeights<float>:   {ConstructCollectionOutput(floatRandomWeights, 13)}");
            Console.WriteLine($"RandomWeights<double>:  {ConstructCollectionOutput(doubleRandomWeights, 13)}");
#endif

#if STAGE_3
            Console.WriteLine();
            Console.WriteLine(" ----------------------- STAGE_3 (1.0 Pts) -----------------------");
            Console.WriteLine();

            string availableChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz0123456789!@$?_-";

            for (int i = 0; i < 5; i++)
            {
                string randomString = StringExtension.Random(availableChars, 12345 + i);

                Console.WriteLine($"Random String: \"{randomString}\" And It's Rot18 Value: \"{randomString.Rot18()}\" And It's Rot18 Value: \"{randomString.Rot18().Rot18()}\"");
            }
#endif

#if STAGE_4
            Console.WriteLine();
            Console.WriteLine(" ----------------------- STAGE_4 (1.0 Pts) -----------------------");
            Console.WriteLine();

            Console.WriteLine($"Float Mean Of Tasks With int Uniform Weights:   [{string.Join(" ", subjectInfoGrade.GetTasks<float>().Mean(intUniformWeight))}]");
            Console.WriteLine($"Int Mean Of Tasks With double Uniform Weights:  [{string.Join(" ", subjectInfoGrade.GetTasks<int>().Mean(doubleUniformWeight))}]");
            Console.WriteLine($"Double Mean Of Tasks With float Random Weights: [{string.Join(" ", subjectInfoGrade.GetTasks<double>().Mean(floatRandomWeights))}]");
            Console.WriteLine($"Float Mean Of Tasks With float Random Weights:  [{string.Join(" ", subjectInfoGrade.GetTasks<float>().Mean(floatRandomWeights))}]");
            Console.WriteLine();
            Console.WriteLine($"Float Mean Of Quizzes With double Uniform Weights: [{string.Join(" ", subjectInfoGrade.GetQuizzes<float>().Mean(doubleUniformWeight))}]");
            Console.WriteLine($"Float Mean Of Quizzes With float Random Weights:   [{string.Join(" ", subjectInfoGrade.GetQuizzes<float>().Mean(floatRandomWeights))}]");
            Console.WriteLine();
            Console.WriteLine($"Float StandardDeviation Of Quizzes With double Uniform Weights: [{string.Join(" ", subjectInfoGrade.GetQuizzes<float>().StandardDeviation(doubleUniformWeight))}]");
            Console.WriteLine($"Float StandardDeviation Of Tasks With double Uniform Weights:   [{string.Join(" ", subjectInfoGrade.GetTasks<float>().StandardDeviation(doubleUniformWeight))}]");
#endif

#if STAGE_5
            Console.WriteLine();
            Console.WriteLine(" ----------------------- STAGE_5 (1.0 Pts) -----------------------");
            Console.WriteLine();

            List<int> intList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            Console.WriteLine($"Int Moving Average Of 9 Elements (Period 3): [{string.Join(" ", intList.MovingAverage().Select(x => string.Format("{0:0.00}", x)))}]");
            Console.WriteLine();
            Console.WriteLine($"Float Moving Average Of Quizzes (Period 3): [{string.Join(" ", subjectInfoGrade.GetQuizzes<float>().MovingAverage().Select(x => string.Format("{0:0.00}", x)))}]");
            Console.WriteLine($"Float Moving Average Of Quizzes (Period 5): [{string.Join(" ", subjectInfoGrade.GetQuizzes<float>().MovingAverage(5).Select(x => string.Format("{0:0.00}", x)))}]");
            Console.WriteLine();
            Console.WriteLine($"Int Moving Average Of Presences (Period 3):   [{string.Join(" ", subjectInfoGrade.GetPresences<int>().MovingAverage().Select(x => string.Format("{0:0.00}", x)))}]");
            Console.WriteLine($"Float Moving Average Of Presences (Period 3): [{string.Join(" ", subjectInfoGrade.GetPresences<float>().MovingAverage().Select(x => string.Format("{0:0.00}", x)))}]");
            Console.WriteLine();
            Console.WriteLine($"Int Moving Average Of Tasks (Period 1): [{string.Join(" ", subjectInfoGrade.GetTasks<int>().MovingAverage(1).Select(x => string.Format("{0:0.00}", x)))}]");
            Console.WriteLine($"Int Moving Average Of Tasks (Period 3): [{string.Join(" ", subjectInfoGrade.GetTasks<int>().MovingAverage(3).Select(x => string.Format("{0:0.00}", x)))}]");
            Console.WriteLine();
            Console.WriteLine($"Float Moving Average Of Tasks (Period 3): [{string.Join(" ", subjectInfoGrade.GetTasks<float>().MovingAverage().Select(x => string.Format("{0:0.00}", x)))}]");
            Console.WriteLine($"Float Moving Average Of Tasks (Period 7): [{string.Join(" ", subjectInfoGrade.GetTasks<float>().MovingAverage(7).Select(x => string.Format("{0:0.00}", x)))}]");
#endif
        }
    }
}