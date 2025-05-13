using System;
using Newtonsoft.Json;

namespace Game.Server.Parsers.Weather
{
    public class PrecipitationProbability
    {
        [JsonProperty("unitCode")]
        public string UnitCode { get; }
    
        [JsonProperty("value")]
        public int? Value { get; }

        [JsonConstructor]
        public PrecipitationProbability(string unitCode, int? value)
        {
            UnitCode = unitCode ?? throw new ArgumentNullException(nameof(unitCode));
            Value = value;
        }
    }
}