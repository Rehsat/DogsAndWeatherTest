using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Game.Server.Parsers.Weather
{
    public class Geometry
    {
        [JsonProperty("type")]
        public string Type { get; }
    
        [JsonProperty("coordinates")]
        public IReadOnlyList<IReadOnlyList<IReadOnlyList<double>>> Coordinates { get; }

        [JsonConstructor]
        public Geometry(string type, IReadOnlyList<IReadOnlyList<IReadOnlyList<double>>> coordinates)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Coordinates = coordinates ?? throw new ArgumentNullException(nameof(coordinates));
        }
    }
}