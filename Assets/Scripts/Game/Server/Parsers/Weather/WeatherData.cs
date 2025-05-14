using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UniRx;
using UnityEngine;

namespace Game.Server.Parsers.Weather
{
    public class WeatherData
    {
        [JsonProperty("@context")]
        public IReadOnlyList<object> Context { get; }
    
        [JsonProperty("type")]
        public string Type { get; }
    
        [JsonProperty("geometry")]
        public Geometry Geometry { get; }
    
        [JsonProperty("properties")]
        public WeatherProperties Properties { get; }

        [JsonConstructor]
        public WeatherData(
            IReadOnlyList<object> context,
            string type,
            Geometry geometry,
            WeatherProperties properties)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Geometry = geometry ?? throw new ArgumentNullException(nameof(geometry));
            Properties = properties ?? throw new ArgumentNullException(nameof(properties));
        }
    }
}