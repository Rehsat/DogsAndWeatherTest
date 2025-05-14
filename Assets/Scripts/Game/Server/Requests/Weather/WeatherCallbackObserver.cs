using EasyFramework.ReactiveEvents;
using Game.Server.Parsers;
using Game.Server.Parsers.Weather;
using Game.Server.Requests;
using UnityEngine;
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
            _currentPeriod = result.Properties.Periods[0];
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

public class SpriteCallbackHandler : IServerCallbackHandler<Sprite>
{
    private readonly ReactiveEvent<Sprite> _onNewDataFromServer;
    public IReadOnlyReactiveEvent<Sprite> OnNewDataFromServer => _onNewDataFromServer;

    public SpriteCallbackHandler()
    {
        _onNewDataFromServer = new ReactiveEvent<Sprite>();
    }
    public void HandleServerCallback(DownloadHandler callback)
    {
        byte[] imageData = callback.data; // Получаем сырые данные
        Texture2D texture = new Texture2D(2, 2);
    
        if (texture.LoadImage(imageData)) // Автоматически определяет формат (PNG/JPG)
        {
            var newSprite = Sprite.Create(
                texture,
                new Rect(0, 0, texture.width, texture.height),
                new Vector2(0.5f, 0.5f)
            );
            _onNewDataFromServer.Notify(newSprite);
        }
        else
        {
            Debug.LogError("Failed to load texture from downloaded data");
        }
    }
}