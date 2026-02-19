using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Framework.Runtime.Waves
{
    [Serializable]
    public struct WaveBurst
    {
        public int enemyIndex;
        public int count;
        public float enemySpawnDelay;
        public float burstDelay;
    }
}