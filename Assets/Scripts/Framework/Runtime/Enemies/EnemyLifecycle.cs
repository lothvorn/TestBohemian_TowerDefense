using Application.Interfaces;
using Domain;
using Framework.Runtime.Services;
using UnityEngine;

namespace Framework.Runtime.Enemies
{
    public class EnemyLifecycle : MonoBehaviour, IEnemyLifecycle
    {
        private EnemiesService _enemiesService;
        public Transform Transform => transform;
        public Enemy EnemyEntity { get; private set; }


        public void Initialize (EnemiesService enemiesService, Enemy enemyEntity)
        {
            _enemiesService = enemiesService;
            EnemyEntity = enemyEntity;
        }

        public void Despawn()
        {
            _enemiesService.Unregister(this);
            Destroy(transform.root.gameObject);
        }
    }
}