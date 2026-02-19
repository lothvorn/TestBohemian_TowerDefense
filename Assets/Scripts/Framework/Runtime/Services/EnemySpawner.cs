using Core;
using Core.MessageBus;
using Domain;
using Framework.Runtime.Enemies;
using Framework.Runtime.Path;
using UnityEngine;

namespace Framework.Runtime.Services
{ 
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour _pathProviderBehaviour; // must implement IPathProvider

        private IPathProvider _pathProvider;
        private IMessageBus _messageBus;
        private EnemiesService _enemiesService;

        private void Awake()
        {
            _pathProvider = (IPathProvider)_pathProviderBehaviour;
        }
        public void Initialize(IMessageBus messageBus, EnemiesService enemiesService)
        {
            _messageBus = messageBus;
            _enemiesService = enemiesService;
        }
        
        public EnemyView SpawnEnemy(EnemyDefinition enemyDefinition)
        {
            EnemyView enemyView = CreateEnemyView(enemyDefinition);
            InitializeEnemyPrefabComponents(enemyDefinition, enemyView);
            return enemyView;
        }

        private void InitializeEnemyPrefabComponents(EnemyDefinition enemyDefinition, EnemyView enemyView)
        {
            Enemy enemyEntity = enemyDefinition.CreateEnemyEntity();
            
            enemyView.SetEnemyEntity(enemyEntity);
            InitializePathFollower(enemyView, enemyEntity);

            FortressAttackSensor fortressAttackSensor = enemyView.GetComponent<FortressAttackSensor>();
            fortressAttackSensor.Initialize(_messageBus);

            IEnemyLIfecycle enemyLIfecycle = enemyView.GetComponent<IEnemyLIfecycle>();
            enemyLIfecycle.Initialize(_enemiesService);
            
            _enemiesService.Register(enemyLIfecycle);
        }

        private void InitializePathFollower(EnemyView enemyView, Enemy enemyEntity)
        {
            PathFollower pathFollower = enemyView.GetComponent<PathFollower>();
            pathFollower.Initialize(_pathProvider, enemyEntity.Speed);
        }

        private EnemyView CreateEnemyView(EnemyDefinition enemyDefinition)
        {
            EnemyView enemyViewPrefab = enemyDefinition.Prefab;

            Vector3 spawnPosition = _pathProvider.GetPoint(0);
            EnemyView enemyViewInstance = Instantiate(enemyViewPrefab, spawnPosition, Quaternion.identity);
            return enemyViewInstance;
        }
    }
}