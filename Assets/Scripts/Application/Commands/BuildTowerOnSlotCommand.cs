using Core.MessageBus;
using Framework.Runtime.Towers;

namespace Application.Commands
{
    public readonly struct BuildTowerOnSlotCommand : ICommand
    {
        public TowerSlot Slot { get; }
        public TowerDefinition TowerDefinition { get; }

        public BuildTowerOnSlotCommand(TowerSlot slot, TowerDefinition towerDefinition)
        {
            Slot = slot;
            TowerDefinition = towerDefinition;
        }
    }
}