using Application.Interfaces;
using Core.MessageBus;

namespace Application.Events
{
    public readonly struct EnemyReachedFortressEvent : IEvent
    {
        public IEnemyLifecycle Lifecycle { get; }
        public int Damage { get; }

        public EnemyReachedFortressEvent(IEnemyLifecycle lifecycle, int damage)
        {
            Lifecycle = lifecycle;
            Damage = damage;
        }
    }
}