using UnityEngine;

namespace Softweather.Enemy
{
    [RequireComponent(typeof(Animator))]
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] private float maxEnemyHealth = 100f;

        private float currentHitPoints;
        private bool isDead = false;
        private Animator animator;

        public bool IsDead => isDead;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
            currentHitPoints = maxEnemyHealth;
        }

        public void TakeDamage(float damage)
        {
            currentHitPoints -= damage;
            if (currentHitPoints <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            if (isDead)
            {
                return;
            }

            isDead = true;
            animator.SetTrigger(AnimationTriggers.DieTrigger);
        }
    }
}