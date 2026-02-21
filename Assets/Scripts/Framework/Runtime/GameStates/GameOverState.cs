using Core;
using Domain;
using Framework.UI;

namespace Framework.Runtime.GameStates
{
    public class GameOverState : GameStateBase
    {
        private readonly GameOverView _gameOverView;
        private readonly Score _score;

        public GameOverState(
            GameStateMachine gameStateMachine,
            GameOverView gameOverView,
            Score score
        )
            : base(gameStateMachine)
        {
            _gameOverView = gameOverView;
            _score = score;
        }

        public override void Enter()
        {
            _gameOverView.gameObject.SetActive(true);
            _gameOverView.SetScreenValues(_score.Value, _score.GameWon);
            _gameOverView.RestartClicked += GoToNextState;
 

        }

        public override void Exit()
        {
            _gameOverView.RestartClicked -= GoToNextState;
            _gameOverView.gameObject.SetActive(false);
        }


    }
}