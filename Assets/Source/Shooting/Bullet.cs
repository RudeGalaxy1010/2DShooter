using System;
using Source.Common;
using Source.HealthComponents;
using Source.Identity;
using UnityEngine;

namespace Source.Shooting
{
    [RequireComponent(typeof(Collider2D))]
    public class Bullet : MonoBehaviour, IRunable
    {
        private const float SqrMaxDistance = 10000f;

        public event Action<Bullet> Destroyed;

        [SerializeField] private int _damage;
        [SerializeField] private float _speed;

        private EntityType _createdBy;
        private Vector3 _direction;
        
        public void Construct(EntityType createdBy, Vector3 direction)
        {
            _createdBy = createdBy;
            _direction = direction;
        }
        
        public void Run()
        {
            transform.position += _direction * (_speed * Time.deltaTime);

            if (transform.position.x * transform.position.x + transform.position.y * transform.position.y > SqrMaxDistance)
            {
                Die();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Id id) 
                && id.EntityEntityType == _createdBy)
            {
                return;
            }

            if (other.TryGetComponent(out Health health))
            {
                health.TakeDamage(_damage);
            }
            
            Die();
        }

        private void Die()
        {
            Destroyed?.Invoke(this);
            Destroy(gameObject);
        }
    }
}
