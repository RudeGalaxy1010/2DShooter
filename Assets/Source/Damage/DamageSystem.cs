using System.Collections.Generic;
using Source.Common;
using Source.HealthComponents;
using Source.Spawn;
using Source.StaticData;
using UnityEngine;

namespace Source.Damage
{
    public class DamageSystem : IInitable, IDeinitable, IRunable
    {
        private readonly DamageSystemEmitter _damageSystemEmitter;
        private readonly StaticDataProvider _staticDataProvider;
        private readonly EnemySpawner _enemySpawner;

        private readonly Dictionary<DamageOnTouch, EnemyStaticData> _damageOnTouchComponents;
        private float _timer;

        public DamageSystem(DamageSystemEmitter damageSystemEmitter, StaticDataProvider staticDataProvider, EnemySpawner enemySpawner)
        {
            _damageSystemEmitter = damageSystemEmitter;
            _staticDataProvider = staticDataProvider;
            _enemySpawner = enemySpawner;
            _damageOnTouchComponents = new Dictionary<DamageOnTouch, EnemyStaticData>();
        }
        
        public void Init()
        {
            _enemySpawner.EnemyCreated += OnEnemyCreated;
        }
        
        private void OnEnemyCreated(GameObject enemy)
        {
            if (enemy.TryGetComponent(out DamageOnTouch damageOnTouch))
            {
                Add(damageOnTouch);
            }

            if (enemy.TryGetComponent(out Health health))
            {
                health.Died += OnEnemyDied;
            }
        }

        private void OnEnemyDied(Health enemyHealth)
        {
            enemyHealth.Died -= OnEnemyDied;
            
            if (enemyHealth.TryGetComponent(out DamageOnTouch damageOnTouch))
            {
                Remove(damageOnTouch);
            }
        }
        
        private void Add(DamageOnTouch damageOnTouch)
        {
            if (_damageOnTouchComponents.ContainsKey(damageOnTouch))
            {
                return;
            }
            
            _damageOnTouchComponents.Add(damageOnTouch, _staticDataProvider.GetStaticDataForEnemy(damageOnTouch.Id.EntityEntityType));
        }
        
        private void Remove(DamageOnTouch damageOnTouch)
        {
            if (_damageOnTouchComponents.ContainsKey(damageOnTouch) == false)
            {
                return;
            }

            _damageOnTouchComponents.Remove(damageOnTouch);
        }

        public void Run()
        {
            _timer += Time.deltaTime;

            if (_timer < _damageSystemEmitter.CooldownTime)
            {
                return;
            }
            
            _timer = 0;
            foreach (var damageOnTouchComponent in _damageOnTouchComponents)
            {
                if (damageOnTouchComponent.Key.Health != null)
                {
                    damageOnTouchComponent.Key.Health.TakeDamage(damageOnTouchComponent.Value.DamagePerTouch);
                }   
            }
        }
        
        public void Deinit()
        {
            _enemySpawner.EnemyCreated -= OnEnemyCreated;
        }
    }
}
