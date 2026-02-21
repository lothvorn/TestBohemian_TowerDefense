using Application.Commands;
using Application.Interfaces;
using Core.MessageBus;
using Framework.Runtime.Interfaces;
using UnityEngine;

namespace Framework.Runtime.Enemies
{
    public class FortressAttackCommandPublisher : MonoBehaviour
    {
        private IMessageBus _messageBus;
        private IGoalReachedSignal _goalReachedSignal;
        private IDamageDealer _damageDealer;
        private IEnemyLifecycle _enemyLifecycle;
        private void Awake()
        {
            _goalReachedSignal = GetComponent<IGoalReachedSignal>();
            _damageDealer = GetComponent<IDamageDealer>();
            _enemyLifecycle = GetComponent<IEnemyLifecycle>();
        }
        
        public void Initialize(IMessageBus messageBus)
        {
            _messageBus = messageBus;
        }

        private void OnEnable()
        {
            _goalReachedSignal.GoalReached += AttackFortress;
        }

        private void OnDisable()
        {
            _goalReachedSignal.GoalReached -= AttackFortress;
        }

        private void AttackFortress()
        {
            //var xyz = new AttackFortressCommand(_enemyLifecycle, 1);
            _messageBus.Publish(new AttackFortressCommand(_enemyLifecycle, _damageDealer.Damage));
            //_messageBus.Publish(new EnemyReachedFortressEvent(_enemyLifecycle));
            //_enemyLifecycle.Despawn();
        }
    }
}