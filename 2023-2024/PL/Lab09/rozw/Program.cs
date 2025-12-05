#define STAGE_1
#define STAGE_2
#define STAGE_3
#define STAGE_4

using System.Text.Json;
using System.Reflection;
using System.Globalization;
using System.Numerics;
using System.Text;

namespace PL_Lab09
{
    internal struct WeatcherData
    {
        public float Temperature { get; set; }
        public float Precipitation { get; set; }
        public float Humidity { get; set; }
        public float WindSpeed { get; set; }
        public int WindDirection { get; set; }
    }

    internal class Program
    {
        internal static IDictionary<DateTime, WeatcherData> GetWeatherData(string filename)
        {
            IDictionary<DateTime, WeatcherData> result = new SortedDictionary<DateTime, WeatcherData>();

            try
            {
                using (StreamReader reader = new StreamReader(filename))
                {
                    string content = reader.ReadToEnd();

                    result = JsonSerializer.Deserialize<SortedDictionary<DateTime, WeatcherData>>(content);
                }
            }
            catch (Exception exception)
            {
                int dotNetVersion = System.Environment.Version.Major;

                string assemblyConfig = typeof(Program).Assembly.GetCustomAttribute<AssemblyConfigurationAttribute>().Configuration;

                Console.WriteLine($"Please Paste \"Data.json\" File Into ../bin/{assemblyConfig}/net{dotNetVersion}.0/");

                Console.WriteLine(); Console.WriteLine(exception.Message);
            }

            return result;
        }

        internal static void Main(string[] args)
        {
            IDictionary<DateTime, WeatcherData> weatherData = GetWeatherData("Data.json");

            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

#if STAGE_1
            Console.WriteLine();
            Console.WriteLine(" ----------------------- STAGE_1 (1.0 Pts) -----------------------");
            Console.WriteLine();

            float floatTemperature = new Temperature() { Value = 24.5f };
            int intTemperature = (int)(new Temperature() { Value = 18.7f });

            float floatPrecipitation = new Precipitation() { Value = 3.9f };
            int intPrecipitation = (int)(new Precipitation() { Value = 0.7f });

            int intWindDirection = new WindDirection() { Value = 341 };
            float flaotWindDirection = new WindDirection() { Value = 271 };

            List<WindDirection> windDirections = new List<WindDirection>()
            {
                new WindDirection() {Value = 345},
                new WindDirection() {Value = 152},
                new WindDirection() {Value = 279},
            };

            List<Temperature> temperatures = new List<Temperature>()
            {
                new Temperature() {Value = 22.1f},
                new Temperature() {Value = 21.7f},
                new Temperature() {Value = 19.2f},
            };

            List<Precipitation> precipitation = new List<Precipitation>()
            {
                new Precipitation() {Value = 2.1f},
                new Precipitation() {Value = 1.9f},
                new Precipitation() {Value = 3.8f},
            };

            WeatherInfo weatherInfo_1 = new WeatherInfo() { WindDirection = windDirections[0], Temperature = temperatures[0], Precipitation = precipitation[0] };
            WeatherInfo weatherInfo_2 = new WeatherInfo() { WindDirection = windDirections[1], Temperature = temperatures[1], Precipitation = precipitation[1] };
            WeatherInfo weatherInfo_3 = new WeatherInfo() { WindDirection = windDirections[2], Temperature = temperatures[2], Precipitation = precipitation[2] };

            WeatherForecast weatherForecast_1 = new WeatherForecast();
            {
                weatherForecast_1.Info[DateTime.Now.AddDays(-3)] = weatherInfo_1;
                weatherForecast_1.Info[DateTime.Now.AddDays(-2)] = weatherInfo_2;
                weatherForecast_1.Info[DateTime.Now.AddDays(-1)] = weatherInfo_3;
            }

            Console.WriteLine($"Temperature: [{string.Join(" ", weatherForecast_1.GetTemperatures<float>())}]");
            Console.WriteLine($"Precipitation: [{string.Join(" ", weatherForecast_1.GetPrecipitations<float>())}]");
            Console.WriteLine($"WindDirection: [{string.Join(" ", weatherForecast_1.GetWindDirections<float>())}]");
#endif

#if STAGE_2
            Console.WriteLine();
            Console.WriteLine(" ----------------------- STAGE_2 (1.5 Pts) -----------------------");
            Console.WriteLine();

            string ConstructCollectionOutput<T>(IWeights<T> values, int limit) where T : INumber<T>
            {
                StringBuilder stringBuilder = new StringBuilder(3 * limit);

                stringBuilder.Append('[');

                foreach (T value in values)
                {
                    if (--limit < 0) break;

                    stringBuilder.Append($"{value} ");
                }

                stringBuilder.Replace(' ', ']', stringBuilder.Length - 1, 1);

                return stringBuilder.ToString();
            }

            UniformWeights<int> intUniformWeight = new UniformWeights<int>();
            UniformWeights<byte> byteUniformWeight = new UniformWeights<byte>();
            UniformWeights<double> doubleUniformWeight = new UniformWeights<double>();

            ExponentialWeights<int> intExponentialWeights = new ExponentialWeights<int>();
            ExponentialWeights<byte> byteExponentialWeights = new ExponentialWeights<byte>();
            ExponentialWeights<float> floatExponentialWeights = new ExponentialWeights<float>();

            RandomWeights<byte> byteRandomWeights = new RandomWeights<byte>(256, 300);
            RandomWeights<float> floatRandomWeights = new RandomWeights<float>(5, 300);
            RandomWeights<double> doubleRandomWeights = new RandomWeights<double>(0, 1);

            Console.WriteLine($"UniformWeights<int>:    {ConstructCollectionOutput(intUniformWeight, 5)}");
            Console.WriteLine($"UniformWeights<byte>:   {ConstructCollectionOutput(byteUniformWeight, 5)}");
            Console.WriteLine($"UniformWeights<double>: {ConstructCollectionOutput(doubleUniformWeight, 5)}");
            Console.WriteLine();
            Console.WriteLine($"ExponentialWeights<int>: {ConstructCollectionOutput(intExponentialWeights, 5)}");
            Console.WriteLine($"ExponentialWeights<byte>: {ConstructCollectionOutput(byteExponentialWeights, 5)}");
            Console.WriteLine($"ExponentialWeights<float>: {ConstructCollectionOutput(floatExponentialWeights, 5)}");
            Console.WriteLine();
            try
            {
                Console.WriteLine($"RandomWeights<byte>: {ConstructCollectionOutput(byteRandomWeights, 15)}");

                Console.WriteLine($"RandomWeights<byte>: SHOULD THROW AN EXCEPTION, ERROR!");
            }
            catch
            {
                Console.WriteLine($"RandomWeights<byte>: Thrown An Exception, OK!");
            }

            Console.WriteLine($"RandomWeights<float>: {ConstructCollectionOutput(floatRandomWeights, 15)}");
            Console.WriteLine($"RandomWeights<double>: {ConstructCollectionOutput(doubleRandomWeights, 15)}");
#endif

#if STAGE_3
            Console.WriteLine();
            Console.WriteLine(" ----------------------- STAGE_3 (1.0 Pts) -----------------------");
            Console.WriteLine();

            for (int i = 0; i < 5; i++)
            {
                string randomString = StringExtension.Random();

                Console.WriteLine($"Random String: \"{randomString}\" And It's AlphaNumeric Value: \"{randomString.AlphaNumeric()}\"");
            }
#endif

#if STAGE_4
            Console.WriteLine();
            Console.WriteLine(" ----------------------- STAGE_4 (1.5 Pts) -----------------------");
            Console.WriteLine();

            WeatherForecast weatherForecast_2 = new WeatherForecast();
            {
                foreach (var value in weatherData)
                {
                    weatherForecast_2.Info.Add(value.Key, new WeatherInfo()
                    {
                        Temperature = new Temperature() { Value = value.Value.Temperature },
                        Precipitation = new Precipitation() { Value = value.Value.Precipitation },
                        WindDirection = new WindDirection() { Value = value.Value.WindDirection },
                    });
                }
            }

            Console.WriteLine($"Temperature<float> Average with UniformWeights<int>: {weatherForecast_2.GetTemperatures<float>().Average(intUniformWeight)}");
            Console.WriteLine($"Temperature<int> Average with UniformWeights<int>: {weatherForecast_2.GetTemperatures<int>().Average(intUniformWeight)}");
            Console.WriteLine();
            Console.WriteLine($"Precipitation<int> Average with RandomWeights<float>: {weatherForecast_2.GetPrecipitations<int>().Average(floatRandomWeights)}");
            Console.WriteLine($"Precipitation<float> Average with RandomWeights<float>: {weatherForecast_2.GetPrecipitations<float>().Average(floatRandomWeights)}");
            Console.WriteLine();
            Console.WriteLine($"WindDirection<float> Average with ExponentialWeights<int>: {weatherForecast_2.GetWindDirections<float>().Average(intExponentialWeights)}");
            Console.WriteLine($"WindDirection<float> Average with ExponentialWeights<float>: {weatherForecast_2.GetWindDirections<float>().Average(floatExponentialWeights)}");
            Console.WriteLine();
            Console.WriteLine($"Precipitation<int> Mode: {weatherForecast_2.GetPrecipitations<int>().Mode()}");
            Console.WriteLine($"Precipitation<float> Mode: {weatherForecast_2.GetPrecipitations<float>().Mode()}");
            Console.WriteLine();
            Console.WriteLine($"Temperature<int> Mode: {weatherForecast_2.GetTemperatures<int>().Mode()}");
            Console.WriteLine($"Temperature<float> Mode: {weatherForecast_2.GetTemperatures<float>().Mode()}");
            Console.WriteLine();
            Console.WriteLine($"WindDirection<int> Median: {weatherForecast_2.GetWindDirections<int>().Median()}");
            Console.WriteLine($"WindDirection<float> Median: {weatherForecast_2.GetWindDirections<float>().Median()}");
            Console.WriteLine();
            Console.WriteLine($"Precipitation<int> Median: {weatherForecast_2.GetPrecipitations<int>().Median()}");
            Console.WriteLine($"Precipitation<float> Median: {weatherForecast_2.GetPrecipitations<float>().Median()}");
            Console.WriteLine();
#endif
        }
    }
}

