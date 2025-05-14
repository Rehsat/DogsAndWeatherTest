using System;
using System.Collections;
using Game.Server.Parsers;
using Game.Server.Parsers.Dogs;
using Game.Server.Parsers.Weather;
using Game.Server.Requests;
using Game.Server.Requests.Weather;
using Game.UI.Weather;
using UniRx;
using UnityEngine.Networking;
using Zenject;

namespace Game.GameStateMachine
{
    public class WeatherDataCollectState : IGameState, IDisposable
    {
        private readonly ServerRequestsSender _serverRequestsSender;
        private readonly IServerCallbackHandler<WeatherPeriod> _serverCallbackHandler;

        private WeatherViewUI _weatherViewUI;

        private CompositeDisposable _compositeDisposable;

        private const int REQUEST_INTERVAL_SECONDS = 5;
        public WeatherDataCollectState(IFactory<WeatherViewUI> weatherViewFactory
            ,IServerCallbackHandler<WeatherPeriod> serverCallbackHandler
            ,ServerRequestsSender serverRequestsSender)
        {
            _serverCallbackHandler = serverCallbackHandler;
            _serverRequestsSender = serverRequestsSender;
            
            _weatherViewUI = weatherViewFactory.Create();
            var weatherController = new WeatherController(serverCallbackHandler, _weatherViewUI);
        }
        public void Enter()
        {
            _compositeDisposable = new CompositeDisposable();
            _weatherViewUI.gameObject.SetActive(true);

            SendGetWeatherDataRequest(); //Не уверен надо ли без ожидания 5и секунд делать, не увидел в ТЗ, но по логике надо
            Observable
                .Interval(TimeSpan.FromSeconds(REQUEST_INTERVAL_SECONDS))
                .Subscribe(l =>SendGetWeatherDataRequest())
                .AddTo(_compositeDisposable);
        }

        private void SendGetWeatherDataRequest()
        {
            var request = new WeatherServerRequest(_serverCallbackHandler.HandleServerCallback);
            _serverRequestsSender.AddRequest(request);
        }
        
        public void Exit()
        {
            _weatherViewUI.gameObject.SetActive(false);
            _compositeDisposable?.Dispose();
            _serverRequestsSender.TryCancelRequestWithURL(WeatherServerRequest.WEATHER_URL);
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }
        public void SetStateMachine(GameStateMachine stateMachine)
        {
        }
    }
}