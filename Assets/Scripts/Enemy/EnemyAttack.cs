using UnityEngine;
using Softweather.Player;
using Softweather.UI;

namespace Softweather.Enemy
{
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] float damage = 25f;

        private PlayerHealth target;

        private void Awake()
        {
            target = FindObjectOfType<PlayerHealth>();
        }

        public void AttackHitEvent()
        {
            if (target == null) return;
            target.TakeDamage(damage);
            target.GetComponent<DisplayDamage>().ShowDamageImpact();
        }
    }
}