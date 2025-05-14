using Newtonsoft.Json;

namespace Game.Server.Parsers.Dogs
{
    public class PaginationInfo
    {
        [JsonProperty("current")]
        public int Current { get; set; }

        [JsonProperty("records")]
        public int Records { get; set; }
    }
}