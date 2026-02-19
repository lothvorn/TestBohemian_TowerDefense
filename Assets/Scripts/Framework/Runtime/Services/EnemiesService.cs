using System.Collections.Generic;
using Framework.Runtime.Interfaces;

namespace Framework.Runtime.Services
{
    public class EnemiesService
    {
        private readonly List<IEnemyLyfecicle> _aliveEnemies = new();

        public void Register(IEnemyLyfecicle enemy)
        {
            _aliveEnemies.Add(enemy);
        }

        public void Unregister(IEnemyLyfecicle enemy)
        {
            _aliveEnemies.Remove(enemy);
        }

        public void DespawnAll()
        {
            for (int i = _aliveEnemies.Count - 1; i >= 0; i--)
            {
                _aliveEnemies[i].Despawn();
            }

            _aliveEnemies.Clear();
        }
    }
}