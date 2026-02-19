using Application.Commands;
using Core.MessageBus;
using Framework.Runtime.Interfaces;
using UnityEngine;

namespace Framework.Runtime.Enemies
{
    public class FortressAttackSensor : MonoBehaviour
    {
        private IMessageBus _messageBus;
        private IGoalReachedSignal _goalReachedSignal;
        private IDamageDealer _damageDealer;
        private IEnemyLyfecicle _enemyLyfecicle;
        private void Awake()
        {
            _goalReachedSignal = GetComponent<IGoalReachedSignal>();
            _damageDealer = GetComponent<IDamageDealer>();
            _enemyLyfecicle = GetComponent<IEnemyLyfecicle>();
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
            _messageBus.Publish(new AttackFortressCommand(_damageDealer.Damage));
            
            _enemyLyfecicle.Despawn();
        }
    }
}