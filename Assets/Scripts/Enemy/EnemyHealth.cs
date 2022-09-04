using System.Collections;
using UnityEngine;
using Softweather.ObjectSpawner;

namespace Softweather.Enemy
{
    [RequireComponent(typeof(Animator))]
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] private float maxEnemyHealth = 100f;
        [SerializeField] private AnimationClip dieAnimation;

        private float currentHitPoints;
        private bool isDead = false;
        private Animator myAnimator;
        private Spawner mySpawner;

        public bool IsDead => isDead;

        private void Awake()
        {
            myAnimator = GetComponent<Animator>();
        }

        private void Start()
        {
            ResetHealth();
        }

        public void InitSpawner(Spawner spawner)
        {
            mySpawner = spawner;
        }

        public void ResetHealth()
        {
            currentHitPoints = maxEnemyHealth;
            isDead = false;
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

            StartCoroutine(DieCoroutine());
        }

        private IEnumerator DieCoroutine()
        {
            isDead = true;
            mySpawner.SpawnAdditinalEnemies();
            myAnimator.SetTrigger(AnimationTriggers.DieTrigger);
            yield return new WaitForSeconds(dieAnimation.length);
            gameObject.SetActive(false);
        }
    }
}