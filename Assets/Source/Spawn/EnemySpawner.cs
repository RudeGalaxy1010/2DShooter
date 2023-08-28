using System;
using Source.Common;
using Source.HealthComponents;
using Source.Identity;
using Source.MoveComponents;
using Source.StaticData;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Source.Spawn
{
    public class EnemySpawner : IRunable
    {
        public event Action<GameObject> EnemyCreated; 

        private readonly EnemySpawnerEmitter _enemySpawnerEmitter;
        private readonly GameObject _player;
        private readonly StaticDataProvider _staticDataProvider;

        private float _timer;
        
        public EnemySpawner(EnemySpawnerEmitter enemySpawnerEmitter, GameObject player, StaticDataProvider staticDataProvider)
        {
            _enemySpawnerEmitter = enemySpawnerEmitter;
            _player = player;
            _staticDataProvider = staticDataProvider;
        }

        public void Run()
        {
            if (_enemySpawnerEmitter == null)
            {
                return;
            }
            
            _timer += Time.deltaTime;

            if (_timer >= _enemySpawnerEmitter.SpawnTime)
            {
                _timer = 0f;
                CreateEnemy(EntityType.Enemy);
            }
        }
        
        private GameObject CreateEnemy(EntityType entityType)
        {
            EnemyStaticData enemyStaticData = _staticDataProvider.GetStaticDataForEnemy(entityType);
            Transform spawnPoint = _enemySpawnerEmitter.SpawnPoints[Random.Range(0, _enemySpawnerEmitter.SpawnPoints.Length)];
            GameObject enemy = Object.Instantiate(enemyStaticData.Prefab, spawnPoint.position, Quaternion.identity);

            if (enemy.TryGetComponent(out EnemyMoveEmitter enemyMoveEmitter))
            {
                enemyMoveEmitter.Target = _player.transform;
            }

            if (enemy.TryGetComponent(out Health health))
            {
                health.Construct(enemyStaticData.Health);
            }

            EnemyCreated?.Invoke(enemy);
            return enemy;
        }
    }
}
