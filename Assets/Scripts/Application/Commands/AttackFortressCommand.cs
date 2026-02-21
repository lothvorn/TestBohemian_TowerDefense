using Application.Interfaces;
using Core.MessageBus;

namespace Application.Commands
{
    public readonly struct AttackFortressCommand : ICommand
    {
        public IEnemyLifecycle EnemyLifecycle { get; }
        public int Damage { get; }

        public AttackFortressCommand(IEnemyLifecycle enemyLifecycle, int damage)
        {
            Damage = damage;
            EnemyLifecycle = enemyLifecycle;
        }
    }
}