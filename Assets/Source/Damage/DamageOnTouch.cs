using Source.HealthComponents;
using Source.Identity;
using UnityEngine;

namespace Source.Damage
{
    [RequireComponent(typeof(Collider2D))]
    public class DamageOnTouch : MonoBehaviour
    {
        [HideInInspector] public Health Health;
        public Id Id;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.GetComponent<Id>().EntityEntityType != Id.EntityEntityType 
                && other.gameObject.TryGetComponent(out Health health))
            {
                Health = health;
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.GetComponent<Id>().EntityEntityType != Id.EntityEntityType 
                && other.gameObject.TryGetComponent(out Health health)
                && health == Health)
            {
                Health = null;
            }
        }
    }
}
