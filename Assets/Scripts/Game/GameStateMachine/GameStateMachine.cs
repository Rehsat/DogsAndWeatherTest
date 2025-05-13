using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.GameStateMachine
{
    public class GameStateMachine : StateMachine<IGameState> //Вообще это получается стейт машина не игровая, а менюшки. Но учитывая, что менюшка это и есть вся игра, думаю такое пойдет. В полноценном проекте стоит отделить эту СМ от основной
    {
        public GameStateMachine(List<IGameState> states) : base(states)
        {
            foreach (var state in States.Values)
            {
                state.SetStateMachine(this);
            }
            EnterState<BootstrapGameState>();
        }
    }

    public interface IGameState : IState
    {
        public void SetStateMachine(GameStateMachine stateMachine);
    }
}

