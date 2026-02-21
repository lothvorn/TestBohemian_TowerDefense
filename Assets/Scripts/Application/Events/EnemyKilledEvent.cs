using Application.Interfaces;
using Core.MessageBus;

namespace Application.Events
{
    public readonly struct EnemyKilledEvent : IEvent
    {
        public IEnemyLifecycle Lifecycle { get; }
        public int Reward { get; }
        public int ScoreDelta { get; }

        public EnemyKilledEvent(
            IEnemyLifecycle lifecycle, 
            int reward, 
            int scoreDelta)
        {
            Lifecycle = lifecycle;
            Reward = reward;
            ScoreDelta = scoreDelta;
        }
    }
}