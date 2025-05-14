using Newtonsoft.Json;

namespace Game.Server.Parsers.Dogs
{
    public class BreedAttributes
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("hypoallergenic")]
        public bool Hypoallergenic { get; set; }

        [JsonProperty("life")]
        public LifeSpan Life { get; set; }

        [JsonProperty("male_weight")]
        public WeightRange MaleWeight { get; set; }

        [JsonProperty("female_weight")]
        public WeightRange FemaleWeight { get; set; }
    }
}