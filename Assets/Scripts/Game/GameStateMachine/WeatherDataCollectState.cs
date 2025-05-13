namespace Game.GameStateMachine
{
    public class WeatherDataCollectState : IGameState
    {
        private GameStateMachine _stateMachine;
        public void SetStateMachine(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        public void Enter()
        {
            throw new System.NotImplementedException();
        }

        public void Exit()
        {
            throw new System.NotImplementedException();
        }
    }
}