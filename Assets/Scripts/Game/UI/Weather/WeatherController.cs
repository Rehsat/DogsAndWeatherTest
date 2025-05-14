using Game.Server.Parsers.Weather;
using UnityEngine;

namespace Game.UI.Weather
{
    public class WeatherController
    {
        private readonly IWeatherView _weatherView;

        public WeatherController(IWeatherView weatherView)
        {
            _weatherView = weatherView;
        }

        public void SetData(GeoFeature geoFeature)
        {
            var currentWeatherData = geoFeature.Properties.Periods[0]; 
            //так понимаю TemeratureTrend будет минус при минусовой погоде
            var text = $"Сегодня - {currentWeatherData.TemperatureTrend}{currentWeatherData.Temperature}{currentWeatherData.TemperatureUnit}";
            _weatherView.SetText(text);
        }
    }
}