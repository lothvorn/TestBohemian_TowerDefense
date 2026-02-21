using Application.Commands;
using Application.Interfaces;
using Core.MessageBus;
using Framework.Runtime.Services;
using UnityEngine;
using UnityEngine.Events;

namespace Framework.Runtime.Towers
{
    public class TowerView : MonoBehaviour
    {
        private IMessageBus _messageBus;
        private EnemiesService _enemiesService;
        private TowerDefinition _definition;

       [SerializeField] private UnityEvent<Transform> towerShot;
        private float nextFiretime;

        public void Initialize(IMessageBus messageBus, EnemiesService enemiesService, TowerDefinition definition)
        {
            _messageBus = messageBus;
            _enemiesService = enemiesService;
            _definition = definition;

            nextFiretime = Time.time + _definition.FireDelay;
        }

        private void Update()
        {
            if (Time.time <= nextFiretime)
                return;
            
            if (_enemiesService.TryGetClosestEnemyInRange(
                    transform.position,
                    _definition.Range,
                    out IEnemyLifecycle lifecycle))
            {
                _messageBus.Publish(new DamageEnemyCommand(lifecycle, _definition.Damage));
                nextFiretime = Time.time + _definition.FireDelay;

                towerShot.Invoke(lifecycle.Transform);
            }
        }
    }
}