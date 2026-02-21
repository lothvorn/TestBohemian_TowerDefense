using UnityEngine;
using UnityEngine.Serialization;

namespace Framework.Runtime.Towers
{
    [CreateAssetMenu(menuName = "Tower Defense/Towers/TowerDefinition", fileName = "Tower_")]
    public class TowerDefinition : ScriptableObject
    {
        [FormerlySerializedAs("prefab")] [SerializeField] private TowerView towerPrefab;
        
        [Min(0)]
        [SerializeField] private int baseCost = 5;
        
        [Min(0f)]
        [SerializeField] private float range = 4f;

        [FormerlySerializedAs("fireRate")]
        [Min(0.01f)]
        [SerializeField] private float fireDelay = 1f;

        [Min(1)]
        [SerializeField] private int damage = 1;

        public TowerView TowerPrefab => towerPrefab;
        public int BaseCost => baseCost;
        public float Range => range;
        public float FireDelay => fireDelay;
        public int Damage => damage;
    }
}