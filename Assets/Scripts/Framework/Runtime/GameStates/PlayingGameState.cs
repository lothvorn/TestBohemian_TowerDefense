using Application.Events;
using Core;
using Core.MessageBus;
using Framework.Runtime.Enemies;
using Framework.Runtime.Services;
using Framework.Runtime.Waves;
using Framework.UI;

namespace Framework.Runtime.GameStates
{
    public class PlayingGameState : GameStateBase
    {
        private readonly PlayingGameView _playingGameView;
        private readonly WaveSpawner _waveSpawner;
        IMessageBus _messageBus;
        private EnemiesService _enemiesService;

        public PlayingGameState(
            GameStateMachine gameStateMachine,
            PlayingGameView levelGamplayView,
            WaveSpawner waveSpawner,
            IMessageBus messageBus,
            EnemiesService enemiesService) 
            : base(gameStateMachine)
        {
            _playingGameView = levelGamplayView;
            _messageBus = messageBus;
            _waveSpawner = waveSpawner;
            _enemiesService = enemiesService;
        }

        public override void Enter()
        {
            _playingGameView.gameObject.SetActive(true);
            _messageBus.Subscribe<FortressDestroyedEvent>(GoToNextState);
            
            _playingGameView.EndGameClicked += GoToNextState;

            _waveSpawner.StartWave();
        }

        private void GoToNextState(FortressDestroyedEvent obj)
        {
            GoToNextState();
        }

        public override void Exit()
        {
            _messageBus.Unsubscribe<FortressDestroyedEvent>(GoToNextState);
            _playingGameView.EndGameClicked -= GoToNextState;
            
            _waveSpawner.StopWave();
            _enemiesService.DespawnAll();
            _playingGameView.gameObject.SetActive(false);
        }

    }
}