using Core.MessageBus;

namespace Application.Events
{
    public readonly struct FortressAttackedEvent : IEvent
    {
        public int Damage { get; }

        public FortressAttackedEvent(int damage)
        {
            Damage = damage;
        }
    }
}