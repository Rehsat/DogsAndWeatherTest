using System.Collections;
using Game.Server.Parsers.Dogs;
using Game.Server.Parsers.Weather;
using Game.UI.PanelSwitch;
using UniRx;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

namespace Game.GameStateMachine
{
    public class BootstrapGameState : IGameState
    {
        private readonly PanelSwitchView _panelSwitchView;
        private GameStateMachine _stateMachine;

        public BootstrapGameState(IFactory<PanelSwitchView> panelSwitchFactory)
        {
            _panelSwitchView = panelSwitchFactory.Create();
        }
        public void SetStateMachine(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine; // в теории для этого метода можно выделить абстрактный класс, но мне кажется он осбо много не даст пользы, а лишнее наследование появится
        }
        public void Enter()
        {
            var panelSwitchPresenter = new PanelSwitchPresenter(_stateMachine, _panelSwitchView);
            _stateMachine.EnterState<WeatherDataCollectState>();
        }
        public void Exit()
        {
        }
    }
}