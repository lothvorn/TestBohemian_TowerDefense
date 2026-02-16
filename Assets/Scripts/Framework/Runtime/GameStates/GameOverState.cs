using Core;
using Framework.UI;

namespace Framework.Runtime.GameStates
{
    public class GameOverState : GameStateBase
    {
        private readonly GameOverView _gameOverView;

        public GameOverState(
            GameStateMachine gameStateMachine,
            GameOverView gameOverView
        )
            : base(gameStateMachine)
        {
            _gameOverView = gameOverView;
        }

        public override void Enter()
        {
            _gameOverView.gameObject.SetActive(true);
            _gameOverView.RestartClicked += GoToNextState;
 

        }

        public override void Exit()
        {
            _gameOverView.RestartClicked -= GoToNextState;
            _gameOverView.gameObject.SetActive(false);
        }


    }
}