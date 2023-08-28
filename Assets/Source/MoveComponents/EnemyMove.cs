using System.Collections.Generic;
using Source.Common;
using Source.HealthComponents;
using Source.Spawn;
using Source.StaticData;
using UnityEngine;

namespace Source.MoveComponents
{
    public class EnemyMove : IRunable, IInitable, IDeinitable
    {
        private readonly EnemySpawner _enemySpawner;
        private readonly StaticDataProvider _staticDataProvider;
        
        private readonly Dictionary<EnemyMoveEmitter, EnemyStaticData> _enemyMoveEmitters;

        public EnemyMove(EnemySpawner enemySpawner, StaticDataProvider staticDataProvider)
        {
            _enemyMoveEmitters = new Dictionary<EnemyMoveEmitter, EnemyStaticData>();
            _enemySpawner = enemySpawner;
            _staticDataProvider = staticDataProvider;
        }
        
        public void Init()
        {
            _enemySpawner.EnemyCreated += OnEnemyCreated;
        }

        public void Deinit()
        {
            _enemySpawner.EnemyCreated -= OnEnemyCreated;
        }
        
        private void OnEnemyCreated(GameObject enemy)
        {
            if (enemy.TryGetComponent(out EnemyMoveEmitter enemyMoveEmitter))
            {
                Add(enemyMoveEmitter);
            }

            if (enemy.TryGetComponent(out Health health))
            {
                health.Died += OnEnemyDied;
            }
        }

        private void OnEnemyDied(Health enemyHealth)
        {
            enemyHealth.Died -= OnEnemyDied;
            
            if (enemyHealth.TryGetComponent(out EnemyMoveEmitter enemyMoveEmitter))
            {
                Remove(enemyMoveEmitter);
            }
        }
        
        private void Add(EnemyMoveEmitter enemyMoveEmitter)
        {
            if (_enemyMoveEmitters.ContainsKey(enemyMoveEmitter))
            {
                return;
            }
            
            _enemyMoveEmitters.Add(enemyMoveEmitter, _staticDataProvider.GetStaticDataForEnemy(enemyMoveEmitter.Id.EntityEntityType));
        }

        private void Remove(EnemyMoveEmitter enemyMoveEmitter)
        {
            if (_enemyMoveEmitters.ContainsKey(enemyMoveEmitter) == false)
            {
                return;
            }
            
            _enemyMoveEmitters.Remove(enemyMoveEmitter);
        }
        
        public void Run()
        {
            foreach (var enemyMoveEmitters in _enemyMoveEmitters)
            {
                EnemyStaticData enemyStaticData = enemyMoveEmitters.Value;
                EnemyMoveEmitter enemyMoveEmitter = enemyMoveEmitters.Key;
                
                if (enemyMoveEmitter.Target == null)
                {
                    continue;
                }
                
                Transform enemyTransform = enemyMoveEmitter.transform;

                if (Vector2.Distance(enemyTransform.position, enemyMoveEmitter.Target.position) <= enemyMoveEmitter.StopDistance)
                {
                    continue;
                }
                
                enemyTransform.position = Vector3.MoveTowards(enemyTransform.position, enemyMoveEmitter.Target.position, enemyStaticData.Speed * Time.deltaTime);
            }
        }
    }
}
