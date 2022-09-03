using UnityEngine;
using UnityEngine.Events;

namespace Softweather.Player
{
    [RequireComponent(typeof(DeathHandler))]
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private float maxPlayerHealth = 100f;

        private DeathHandler deathHandler;
        private float currentPlayerHealth;

        public event UnityAction<float, float> HealhtChanged;
        public event UnityAction PlayerDied;

        private void Awake()
        {
            deathHandler = GetComponent<DeathHandler>();
        }

        private void Start()
        {
            currentPlayerHealth = maxPlayerHealth;
        }

        public void TakeDamage(float damage)
        {
            currentPlayerHealth -= damage;
            HealhtChanged?.Invoke(currentPlayerHealth, maxPlayerHealth);

            if (currentPlayerHealth <= 0)
            {
                deathHandler.HandleDeath();
                PlayerDied?.Invoke();
            }
        }
    }
}
