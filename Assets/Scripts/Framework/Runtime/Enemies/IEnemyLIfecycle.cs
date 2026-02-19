using Framework.Runtime.Interfaces;
using Framework.Runtime.Services;
using UnityEngine;

namespace Framework.Runtime.Enemies
{
    public class IEnemyLIfecycle : MonoBehaviour, IEnemyLyfecicle
    {
        private EnemiesService _enemiesService;
        
        public void Initialize (EnemiesService enemiesService)
        {
            _enemiesService = enemiesService;
        }
        
        public void Despawn()
        {
            _enemiesService.Unregister(this);
            Destroy(transform.root.gameObject);
        }
    }
}