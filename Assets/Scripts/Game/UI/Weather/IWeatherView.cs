using UnityEngine;

namespace Game.UI.Weather
{
    public interface IWeatherView
    {
        public void SetIcon(Sprite icon);

        public void SetText(string text);
    }
}