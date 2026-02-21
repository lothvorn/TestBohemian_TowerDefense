using System.Collections.Generic;
using Application.Events;
using Application.Interfaces;
using Core.MessageBus;
using Framework.Runtime.Towers;
using UnityEngine;

namespace Framework.Runtime.Services
{
    public class TowerService : IStartable
    {
        private readonly IMessageBus _messageBus;
        private readonly EnemiesService _enemiesService;
        private readonly List<(TowerSlot slot, TowerView towerView)> _spawnedTowers = new();

        public TowerService(IMessageBus messageBus, EnemiesService enemiesService)
        {
            _messageBus = messageBus;
            _enemiesService = enemiesService;
        }

        public void Start()
        {
            _messageBus.Subscribe<TowerBuiltEvent>(BuildTower);
        }

        public void Stop()
        {
            _messageBus.Unsubscribe<TowerBuiltEvent>(BuildTower);
        }

        public void DespawnAll()
        {
            for (int i = 0; i < _spawnedTowers.Count; i++)
            {
                var entry = _spawnedTowers[i];
                entry.slot.Clear();
                
                Object.Destroy(entry.towerView.gameObject);
            }

            _spawnedTowers.Clear();
        }

        private void BuildTower(TowerBuiltEvent eventData)
        {
            TowerSlot slot = eventData.Slot;

            if (slot.IsOccupied)
                return;

            TowerView prefab = eventData.TowerDefinition.TowerPrefab;

            TowerView towerInstance = Object.Instantiate(prefab, slot.Anchor.position, slot.Anchor.rotation);
            towerInstance.Initialize(_messageBus, _enemiesService,  eventData.TowerDefinition);
            
            slot.AssignTower(towerInstance);

            _spawnedTowers.Add((slot, towerInstance));
        }
    }
}