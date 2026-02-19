using System.Collections.Generic;
using Application.Interfaces;
using Application.UseCases;
using Core;
using Core.MessageBus;
using Domain;
using Framework.Runtime.GameStates;
using Framework.UI;
using UnityEngine;

namespace Framework.Runtime.Services
{
    public class GameCompositionRoot : MonoBehaviour
    {

        [Header("Gameplay")] 
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private WaveSpawner _waveSpawner;
        
        [Header("UI")]
        [SerializeField] private MainMenuView _mainMenuView;
        [SerializeField] private PlayingGameView playingGameView;
        [SerializeField] private GameOverView _gameOverView;
        


        private EnemiesService _enemiesService = new();
        
        private Fortress _fortres;
        private MainMenuState _mainMenuState;
        private PlayingGameState _playingGameState;
        private GameOverState _gameOverState;

        
        private GameStateMachine gameStateMachine;
        private IMessageBus _messageBus;
        private readonly List<IStartable> _startables = new();
        
        private Fortress _fortress;

        private void Awake()
        {
            StartupInfrastructure();
            SetupGameStateFlowStateManchine();
            StartupDomain();
            _enemySpawner.Initialize(_messageBus, _enemiesService);

            StartupUseCases();
            
            StartupStartables();
        }

        private void OnDestroy()
        {
            TeardownStartables();
        }

        private void TeardownStartables()
        {
            for (int i = 0; i < _startables.Count; i++)
                _startables[i].Stop();
        }

        private void StartupStartables()
        {
            for (int i = 0; i < _startables.Count; i++)
            {
                _startables[i].Start();
            }
        }

        private void StartupUseCases()
        {
            AttackFortressUseCase attackFortressUseCase = new AttackFortressUseCase(_fortress, _messageBus);
            _startables.Add(attackFortressUseCase);
        }

        private void StartupDomain()
        {
            _fortress = new Domain.Fortress(5);
        }

        private void SetupGameStateFlowStateManchine()
        {
            gameStateMachine = new GameStateMachine();
            _mainMenuState = new MainMenuState(gameStateMachine, _mainMenuView);
            _playingGameState = new PlayingGameState(gameStateMachine, playingGameView, _waveSpawner, _messageBus, _enemiesService);
            _gameOverState = new GameOverState(gameStateMachine, _gameOverView);
        }

        private void StartupInfrastructure()
        {
            _messageBus = new DictionaryMessageBus();
        }

        private void Start()
        {
            _mainMenuState.SetNextState(_playingGameState);
            _playingGameState.SetNextState(_gameOverState);
            _gameOverState.SetNextState(_mainMenuState);
            SetAllPanelsInactive();
            gameStateMachine.ChangeState(_mainMenuState);
        }
        
        private void SetAllPanelsInactive()
        {
            _mainMenuView.gameObject.SetActive(false);
            playingGameView.gameObject.SetActive(false);
            _gameOverView.gameObject.SetActive(false);
        }
    }
}