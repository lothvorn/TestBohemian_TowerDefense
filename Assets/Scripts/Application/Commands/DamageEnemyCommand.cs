using Application.Interfaces;
using Core.MessageBus;

namespace Application.Commands
{
    public struct DamageEnemyCommand : ICommand
    {
        public IEnemyLifecycle lifecycle { get; }
        public int damage { get; }

        public DamageEnemyCommand(
            IEnemyLifecycle lifecycle, 
            int damage)
        {
            this.lifecycle = lifecycle;
            this.damage = damage;
        }
    }
}