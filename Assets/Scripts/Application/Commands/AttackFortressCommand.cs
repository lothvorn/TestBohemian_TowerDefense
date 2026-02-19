using Core.MessageBus;

namespace Application.Commands
{
    public readonly struct AttackFortressCommand : ICommand
    {
        public int Damage { get; }

        public AttackFortressCommand(int damage)
        {
            Damage = damage;
        }
    }
}