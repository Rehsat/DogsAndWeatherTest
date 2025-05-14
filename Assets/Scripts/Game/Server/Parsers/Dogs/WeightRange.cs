using Newtonsoft.Json;

namespace Game.Server.Parsers.Dogs
{
    public class WeightRange
    {
        [JsonProperty("min")]
        public int Min { get; set; }

        [JsonProperty("max")]
        public int Max { get; set; }
    }
}