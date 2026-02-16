namespace Core
{
    public class GameStateMachine
    {
        private IGameState currentState;

        public IGameState CurrentState => currentState;

        public void ChangeState(IGameState nextState)
        {
            if (ReferenceEquals(currentState, nextState))
                return;

            currentState?.Exit();
            currentState = nextState;
            currentState.Enter();
        }

        public void ExitCurrentState()
        {
            currentState?.Exit();
            currentState = null;
        }
    }
}