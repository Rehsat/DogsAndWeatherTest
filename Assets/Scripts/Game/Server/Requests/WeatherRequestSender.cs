using System;

namespace Game.Server.Requests
{
    public class WeatherRequestSender : RequestSender
    {
        private readonly Action<string> _callback;

        public const string WEATHER_URL = "https://api.weather.gov/gridpoints/TOP/32,81/forecast"; // можно создать конфиг со всеми URL и уже из него брать
        // но для этого тестового это будет оверинженеринг, на мой взглядд
        
        public WeatherRequestSender(Action<string> callback) : base(WEATHER_URL)
        {
            _callback = callback;
        }

        protected override void OnComplete(string serverCallback)
        {
            _callback.Invoke(serverCallback);
        }
    }
}