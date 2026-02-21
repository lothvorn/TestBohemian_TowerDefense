using Application.Commands;
using Application.Events;
using Application.Interfaces;
using Core.MessageBus;
using Domain;
using UnityEngine;

namespace Application.UseCases
{
    public class ApplyDamageToEnemyUseCase : IStartable
    {
        private readonly IMessageBus _messageBus;
        private readonly Wallet _wallet;
        private readonly Score _score;
        
        public ApplyDamageToEnemyUseCase(
            IMessageBus messageBus,
            Wallet wallet,
            Score score)
        {
            _messageBus = messageBus;
            _wallet = wallet;
            _score = score;
        }

        public void Start()
        {
            _messageBus.Subscribe<DamageEnemyCommand>(Execute);
        }

        public void Stop()
        {
            _messageBus.Unsubscribe<DamageEnemyCommand>(Execute);
        }

        private void Execute(DamageEnemyCommand command)
        {
            Enemy enemy = command.lifecycle.EnemyEntity;

            if (enemy.IsDead())
                return;
            
            enemy.TakeDamage(command.damage);
            Debug.Log("[USE CASE][APPLY DAMAGE] Remaining life " + enemy.CurrentHealth);
            
            if (enemy.IsDead())
            {
                int reward = enemy.Reward;
                int score = enemy.Score;

                _wallet.AddGold(reward);
                _score.Add(score);
                
                _messageBus.Publish(
                    new EnemyKilledEvent(command.lifecycle, reward, score));
            }
        }
    }
}