using Application.Commands;
using Application.Events;
using Application.Interfaces;
using Core.MessageBus;
using Domain;
using Framework.Runtime.Towers;

namespace Application.UseCases
{
    public class BuildTowerOnSlotUseCase : IStartable
    {
        private readonly IMessageBus _messageBus;
        private readonly Wallet _wallet;

        public BuildTowerOnSlotUseCase(IMessageBus messageBus, Wallet wallet)
        {
            _messageBus = messageBus;
            _wallet = wallet;
        }

        public void Start()
        {
            _messageBus.Subscribe<BuildTowerOnSlotCommand>(Execute);
        }

        public void Stop()
        {
            _messageBus.Unsubscribe<BuildTowerOnSlotCommand>(Execute);
        }

        private void Execute(BuildTowerOnSlotCommand command)
        {
            TowerSlot slot = command.Slot;

            if (slot.IsOccupied)
                return;

            int cost = slot.TowerCost;

            if (!_wallet.CanSpend(cost))
                return;

            _wallet.SpendGold(cost);

            _messageBus.Publish(new TowerBuiltEvent(slot, command.TowerDefinition, cost));
        }
    }
}