using Application.Events;
using Core;
using Core.MessageBus;
using Domain;
using Framework.Runtime.Services;
using Framework.UI;

namespace Framework.Runtime.GameStates
{
    public class PlayingGameState : GameStateBase
    {
        private readonly PlayingGameView _playingGameView;
        private readonly WaveSpawner _waveSpawner;
        private readonly IMessageBus  _messageBus;
        private EnemiesService _enemiesService;

        private readonly Wallet _wallet;
        private readonly Score _score;
        private readonly Fortress _fortress;
        private readonly TowerService _towerService;

        public PlayingGameState(
            GameStateMachine gameStateMachine,
            PlayingGameView levelGamplayView,
            WaveSpawner waveSpawner,
            IMessageBus messageBus,
            EnemiesService enemiesService,
            Wallet wallet,
            Score score,
            Fortress fortress,
            TowerService towerService) 
            : base(gameStateMachine)
        {
            _playingGameView = levelGamplayView;
            _messageBus = messageBus;
            _waveSpawner = waveSpawner;
            _enemiesService = enemiesService;
            _towerService = towerService;

            _wallet = wallet;
            _score = score;
            _fortress = fortress;
        }

        public override void Enter()
        {
            _messageBus.Subscribe<FortressDestroyedEvent>(LoseGame);
            _messageBus.Subscribe<EnemyKilledEvent>(RefreshHudAfterEnemyKilled);
            _messageBus.Subscribe<EnemyReachedFortressEvent>(RefresHudAfterFortressAttacked);
            _messageBus.Subscribe<TowerBuiltEvent>(RefreshHudAfterTowerBuilt);
            _messageBus.Subscribe<AllEnemiesKilled>(WinGame);
            
            _playingGameView.EndGameClicked += GoToNextState;
            
            
            _fortress.Reset();
            _score.Reset();
            _wallet.Reset();
            _waveSpawner.StartWave();
            
            _playingGameView.gameObject.SetActive(true);
            RefreshHud();
        }

        public override void Exit()
        {
            _messageBus.Unsubscribe<FortressDestroyedEvent>(LoseGame);
            _messageBus.Unsubscribe<EnemyKilledEvent>(RefreshHudAfterEnemyKilled);
            _messageBus.Unsubscribe<EnemyReachedFortressEvent>(RefresHudAfterFortressAttacked);
            _messageBus.Unsubscribe<TowerBuiltEvent>(RefreshHudAfterTowerBuilt);
            _messageBus.Unsubscribe<AllEnemiesKilled>(WinGame);
            _playingGameView.EndGameClicked -= GoToNextState;

            _waveSpawner.StopWave();
            _enemiesService.ResetStatus();
            _towerService.DespawnAll();
            
            _playingGameView.gameObject.SetActive(false);
        }


        private void WinGame(AllEnemiesKilled eventData)
        {
            _score.GameWon = true;
            GoToNextState();
        }

        private void RefreshHud()
        {
            _playingGameView.SetGold(_wallet.Gold);
            _playingGameView.SetScore(_score.Value);
            _playingGameView.SetLives(_fortress.Lives);
        }

        private void RefreshHudAfterTowerBuilt(TowerBuiltEvent obj)
        {
            _playingGameView.SetGold(_wallet.Gold);
        }

        private void RefresHudAfterFortressAttacked(EnemyReachedFortressEvent obj)
        {
            _playingGameView.SetLives(_fortress.Lives);
        }

        private void RefreshHudAfterEnemyKilled(EnemyKilledEvent obj)
        {
            _playingGameView.SetGold(_wallet.Gold);
            _playingGameView.SetScore(_score.Value);
        }

        private void LoseGame(FortressDestroyedEvent obj)
        {
            _score.GameWon = false;
            GoToNextState();
        }
    }
}