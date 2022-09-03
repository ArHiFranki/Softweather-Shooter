using UnityEngine;
using UnityEngine.UI;
using Softweather.Player;

namespace Softweather.UI
{
    [RequireComponent(typeof(PlayerHealth))]
    public class DisplayHealth : MonoBehaviour
    {
        [SerializeField] private Slider healthSlider;

        private PlayerHealth myPlayerHealth;

        private void Awake()
        {
            myPlayerHealth = GetComponent<PlayerHealth>();
        }

        private void Start()
        {
            healthSlider.value = 1f;
        }

        private void OnEnable()
        {
            myPlayerHealth.HealhtChanged += OnHealthChanged;
        }

        private void OnDisable()
        {
            myPlayerHealth.HealhtChanged -= OnHealthChanged;
        }

        private void OnHealthChanged(float value, float maxValue)
        {
            healthSlider.value = value / maxValue;
        }
    }
}