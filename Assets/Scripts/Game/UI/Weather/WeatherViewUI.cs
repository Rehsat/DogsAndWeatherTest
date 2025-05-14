using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Weather
{
    public class WeatherViewUI : MonoBehaviour, IWeatherView
    {
        [SerializeField] private Image _weatherIcon;
        [SerializeField] private TMP_Text _weatherText;

        public void SetIcon(Sprite icon)
        {
            _weatherIcon.sprite = icon;
        }

        public void SetText(string text)
        {
            _weatherText.text = text;
        }
    }
}
