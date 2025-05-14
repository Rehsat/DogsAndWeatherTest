using System;
using System.Collections.Generic;
using Game.GameStateMachine;
using UnityEngine;

namespace Game.UI.PanelSwitch
{
    public class PanelSwitchPresenter
    {
        private readonly GameStateMachine.GameStateMachine _gameStateMachine;
        private readonly IPanelSwitchView _panelSwitchView;

        private Dictionary<Panel, Type> _stateByPanelType;

        public PanelSwitchPresenter(GameStateMachine.GameStateMachine gameStateMachine, IPanelSwitchView panelSwitchView)
        {
            _gameStateMachine = gameStateMachine;
            _panelSwitchView = panelSwitchView;
            _stateByPanelType = new Dictionary<Panel, Type>()
            {
                {Panel.Weather, typeof(WeatherDataCollectState)},
                {Panel.Dogs, typeof(DogsDataCollectState)}
            };
            
            _panelSwitchView.OnPanelSwitch.SubscribeWithSkip(OnPanelSwitch);
        }

        private void OnPanelSwitch(Panel panelType)
        {
            var state = _stateByPanelType[panelType];
            _gameStateMachine.TryEnterState(state);
        }
    }
}