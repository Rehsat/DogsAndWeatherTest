using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Game.Server.Parsers.Dogs
{
    public class BreedResponse //TODO Weight Range LifeSpan
    {
        [JsonProperty("data")]
        public List<BreedData> Data { get; set; }

        [JsonProperty("meta")]
        public MetaData Meta { get; set; }

        [JsonProperty("links")]
        public ResponseLinks Links { get; set; }
    }
}