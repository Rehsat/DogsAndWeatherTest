using System.Collections;
using Game.Server.Parsers.Weather;
using UniRx;
using UnityEngine;
using UnityEngine.Networking;

namespace Game.GameStateMachine
{
    public class BootstrapGameState : IGameState
    {
        private GameStateMachine _stateMachine;
        public void SetStateMachine(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine; // в теории для этого метода можно выделить абстрактный класс, но мне кажется он осбо много не даст пользы, а лишнее наследование появится
        }
        public void Enter()
        {
            Debug.LogError(124);
            Observable.TimerFrame(1).Subscribe((l =>
            {
                CoroutineStarter.Instance.StartCoroutine2(this);
            }));
            // _stateMachine.EnterState<WeatherDataCollectState>();
        }

        public IEnumerator GetData()
        {
            var parser = new WeatherDataParser();
            using var request = UnityWebRequest.Get("https://api.weather.gov/gridpoints/TOP/32,81/forecast");
            yield return request.SendWebRequest();
            var resultServer = request.downloadHandler.text;
            var result = parser.Parse(resultServer);
        }

        public void Exit()
        {
        }
    }
}