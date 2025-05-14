using System.Collections;
using System.Collections.Generic;
using EasyFramework.ReactiveEvents;
using Game.Factories;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.PanelSwitch
{
    public class PanelSwitchView : MonoBehaviour, IPanelSwitchView, ISimpleConstructable
    {
        [SerializeField] private List<PanelSwitchButton> _panelSwitchButtons;
        private ReactiveEvent<Panel> _onPanelSwitch;
        
        public IReadOnlyReactiveEvent<Panel> OnPanelSwitch => _onPanelSwitch;

        public void Construct()
        {
            _onPanelSwitch = new ReactiveEvent<Panel>();
            _panelSwitchButtons.ForEach( button => button.Construct(_onPanelSwitch));
        }
    }
}