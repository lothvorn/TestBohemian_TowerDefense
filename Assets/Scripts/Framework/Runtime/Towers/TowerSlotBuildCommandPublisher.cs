using Application.Commands;
using Core.MessageBus;
using UnityEngine;

namespace Framework.Runtime.Towers
{
    public class TowerSlotBuildCommandPublisher : MonoBehaviour
    {
        [SerializeField] private TowerSlot towerSlot;
        [SerializeField] private TowerDefinition towerDefinition;

        private IMessageBus _messageBus;

        public void Initialize(IMessageBus messageBus)
        {
            _messageBus = messageBus;
        }

        private void Awake()
        {
            if (towerSlot == null)
                towerSlot = GetComponent<TowerSlot>();
        }

        private void OnEnable()
        {
            towerSlot.Clicked += OnTowerSlotClicked;
        }

        private void OnDisable()
        {
            towerSlot.Clicked -= OnTowerSlotClicked;
        }

        private void OnTowerSlotClicked(TowerSlot clickedSlot)
        {
            _messageBus.Publish(new BuildTowerOnSlotCommand(clickedSlot, towerDefinition));
        }
    }
}