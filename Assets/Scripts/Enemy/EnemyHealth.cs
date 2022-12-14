using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Softweather.ObjectSpawner;

namespace Softweather.Enemy
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(EnemyAI))]
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(CollidersController))]
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
        private CollidersController myCollidersController;

        public bool IsDead => isDead;

        private void Awake()
        {
            myAnimator = GetComponent<Animator>();
            myEnemyAI = GetComponent<EnemyAI>();
            myNavMeshAgent = GetComponent<NavMeshAgent>();
            myCollidersController = GetComponent<CollidersController>();
        }

        private void Start()
        {
            ResetEnemy();
        }

        public void InitSpawner(Spawner spawner)
        {
            mySpawner = spawner;
        }

        public void ResetEnemy()
        {
            currentHitPoints = maxEnemyHealth;
            isDead = false;
            SetEnemyAICondition(true);
            myCollidersController.SetColliderCondition(true);
            myEnemyAI.SetProvokedCondition(false);
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
            SetEnemyAICondition(false);
            myCollidersController.SetColliderCondition(false);
            myAnimator.SetTrigger(AnimationTriggers.DieTrigger);
            mySpawner.SpawnAdditinalEnemies();
            yield return new WaitForSeconds(dieAnimation.length);
            gameObject.SetActive(false);
        }

        private void SetEnemyAICondition(bool condition)
        {
            myEnemyAI.enabled = condition;
            myNavMeshAgent.enabled = condition;
        }
    }
}