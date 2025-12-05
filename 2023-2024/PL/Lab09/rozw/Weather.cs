using System.Numerics;

namespace PL_Lab09
{
    internal struct Temperature
    {
        public float Value { get; set; }

        public static implicit operator float(Temperature temperature) => temperature.Value;
    }

    internal struct Precipitation
    {
        public float Value { get; set; }

        public static implicit operator float(Precipitation precipitation) => precipitation.Value;
    }

    internal struct WindDirection
    {
        public int Value { get; set; }

        public static implicit operator int(WindDirection windDirection) => windDirection.Value;
    }

    internal struct WeatherInfo
    {
        public Temperature Temperature { get; set; }
        public Precipitation Precipitation { get; set; }
        public WindDirection WindDirection { get; set; }
    }

    internal class WeatherForecast
    {
        public SortedDictionary<DateTime, WeatherInfo> Info { get; } = new SortedDictionary<DateTime, WeatherInfo>();

        public List<T> GetTemperatures<T>() where T : INumber<T>
        {
            return this.Info.Values.Select(x => T.CreateChecked(x.Temperature.Value)).ToList();
        }

        public List<T> GetPrecipitations<T>() where T: INumber<T>
        {
            return this.Info.Values.Select(x => T.CreateChecked(x.Precipitation.Value)).ToList();
        }

        public List<T> GetWindDirections<T>() where T : INumber<T>
        {
            return this.Info.Values.Select(x => T.CreateChecked(x.WindDirection.Value)).ToList();
        }
    }
}
