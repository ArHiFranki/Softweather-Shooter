using UnityEngine;

namespace Softweather.Player
{
    [RequireComponent(typeof(DeathHandler))]
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private float maxPlayerHealth = 100f;

        private DeathHandler deathHandler;
        private float currentPlayerHealth;

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
            if (currentPlayerHealth <= 0)
            {
                deathHandler.HandleDeath();
            }
        }
    }
}
