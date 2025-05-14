using System;
using System.Collections;
using Game.Server.Parsers;
using Game.Server.Parsers.Dogs;
using Game.Server.Parsers.Weather;
using Game.Server.Requests;
using Game.UI.Weather;
using UniRx;
using UnityEngine.Networking;
using Zenject;

namespace Game.GameStateMachine
{
    public class WeatherDataCollectState : IGameState, IDisposable
    {
        //TODO Выделить отдельную сущность под запросы реквестов
        private readonly RequestSendHandler _requestSendHandler;
        private readonly BaseParser<GeoFeature> _serverDataParser;

        private GameStateMachine _stateMachine;
        private WeatherViewUI _weatherViewUI;
        private WeatherController _weatherController;

        private CompositeDisposable _compositeDisposable;

        private const int REQUEST_INTERVAL_SECONDS = 1;
        public WeatherDataCollectState(IFactory<WeatherViewUI> weatherViewFactory, RequestSendHandler requestSendHandler)
        {
            _requestSendHandler = requestSendHandler;
            _serverDataParser = new BaseParser<GeoFeature>();
            _compositeDisposable = new CompositeDisposable();
            
            _weatherViewUI = weatherViewFactory.Create();
            _weatherController = new WeatherController(_weatherViewUI);
        }
        public void SetStateMachine(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        public void Enter()
        {
            _weatherViewUI.gameObject.SetActive(true);
        
            Observable
                .Interval(TimeSpan.FromSeconds(REQUEST_INTERVAL_SECONDS))
                .Subscribe(l =>SendGetWeatherDataRequest())
                .AddTo(_compositeDisposable);
        }

        private void SendGetWeatherDataRequest()
        {
            var request = new WeatherRequestSender(HandleWeatherDataCallback);
            _requestSendHandler.AddRequest(request);
        }

        private void HandleWeatherDataCallback(string callback)
        {
            var result = _serverDataParser.Parse(callback);
            _weatherController.SetData(result);
        }
        
        public void Exit()
        {
            _weatherViewUI.gameObject.SetActive(false);
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }
    }
    
}