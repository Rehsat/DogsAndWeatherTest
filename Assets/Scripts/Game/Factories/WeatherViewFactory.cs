using Game.Configs;
using Game.UI.Weather;
using UnityEngine;

namespace Game.Factories
{
    public class WeatherViewFactory : SimpleUIFactory<WeatherViewUI>
    {
        public WeatherViewFactory(PrefabsContainer prefabsContainer, Canvas mainCanvas) : base(prefabsContainer, mainCanvas)
        {
        }

        protected override Prefab GetPrefabType()
        {
            return Prefab.WeatherUI;
        }
    }
}
