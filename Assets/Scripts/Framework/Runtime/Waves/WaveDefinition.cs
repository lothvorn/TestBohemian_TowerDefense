using UnityEngine;

namespace Framework.Runtime.Waves
{
    [CreateAssetMenu(menuName = "Tower Defense/Waves/WaveDefinition")]
    public class WaveDefinition : ScriptableObject
    {
        [SerializeField] private WaveBurst[] _bursts;

        public WaveBurst[] Bursts => _bursts;
    }
}