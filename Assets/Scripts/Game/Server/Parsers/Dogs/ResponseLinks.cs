using Newtonsoft.Json;

namespace Game.Server.Parsers.Dogs
{
    public class ResponseLinks
    {
        [JsonProperty("self")]
        public string Self { get; set; }

        [JsonProperty("current")]
        public string Current { get; set; }

        [JsonProperty("next")]
        public string Next { get; set; }

        [JsonProperty("last")]
        public string Last { get; set; }
    }
}