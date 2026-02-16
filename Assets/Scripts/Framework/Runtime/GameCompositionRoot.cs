using System;
using Core;
using Core.EventBus;
using Framework.Runtime.GameStates;
using Framework.UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace Framework.Runtime
{
    public class GameCompositionRoot : MonoBehaviour
    {

        [Header("UI")]
        [SerializeField] private MainMenuView _mainMenuView;
        [SerializeField] private LevelGameplayView _levelGameplayView;
        [SerializeField] private GameOverView _gameOverView;

        private GameStateMachine gameStateMachine;
        private IEventBus eventBus;

        private MainMenuState _mainMenuState;
        private LevelGameplayState _levelGameplayState;
        private GameOverState _gameOverState;

        private void Awake()
        {
            gameStateMachine = new GameStateMachine();
            eventBus = new DictionaryEventBus();

            _mainMenuState = new MainMenuState(gameStateMachine, _mainMenuView);
            _levelGameplayState = new LevelGameplayState(gameStateMachine, _levelGameplayView);
            _gameOverState = new GameOverState(gameStateMachine, _gameOverView);
        }

        private void Start()
        {
            _mainMenuState.SetNextState(_levelGameplayState);
            _levelGameplayState.SetNextState(_gameOverState);
            _gameOverState.SetNextState(_mainMenuState);
            SetAllPanelsInactive();
            gameStateMachine.ChangeState(_mainMenuState);
        }
        
        private void SetAllPanelsInactive()
        {
            _mainMenuView.gameObject.SetActive(false);
            _levelGameplayView.gameObject.SetActive(false);
            _gameOverView.gameObject.SetActive(false);
        }
    }
}