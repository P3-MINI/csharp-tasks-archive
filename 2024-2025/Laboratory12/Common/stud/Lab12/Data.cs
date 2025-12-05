namespace Lab12;

public class CityWeather
{
    public string City { get; set; }
    public List<double> DailyTemperatures { get; set; }
}

public class WeatherApiResponse
{
    public DailyWeather Daily { get; set; }
}

public class DailyWeather
{
    public List<double> Temperature { get; set; }
}

public class ProcessedCityWeather
{
    public string City { get; set; }
    public double AverageTemperature { get; set; }
    public List<string> ExtremeWeatherDays { get; set; }
}