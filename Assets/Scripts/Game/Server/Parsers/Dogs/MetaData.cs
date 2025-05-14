using Newtonsoft.Json;

namespace Game.Server.Parsers.Dogs
{
    public class MetaData
    {
        [JsonProperty("pagination")]
        public PaginationInfo Pagination { get; set; }
    }
}