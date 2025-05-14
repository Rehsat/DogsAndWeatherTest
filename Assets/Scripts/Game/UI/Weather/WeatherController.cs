using Game.Server.Parsers.Weather;
using Game.Server.Requests;
using UniRx;
using UnityEngine;

namespace Game.UI.Weather
{
    public class WeatherController
    {
        private readonly IWeatherView _weatherView;
        private CompositeDisposable _currentWeatherDataDisposable;
        public WeatherController(IServerCallbackHandler<WeatherPeriod> observer, IWeatherView weatherView)
        {
            _weatherView = weatherView;
            observer.OnNewDataFromServer.SubscribeWithSkip(SetData);
        }

        public void SetData(WeatherPeriod currentWeatherData)
        {
            var text = $"Сегодня - {currentWeatherData.Temperature}{currentWeatherData.TemperatureUnit}";
            _weatherView.SetText(text);
            
            _currentWeatherDataDisposable?.Dispose();
            _currentWeatherDataDisposable= new CompositeDisposable();
            currentWeatherData.Sprite.Subscribe(_weatherView.SetIcon);
        }
    }
}