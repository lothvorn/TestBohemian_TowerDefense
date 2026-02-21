using Domain;
using Framework.Runtime.Interfaces;
using UnityEngine;

namespace Framework.Runtime.Enemies
{
    public class EnemyView : MonoBehaviour, IDamageDealer
    { 
        public Enemy enemyEntity { get; private set; }
        public int Damage => enemyEntity.Strength;


        public void SetEnemyEntity(Enemy enemyModel)
        {
            enemyEntity = enemyModel;
            gameObject.name = enemyEntity.Name;
        }
    }
}