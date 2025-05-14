using System;

namespace Game.GameStateMachine
{
    public class DogsDataCollectState : IGameState, IDisposable
    {
        private GameStateMachine _stateMachine;

        public DogsDataCollectState()
        {
            
        }
        public void SetStateMachine(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        
        public void Enter()
        {
            
        }

        public void Exit()
        {
        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}