using UnityEngine;

namespace Softweather.Enemy
{
    public class DamageTaker : MonoBehaviour
    {
        [SerializeField] private EnemyHealth enemyHealth;
        [SerializeField] private float damage;

        public void DealDamage()
        {
            enemyHealth.TakeDamage(damage);
        }
    }
}