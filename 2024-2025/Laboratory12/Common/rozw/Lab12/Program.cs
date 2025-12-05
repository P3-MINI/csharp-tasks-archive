using HttpSimulator;
using System.Text.Json;

namespace Lab12;

internal class Program
{
    static async Task Main(string[] args)
    {
        var cities = new[] { "New York", "London", "Tokyo", "Sydney", "Berlin" };

        var weatherTasks = cities.Select(city => FetchWeatherDataAsync(city));

        try
        {
            var weatherData = await Task.WhenAll(weatherTasks);

            Console.WriteLine("Processing data asynchronously...");
            var processedData = await ProcessWeatherDataAsync(weatherData);

            Console.WriteLine("Displaying results...");
            await DisplayResultsAsync(processedData);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    static async Task<CityWeather> FetchWeatherDataAsync(string city)
    {
        Console.WriteLine($"Fetching data for {city}...");
        using var client = new HttpClient(MockHttpMessageHandlerSingleton.Instance);
        var apiUrl = $"https://127.0.0.1:2137/api/v13/forecast?city={city}&daily=temperature";

        var response = await client.GetStringAsync(apiUrl);
        var data = JsonSerializer.Deserialize<WeatherApiResponse>(response);
        if (data == null)
            throw new Exception("Invalid JSON data!");

        return new CityWeather
        {
            City = city,
            DailyTemperatures = data.Daily.Temperature
        };
    }

    static async Task<List<ProcessedCityWeather>> ProcessWeatherDataAsync(CityWeather[] weatherData)
    {
        var tasks = weatherData.Select(async cityData =>
        {
            var averageTemp = await Task.Run(() => cityData.DailyTemperatures.Average());
            var extremeDays = await IdentifyExtremeWeatherDaysAsync(cityData.DailyTemperatures);

            return new ProcessedCityWeather
            {
                City = cityData.City,
                AverageTemperature = averageTemp,
                ExtremeWeatherDays = extremeDays
            };
        });

        return await Task.WhenAll(tasks).ContinueWith(t => t.Result.ToList());
    }

    static async Task<List<string>> IdentifyExtremeWeatherDaysAsync(List<double> dailyTemperatures)
    {
        return await Task.Run(() =>
            dailyTemperatures
                .Select((temp, index) => new { Day = $"Day {index + 1}", Temp = temp })
                .Where(x => x.Temp > 30 || x.Temp < 0)
                .Select(x => x.Day)
                .ToList()
        );
    }

    static async Task DisplayResultsAsync(List<ProcessedCityWeather> processedData)
    {
        var highestTempCity = processedData.OrderByDescending(x => x.AverageTemperature).First();
        var overallAverageTemp = await Task.Run(() => processedData.Average(x => x.AverageTemperature));

        Console.WriteLine($"\nCity with the highest average temperature: {highestTempCity.City} ({highestTempCity.AverageTemperature:F2}°C)");
        Console.WriteLine($"Overall average temperature: {overallAverageTemp:F2}°C");

        foreach (var cityWeather in processedData)
        {
            Console.WriteLine($"\n{cityWeather.City}:");
            Console.WriteLine($"  Average Temperature: {cityWeather.AverageTemperature:F2}°C");
            Console.WriteLine($"  Extreme Weather Days: {string.Join(", ", cityWeather.ExtremeWeatherDays)}");
        }

        await Task.CompletedTask;
    }
}
