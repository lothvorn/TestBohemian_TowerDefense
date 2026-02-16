using Core;
using Framework.UI;

namespace Framework.Runtime.GameStates
{
    public class LevelGameplayState : GameStateBase
    {
        private readonly LevelGameplayView _levelGameplayView;
        public LevelGameplayState(
            GameStateMachine gameStateMachine, 
            LevelGameplayView levelGamplayView) 
            : base(gameStateMachine)
        {
            _levelGameplayView = levelGamplayView;
        }

        public override void Enter()
        {
            _levelGameplayView.gameObject.SetActive(true);
            _levelGameplayView.EndGameClicked += GoToNextState;
        }

        public override void Exit()
        {
            _levelGameplayView.EndGameClicked -= GoToNextState;
            _levelGameplayView.gameObject.SetActive(false);
        }

    }
}