using EasyFramework.ReactiveEvents;
using Game.Server.Parsers;
using Game.Server.Parsers.Dogs;
using Game.Server.Requests;
using UnityEngine.Networking;

namespace Game.UI.Dogs
{
    public class DogBreedCallbackHandler : IServerCallbackHandler<BreedResponse>
    {
        private readonly BaseParser<BreedResponse> _serverDataParser;
        private readonly ReactiveEvent<BreedResponse> _onNewDataFromServer;
        public IReadOnlyReactiveEvent<BreedResponse> OnNewDataFromServer => _onNewDataFromServer;

        public DogBreedCallbackHandler()
        {
            _serverDataParser= new BaseParser<BreedResponse>();
            _onNewDataFromServer = new ReactiveEvent<BreedResponse>();
        }
        public void HandleServerCallback(DownloadHandler callback)
        {
            var result = _serverDataParser.Parse(callback.text);
            _onNewDataFromServer.Notify(result);
        }
    }
}