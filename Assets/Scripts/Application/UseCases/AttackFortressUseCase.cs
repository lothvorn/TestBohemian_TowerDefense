using Application.Commands;
using Application.Events;
using Application.Interfaces;
using Core.MessageBus;
using Domain;

namespace Application.UseCases
{
    public class AttackFortressUseCase : IStartable
    {
        private readonly Fortress _fortress;
        private readonly IMessageBus _messageBus;

        public AttackFortressUseCase(
            Fortress fortress,
            IMessageBus messageBus)
        {
            _fortress = fortress;
            _messageBus = messageBus;
        }

        public void Start()
        {
            _messageBus.Subscribe<AttackFortressCommand>(Execute);
        }

        public void Stop()
        {
            _messageBus.Unsubscribe<AttackFortressCommand>(Execute);
        }

        private void Execute(AttackFortressCommand command)
        {
            _fortress.ReceiveDamage(command.Damage);
            _messageBus.Publish(new EnemyReachedFortressEvent(command.EnemyLifecycle, command.Damage));
            
            if (_fortress.IsDestroyed())
            {
                _messageBus.Publish(new FortressDestroyedEvent());
            }
        }
    }
}