using Domain;
using UnityEngine;

namespace Application.Interfaces
{
    public interface IEnemyLifecycle
    {
        public void Despawn();
        Transform Transform { get; }
        Enemy EnemyEntity { get; }
        
    }
}