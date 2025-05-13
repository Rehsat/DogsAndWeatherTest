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
            _stateMachine.EnterState<WeatherDataCollectState>();
        }

        public void Exit()
        {
        }
    }
}