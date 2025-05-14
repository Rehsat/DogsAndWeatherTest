using Newtonsoft.Json;

namespace Game.Server.Parsers.Dogs
{
    public class BreedData
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("attributes")]
        public BreedAttributes Attributes { get; set; }
    }
}