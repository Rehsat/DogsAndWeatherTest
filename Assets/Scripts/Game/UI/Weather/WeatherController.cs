using Game.Server.Parsers.Weather;
using Game.Server.Requests;
using UnityEngine;

namespace Game.UI.Weather
{
    public class WeatherController
    {
        private readonly IWeatherView _weatherView;

        public WeatherController(IServerCallbackHandler<WeatherData> observer, IWeatherView weatherView)
        {
            _weatherView = weatherView;
            observer.OnNewDataFromServer.SubscribeWithSkip(SetData);
        }

        public void SetData(WeatherData weatherData)
        {
            var currentWeatherData = weatherData.Properties.Periods[0]; 
            var text = $"Сегодня - {currentWeatherData.Temperature}{currentWeatherData.TemperatureUnit}";
            _weatherView.SetText(text);
        }
    }
}