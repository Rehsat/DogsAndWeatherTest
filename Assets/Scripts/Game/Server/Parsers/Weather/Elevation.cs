using System;
using Newtonsoft.Json;

namespace Game.Server.Parsers.Weather
{
    public class Elevation
    {
        [JsonProperty("unitCode")]
        public string UnitCode { get; }
    
        [JsonProperty("value")]
        public double Value { get; }

        [JsonConstructor]
        public Elevation(string unitCode, double value)
        {
            UnitCode = unitCode ?? throw new ArgumentNullException(nameof(unitCode));
            Value = value;
        }
    }
}