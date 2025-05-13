using System;
using Newtonsoft.Json;

namespace Game.Server.Parsers.Weather
{
    public class WeatherPeriod
    {
        [JsonProperty("number")]
        public int Number { get; }
    
        [JsonProperty("name")]
        public string Name { get; }
    
        [JsonProperty("startTime")]
        public DateTimeOffset StartTime { get; }
    
        [JsonProperty("endTime")]
        public DateTimeOffset EndTime { get; }
    
        [JsonProperty("isDaytime")]
        public bool IsDaytime { get; }
    
        [JsonProperty("temperature")]
        public int Temperature { get; }
    
        [JsonProperty("temperatureUnit")]
        public string TemperatureUnit { get; }
    
        [JsonProperty("temperatureTrend")]
        public string TemperatureTrend { get; }
    
        [JsonProperty("probabilityOfPrecipitation")]
        public PrecipitationProbability ProbabilityOfPrecipitation { get; }
    
        [JsonProperty("windSpeed")]
        public string WindSpeed { get; }
    
        [JsonProperty("windDirection")]
        public string WindDirection { get; }
    
        [JsonProperty("icon")]
        public string Icon { get; }
    
        [JsonProperty("shortForecast")]
        public string ShortForecast { get; }
    
        [JsonProperty("detailedForecast")]
        public string DetailedForecast { get; }

        [JsonConstructor]
        public WeatherPeriod(
            int number,
            string name,
            DateTimeOffset startTime,
            DateTimeOffset endTime,
            bool isDaytime,
            int temperature,
            string temperatureUnit,
            string temperatureTrend,
            PrecipitationProbability probabilityOfPrecipitation,
            string windSpeed,
            string windDirection,
            string icon,
            string shortForecast,
            string detailedForecast)
        {
            Number = number;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            StartTime = startTime;
            EndTime = endTime;
            IsDaytime = isDaytime;
            Temperature = temperature;
            TemperatureUnit = temperatureUnit ?? throw new ArgumentNullException(nameof(temperatureUnit));
            TemperatureTrend = temperatureTrend ?? throw new ArgumentNullException(nameof(temperatureTrend));
            ProbabilityOfPrecipitation = probabilityOfPrecipitation ?? throw new ArgumentNullException(nameof(probabilityOfPrecipitation));
            WindSpeed = windSpeed ?? throw new ArgumentNullException(nameof(windSpeed));
            WindDirection = windDirection ?? throw new ArgumentNullException(nameof(windDirection));
            Icon = icon ?? throw new ArgumentNullException(nameof(icon));
            ShortForecast = shortForecast ?? throw new ArgumentNullException(nameof(shortForecast));
            DetailedForecast = detailedForecast ?? throw new ArgumentNullException(nameof(detailedForecast));
        }
    }
}