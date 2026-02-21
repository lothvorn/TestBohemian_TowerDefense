using System.Collections.Generic;
using Application.Interfaces;
using Application.UseCases;
using Core;
using Core.MessageBus;
using Domain;
using Framework.Runtime.GameStates;
using Framework.Runtime.Towers;
using Framework.UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace Framework.Runtime.Services
{
    public class GameCompositionRoot : MonoBehaviour
    {

        [FormerlySerializedAs("_enemySpawner")]
        [Header("Gameplay")] 
        [SerializeField] private EnemySpawner _enemySpawnerService;
        [SerializeField] private WaveSpawner _waveSpawner;
        
        [Header("UI")]
        [SerializeField] private MainMenuView _mainMenuView;
        [SerializeField] private PlayingGameView playingGameView;
        [SerializeField] private GameOverView _gameOverView;
        
        [Header("Constant command publishers")]
        [SerializeField] private TowerSlotBuildCommandPublisher [] _towerSlotBuildCommandPublishers;

        private EnemiesService _enemiesService;
        private TowerService _towerService;

        private Fortress _fortress;
        private Wallet _wallet;
        private Score _score;
        
        private MainMenuState _mainMenuState;
        private PlayingGameState _playingGameState;
        private GameOverState _gameOverState;


        private GameStateMachine gameStateMachine;
        private IMessageBus _messageBus;
        private readonly List<IStartable> _startables = new();


        private void Awake()
        {
            StartupInfrastructure();
            CreateEntities();
            StartupServices();
            SetupGameStateFlowStateManchine();
            StartupUseCases();
            StartupStartables();
        }

        public void CreateEntities()
        {
            _wallet = new Wallet(10);
            _score = new Score();
            _fortress = new Fortress(10);
        }
        private void StartupServices()
        {
            _waveSpawner.Initialize(_messageBus);
            
            _enemiesService = new EnemiesService(_messageBus);
            _startables.Add(_enemiesService);
            _enemySpawnerService.Initialize(_messageBus, _enemiesService);

            _towerService = new TowerService(_messageBus, _enemiesService);
            _startables.Add(_towerService);

            for (int i = 0; i < _towerSlotBuildCommandPublishers.Length; i++)
                _towerSlotBuildCommandPublishers[i].Initialize(_messageBus); 
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

            ApplyDamageToEnemyUseCase applyDamageToEnemyUseCase =
                new ApplyDamageToEnemyUseCase(_messageBus, _wallet, _score);
            _startables.Add(applyDamageToEnemyUseCase);

            BuildTowerOnSlotUseCase buildTowerOnSlotUseCase = new BuildTowerOnSlotUseCase(_messageBus, _wallet);
            _startables.Add(buildTowerOnSlotUseCase);
        }
        
        private void SetupGameStateFlowStateManchine()
        {
            gameStateMachine = new GameStateMachine();
            _mainMenuState = new MainMenuState(gameStateMachine, _mainMenuView);
            
            _playingGameState = new PlayingGameState(
                gameStateMachine, 
                playingGameView, 
                _waveSpawner, 
                _messageBus, 
                _enemiesService, 
                _wallet,_score,
                _fortress,
                _towerService);
            
            _gameOverState = new GameOverState(gameStateMachine, _gameOverView, _score);
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