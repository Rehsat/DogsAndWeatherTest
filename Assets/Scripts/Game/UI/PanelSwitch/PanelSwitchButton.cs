using EasyFramework.ReactiveEvents;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.PanelSwitch
{
    public class PanelSwitchButton : MonoBehaviour
    {
        [SerializeField] private Panel _panelToSwtchType;
        [SerializeField] private Button _button;

        private ReactiveEvent<Panel> _onSwitchPanel;
    
        public void Construct(ReactiveEvent<Panel> onSwitchPanel)
        {
            _onSwitchPanel = onSwitchPanel;
            _button.onClick.AddListener(SendCallback);
        }

        private void SendCallback()
        {
            _onSwitchPanel.Notify(_panelToSwtchType);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(SendCallback);
        }
    }
}