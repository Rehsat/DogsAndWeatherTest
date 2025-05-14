using Newtonsoft.Json;

namespace Game.Server.Parsers.Dogs
{
    public class BreedLinks
    {
        [JsonProperty("self")]
        public string Self { get; set; }
    }
}