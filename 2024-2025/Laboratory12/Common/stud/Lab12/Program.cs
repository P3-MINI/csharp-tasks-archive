using HttpSimulator;

namespace Lab12;

internal class Program
{
    static void Main(string[] args)
    {
        var cities = new[] { "New York", "London", "Tokyo", "Sydney", "Berlin" };

        // TODO: Change this code and make it fully asynchronous
        //       See Data.cs for useful data structures
        //       MockWeatherEndpoint.cs and fix bug in ForecastEndpoint method
        foreach (var city in cities)
        {
            using var client = new HttpClient(MockHttpMessageHandlerSingleton.Instance);
            var apiUrl = $"https://127.0.0.1:2137/api/v13/forecast?city={city}&daily=temperature";
            string response = client.GetStringAsync(apiUrl).Result;

            Console.WriteLine(response);
        }

    }
}
