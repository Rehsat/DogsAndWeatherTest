using System;
using System.Linq;
using EasyFramework.ReactiveEvents;
using Game.Server.Parsers;
using Game.Server.Parsers.Weather;
using UnityEngine.Networking;

namespace Game.Server.Requests.Weather
{
    public class WeatherCallbackHandler : IServerCallbackHandler<WeatherPeriod>
    {
        private readonly ServerRequestsSender _serverRequestsSender;
        private readonly SpriteCallbackHandler _spriteCallbackHandler;
        private readonly BaseParser<WeatherData> _serverDataParser;
        private readonly ReactiveEvent<WeatherPeriod> _onNewDataFromServer;
        
        private WeatherPeriod _currentPeriod;
        public IReadOnlyReactiveEvent<WeatherPeriod> OnNewDataFromServer => _onNewDataFromServer;

        public WeatherCallbackHandler(
            ServerRequestsSender serverRequestsSender)
        {
            _serverRequestsSender = serverRequestsSender;
            
            _serverDataParser= new WeatherDataParser();
            _onNewDataFromServer = new ReactiveEvent<WeatherPeriod>();
            _spriteCallbackHandler = new SpriteCallbackHandler();

            _spriteCallbackHandler.OnNewDataFromServer.SubscribeWithSkip(newSprite=>
            {
                if(_currentPeriod == null) return;
                _currentPeriod.SetSprite(newSprite);
            });
        }
        public void HandleServerCallback(DownloadHandler callback)
        {
            var result = _serverDataParser.Parse(callback.text);
            var orderedPeriods = result.Properties.Periods
                .OrderBy(period => period.StartTime)
                .ToList();
    
            var now = DateTimeOffset.UtcNow; // буквально в последнюю минуту сгенерировал нейронкой XD, вроде работает. От сюда
            _currentPeriod = orderedPeriods.FirstOrDefault(p => now >= p.StartTime && now <= p.EndTime);

            if (_currentPeriod == null)
            {
                _currentPeriod = orderedPeriods.FirstOrDefault(p => p.StartTime > now) 
                                 ?? orderedPeriods.Last(); 
            }
                                                         // вот до сюда
            _onNewDataFromServer.Notify(_currentPeriod);
            SendGetSpriteRequest(_currentPeriod.Icon);
        }

        private void SendGetSpriteRequest(string spriteURL)
        {
            var request = new ServerRequest(spriteURL, _spriteCallbackHandler.HandleServerCallback);
            _serverRequestsSender.AddRequest(request);
        }
    }
}