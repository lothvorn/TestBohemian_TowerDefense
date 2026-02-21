using Core;

namespace Framework.Runtime.GameStates
{
    public class GameStateBase : IGameState
    {
        private GameStateMachine _gameStateMachine;
        
        private IGameState _nextState;
        
        public GameStateBase(GameStateMachine gameStateMachine)
        {
            this._gameStateMachine = gameStateMachine;
        }

        public void SetNextState(IGameState nextState)
        {
            _nextState = nextState;
        }
        
        public void GoToNextState()
        {
            _gameStateMachine.ChangeState(_nextState);
        }
        
        public virtual void Enter()
        {
            
        }

        public virtual void Exit()
        {
         
        }
    }
}