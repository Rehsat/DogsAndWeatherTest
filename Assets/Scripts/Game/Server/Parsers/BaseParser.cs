using System;
using System.IO;
using Newtonsoft.Json;

namespace Game.Server.Parsers
{
    public class BaseParser<TReturnType> : IParser<TReturnType>
    {
        private JsonSerializer _jsonSerializer;
        
        public BaseParser()
        {
            _jsonSerializer = new JsonSerializer();
        }
        
        public TReturnType Parse(string json)
        {
            var jsonReader = new JsonTextReader(new StringReader(json));
            try
            {
                return _jsonSerializer.Deserialize<TReturnType>(jsonReader) 
                       ?? throw new Exception("Deserialization returned null");
            }
            catch (JsonException ex)
            {
                throw new Exception("JSON parsing error", ex);
            }
        }
    }
}
