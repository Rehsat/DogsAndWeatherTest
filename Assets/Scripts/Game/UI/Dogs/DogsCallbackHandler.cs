using EasyFramework.ReactiveEvents;
using Game.Server.Parsers;
using Game.Server.Parsers.Dogs;
using Game.Server.Parsers.Weather;
using Game.Server.Requests;
using UnityEngine.Networking;

namespace Game.UI.Dogs
{
    public class DogsCallbackHandler : IServerCallbackHandler<DogBreedsDataResponse>
    {
        private WeatherPeriod _currentPeriod;
        
        private readonly BaseParser<DogBreedsDataResponse> _serverDataParser;
        private readonly ReactiveEvent<DogBreedsDataResponse> _onNewDataFromServer;
        public IReadOnlyReactiveEvent<DogBreedsDataResponse> OnNewDataFromServer => _onNewDataFromServer;

        public DogsCallbackHandler()
        {
            _serverDataParser= new BaseParser<DogBreedsDataResponse>();
            _onNewDataFromServer = new ReactiveEvent<DogBreedsDataResponse>();
        }
        public void HandleServerCallback(DownloadHandler callback)
        {
            var result = _serverDataParser.Parse(callback.text);
            _onNewDataFromServer.Notify(result);
        }
    }
}