using EasyFramework.ReactiveEvents;
using Game.Server.Parsers;
using Game.Server.Parsers.Weather;

namespace Game.Server.Requests.Weather
{
    public class WeatherCallbackHandler : IServerCallbackHandler<WeatherData>
    {
        private readonly BaseParser<WeatherData> _serverDataParser;
        private readonly ReactiveEvent<WeatherData> _onNewDataFromServer;
        
        public IReadOnlyReactiveEvent<WeatherData> OnNewDataFromServer => _onNewDataFromServer;

        public WeatherCallbackHandler()
        {
            _serverDataParser= new WeatherDataParser();
            _onNewDataFromServer = new ReactiveEvent<WeatherData>();
        }
        public void HandleServerCallback(string callback)
        {
            var result = _serverDataParser.Parse(callback);
            _onNewDataFromServer.Notify(result);
        }
    }
}