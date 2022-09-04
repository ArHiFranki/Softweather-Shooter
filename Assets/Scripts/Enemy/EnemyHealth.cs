using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Softweather.ObjectSpawner;

namespace Softweather.Enemy
{
    [RequireComponent(typeof(EnemyAI))]
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Animator))]
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] private float maxEnemyHealth = 100f;
        [SerializeField] private AnimationClip dieAnimation;

        private float currentHitPoints;
        private bool isDead = false;
        private Animator myAnimator;
        private Spawner mySpawner;
        private EnemyAI myEnemyAI;
        private NavMeshAgent myNavMeshAgent;

        public bool IsDead => isDead;

        private void Awake()
        {
            myAnimator = GetComponent<Animator>();
            myEnemyAI = GetComponent<EnemyAI>();
            myNavMeshAgent = GetComponent<NavMeshAgent>();
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
            myEnemyAI.SetProvokedCondition(true);
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
            myEnemyAI.enabled = false;
            myNavMeshAgent.enabled = false;
            mySpawner.SpawnAdditinalEnemies();
            myAnimator.SetTrigger(AnimationTriggers.DieTrigger);
            yield return new WaitForSeconds(dieAnimation.length);
            myEnemyAI.enabled = true;
            myNavMeshAgent.enabled = true;
            myEnemyAI.SetProvokedCondition(false);
            gameObject.SetActive(false);
        }
    }
}