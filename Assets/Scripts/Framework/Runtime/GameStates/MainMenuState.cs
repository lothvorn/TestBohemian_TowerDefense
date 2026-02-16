using Core;
using Framework.UI;
using UnityEngine;

namespace Framework.Runtime.GameStates
{
    public class MainMenuState : GameStateBase
    {
        private readonly MainMenuView _mainMenuView;
        public MainMenuState(
            GameStateMachine gameStateMachine, 
            MainMenuView mainMenuView) 
            : base(gameStateMachine)
        {
            _mainMenuView = mainMenuView;
        }

        public override void Enter()
        {
            _mainMenuView.gameObject.SetActive(true);
            _mainMenuView.StartClicked += GoToNextState;
        }

        public override void Exit()
        { 
            _mainMenuView.StartClicked -= GoToNextState;
            _mainMenuView.gameObject.SetActive(false);
        }
    }
}