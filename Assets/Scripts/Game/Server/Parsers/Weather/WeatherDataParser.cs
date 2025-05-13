using System;
using System.Collections;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace Game.Server.Parsers.Weather
{
    public class WeatherDataParser
    {
        private JsonSerializer _jsonSerializer;
        public WeatherDataParser()
        {
            _jsonSerializer = new JsonSerializer();
        }
        public GeoFeature Parse(string json)
        {
            var jsonReader = new JsonTextReader(new StringReader(json));
            try
            {
                return _jsonSerializer.Deserialize<GeoFeature>(jsonReader) 
                       ?? throw new WeatherParsingException("Deserialization returned null");
            }
            catch (JsonException ex)
            {
                throw new WeatherParsingException("JSON parsing error", ex);
            }
        }

    }

    public class WeatherParsingException : Exception
    {
        public WeatherParsingException(string message) : base(message) { }
        public WeatherParsingException(string message, Exception inner) : base(message, inner) { }
    }
}