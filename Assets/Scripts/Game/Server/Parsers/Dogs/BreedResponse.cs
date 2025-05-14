using Newtonsoft.Json;

namespace Game.Server.Parsers.Dogs
{
    public class BreedResponse
    {
        [JsonProperty("data")]
        public BreedData Data { get; set; }

        [JsonProperty("links")]
        public BreedLinks Links { get; set; }
    }
}