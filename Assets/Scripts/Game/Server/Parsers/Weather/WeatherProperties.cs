using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Game.Server.Parsers.Weather
{
    public class WeatherProperties
    {
        [JsonProperty("units")]
        public string Units { get; }
    
        [JsonProperty("forecastGenerator")]
        public string ForecastGenerator { get; }
    
        [JsonProperty("generatedAt")]
        public DateTimeOffset GeneratedAt { get; }
    
        [JsonProperty("updateTime")]
        public DateTimeOffset UpdateTime { get; }
    
        [JsonProperty("validTimes")]
        public string ValidTimes { get; }
    
        [JsonProperty("elevation")]
        public Elevation Elevation { get; }
    
        [JsonProperty("periods")]
        public IReadOnlyList<WeatherPeriod> Periods { get; }

        [JsonConstructor]
        public WeatherProperties(
            string units,
            string forecastGenerator,
            DateTimeOffset generatedAt,
            DateTimeOffset updateTime,
            string validTimes,
            Elevation elevation,
            IReadOnlyList<WeatherPeriod> periods)
        {
            Units = units ?? throw new ArgumentNullException(nameof(units));
            ForecastGenerator = forecastGenerator ?? throw new ArgumentNullException(nameof(forecastGenerator));
            GeneratedAt = generatedAt;
            UpdateTime = updateTime;
            ValidTimes = validTimes ?? throw new ArgumentNullException(nameof(validTimes));
            Elevation = elevation ?? throw new ArgumentNullException(nameof(elevation));
            Periods = periods ?? throw new ArgumentNullException(nameof(periods));
        }
    }
}