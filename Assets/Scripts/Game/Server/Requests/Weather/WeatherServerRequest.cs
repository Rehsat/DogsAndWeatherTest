using System;
using UnityEngine.Networking;

namespace Game.Server.Requests.Weather
{
    public class WeatherServerRequest : ServerRequest
    {
        public const string WEATHER_URL = "https://api.weather.gov/gridpoints/TOP/32,81/forecast"; 
        // можно создать конфиг со всеми URL и уже из него брать
        // но для этого тестового это будет оверинженеринг, на мой взгляд
        
        public WeatherServerRequest(Action<DownloadHandler> callback) : base(WEATHER_URL, callback)
        {
        }
    }
}