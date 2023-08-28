using UnityEngine;
using UnityEngine.UI;

namespace Source.HealthComponents
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Health _health;
        [SerializeField] private Image _image;
        
        private void OnEnable()
        {
            _health.Changed += OnHealthChanged;
        }

        private void OnDisable()
        {
            _health.Changed -= OnHealthChanged;
        }

        private void OnHealthChanged(float value)
        {
            _image.fillAmount = value;
        }
    }
}
