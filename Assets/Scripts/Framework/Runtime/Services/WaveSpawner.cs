using Framework.Runtime.Enemies;
using Framework.Runtime.Waves;
using UnityEngine;

namespace Framework.Runtime.Services
{
    public class WaveSpawner : MonoBehaviour
    {
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private EnemiesLibrary _enemiesLibrary;
        [SerializeField] private WaveDefinition _waveDefinition;
        
        private int _currentBurstIndex;
        private int _remainingInBurst;
        private float _nextSpawntime;

        private void Awake()
        {
            StopWave();
        }

        public void StartWave()
        {
            ResetWave();
            PrepareNextBurst();
            enabled = true;
        }

        public void StopWave()
        {
            enabled = false;
        }

        private void ResetWave()
        {
            _currentBurstIndex = 0;
            _remainingInBurst = 0;
            _nextSpawntime = 0f;
        }

        private void Update()
        {
            
            if (Time.time <= _nextSpawntime)
                return;
            SpawnEnemyFromBurst();
        }

        private void PrepareNextBurst()
        {
            if (_currentBurstIndex >= _waveDefinition.Bursts.Length)
            {
                enabled = false;
                return;
            }

            WaveBurst burst = _waveDefinition.Bursts[_currentBurstIndex];
            _remainingInBurst = burst.count;
            _nextSpawntime = Time.time + burst.burstDelay + burst.enemySpawnDelay;
        }

        private void SpawnEnemyFromBurst()
        {
            WaveBurst burst = _waveDefinition.Bursts[_currentBurstIndex];

            EnemyDefinition enemyDefinition = _enemiesLibrary.Enemies[burst.enemyIndex];
            _enemySpawner.SpawnEnemy(enemyDefinition);

            _remainingInBurst--;

            if (_remainingInBurst <= 0)
            {
                _currentBurstIndex++;
                PrepareNextBurst();
                return;
            }

            _nextSpawntime = Time.time + burst.enemySpawnDelay;
        }
    }
}