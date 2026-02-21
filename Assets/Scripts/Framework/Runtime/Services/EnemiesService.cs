using System.Collections.Generic;
using Application.Events;
using Application.Interfaces;
using Core.MessageBus;
using UnityEngine;

namespace Framework.Runtime.Services
{
    public class EnemiesService : IStartable
    {
        private readonly List<IEnemyLifecycle> _aliveEnemies = new();
        private readonly IMessageBus _messageBus;

        private bool allWavesSpawned;
        public EnemiesService(IMessageBus messageBus)
        {
            _messageBus = messageBus;
            
        }
        public void Start()
        {
            _messageBus.Subscribe<EnemyKilledEvent>(ReactToEnemyKilled);
            _messageBus.Subscribe<EnemyReachedFortressEvent>(ReactToReachedFortress);
            _messageBus.Subscribe<AllWavesSpawned>(FlagAllWavesSpawned);
        }

        public void Stop()
        {
            _messageBus.Unsubscribe<EnemyKilledEvent>(ReactToEnemyKilled);
            _messageBus.Unsubscribe<EnemyReachedFortressEvent>(ReactToReachedFortress);
            _messageBus.Unsubscribe<AllWavesSpawned>(FlagAllWavesSpawned);
        }

        private void FlagAllWavesSpawned(AllWavesSpawned obj)
        {
            allWavesSpawned = true;
        }

        public void Register(IEnemyLifecycle enemy)
        {
            _aliveEnemies.Add(enemy);
        }

        public void Unregister(IEnemyLifecycle enemy)
        {
            _aliveEnemies.Remove(enemy);
        }

        public void ResetStatus()
        {
            for (int i = _aliveEnemies.Count - 1; i >= 0; i--)
            {
                _aliveEnemies[i].Despawn();
            }

            _aliveEnemies.Clear();

            allWavesSpawned = false;
        }



        private void ReactToReachedFortress(EnemyReachedFortressEvent eventData)
        {
            eventData.Lifecycle.Despawn();
            
            TryPublishAllEnemiesCleared();
        }

        private void TryPublishAllEnemiesCleared()
        {
            if (!allWavesSpawned)
                return;
            if (_aliveEnemies.Count != 0)
                return;
            
            _messageBus.Publish(new AllEnemiesKilled());
        }


        private void ReactToEnemyKilled(EnemyKilledEvent eventData)
        {
            eventData.Lifecycle.Despawn();

            TryPublishAllEnemiesCleared();
        }

        public bool TryGetClosestEnemyInRange(
            Vector3 originPosition,
            float range,
            out IEnemyLifecycle closestEnemyLifecycle)
        {
            closestEnemyLifecycle = null;

            float rangeSquared = range * range;
            float closestDistanceSquared = float.MaxValue;

            for (int index = _aliveEnemies.Count - 1; index >= 0; index--)
            {
                IEnemyLifecycle candidateLifecycle = _aliveEnemies[index];

                Vector3 candidatePosition = candidateLifecycle.Transform.position;
                float distanceSquared = (candidatePosition - originPosition).sqrMagnitude;

                if (distanceSquared > rangeSquared)
                    continue;

                if (distanceSquared >= closestDistanceSquared)
                    continue;

                closestDistanceSquared = distanceSquared;
                closestEnemyLifecycle = candidateLifecycle;
            }

            return closestEnemyLifecycle != null;
        }
    }
}