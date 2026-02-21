using Core.MessageBus;
using Framework.Runtime.Towers;

namespace Application.Events
{
    public readonly struct TowerBuiltEvent : IEvent
    {
        public TowerSlot Slot { get; }
        public TowerDefinition TowerDefinition { get; }
        public int Cost { get; }

        public TowerBuiltEvent(TowerSlot slot, TowerDefinition towerDefinition, int cost)
        {
            Slot = slot;
            TowerDefinition = towerDefinition;
            Cost = cost;
        }
    }
}