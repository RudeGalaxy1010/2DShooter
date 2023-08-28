using System;
using UnityEngine;

namespace Source.HealthComponents
{
    public class Health : MonoBehaviour
    {
        public event Action<float> Changed; 
        public event Action<Health> Died; 

        [HideInInspector] public int Value;
        [HideInInspector] public int MaxValue;

        public void Construct(int maxValue)
        {
            MaxValue = maxValue;
            Value = maxValue;
        }
        
        public void TakeDamage(int value)
        {
            Value -= value;
            Changed?.Invoke((float)Value / MaxValue);

            if (Value <= 0)
            {
                Die();
            }
        }
        
        private void Die()
        {
            Died?.Invoke(this);
            Destroy(gameObject);
        }
    }
}
