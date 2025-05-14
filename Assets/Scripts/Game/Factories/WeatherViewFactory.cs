using Game.Configs;
using Game.UI.Weather;
using UnityEngine;
using Zenject;

namespace Game.Factories
{
    public class WeatherViewFactory : IFactory<WeatherViewUI>
    {
        private readonly Canvas _mainCanvas;
        private readonly WeatherViewUI _weatherViewUI;
        
        public WeatherViewFactory(PrefabsContainer prefabsContainer, Canvas mainCanvas)
        {
            _mainCanvas = mainCanvas;
            _weatherViewUI = prefabsContainer.GetPrefabsComponent<WeatherViewUI>(Prefab.WeatherUI);
        }
        public WeatherViewUI  Create()
        {
            return Object.Instantiate(_weatherViewUI, _mainCanvas.transform);
        }
    }
}
