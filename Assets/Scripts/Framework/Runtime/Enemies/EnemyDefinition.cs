using Domain;
using UnityEngine;
using UnityEngine.Serialization;

namespace Framework.Runtime.Enemies
{
    [CreateAssetMenu(menuName = "Tower Defense/Enemies/Enemy Definition", fileName = "Enemy_")]
    public class EnemyDefinition : ScriptableObject
    {
        [Header("Presentation")]
        [SerializeField] private EnemyView _prefab;
        
        [Header("Stats")]
        [Min(0.01f)]
        [SerializeField] private float _speed = 1f;

        [FormerlySerializedAs("maxHealth")]
        [FormerlySerializedAs("_maxHp")]
        [Min(1)]
        [SerializeField] private int _maxHealth = 10;

        [Min(0)]
        [SerializeField] private int _reward = 1;
        
        [Min(1)]
        [SerializeField] private int _strength = 1;

        public EnemyView Prefab => _prefab;
        public float Speed => _speed;
        public int MaxHealth => _maxHealth;
        public int Reward => _reward;
        public int Strength => _strength;
        
        public Enemy CreateEnemyEntity()
        {
            return new Enemy(Speed, MaxHealth,Reward,Strength);
        }

        
    }
}