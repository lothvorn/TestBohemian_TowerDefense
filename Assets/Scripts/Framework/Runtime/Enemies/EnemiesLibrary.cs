using System.Collections.Generic;
using UnityEngine;

namespace Framework.Runtime.Enemies
{

    [CreateAssetMenu(menuName = "Tower Defense/Enemies/Enemies Library", fileName = "EnemiesLibrary")]
    public class EnemiesLibrary : ScriptableObject
    {
        [SerializeField] private List<EnemyDefinition> _enemies = new();

        public IReadOnlyList<EnemyDefinition> Enemies => _enemies;
    }
}