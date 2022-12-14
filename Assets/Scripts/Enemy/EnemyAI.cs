using UnityEngine;
using UnityEngine.AI;

namespace Softweather.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(EnemyHealth))]
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] private float chaseRange = 5f;
        [SerializeField] private float turnSpeed = 5f;
        [SerializeField] private float modelOffset = 0.1f;

        private Transform myTarget;
        private NavMeshAgent myNavMeshAgent;
        private Animator myAnimator;
        private EnemyHealth myEnemyHealth;
        private float distanceToTarget = Mathf.Infinity;
        private bool isProvoked = false;

        private void Awake()
        {
            myNavMeshAgent = GetComponent<NavMeshAgent>();
            myAnimator = GetComponent<Animator>();
            myEnemyHealth = GetComponent<EnemyHealth>();
        }

        private void Update()
        {
            if (myEnemyHealth.IsDead)
            {
                return;
            }
            else
            {
                EnemyBehavior();
            }
        }

        public void InitTarget(Transform target)
        {
            myTarget = target;
        }

        public void SetProvokedCondition(bool condition)
        {
            isProvoked = condition;
        }

        private void EnemyBehavior()
        {
            distanceToTarget = Vector3.Distance(myTarget.position, transform.position);

            if (isProvoked)
            {
                EngageTarget();
            }
            else if (distanceToTarget <= chaseRange)
            {
                isProvoked = true;
            }
        }

        private void EngageTarget()
        {
            FaceTarget();

            if (distanceToTarget - modelOffset > myNavMeshAgent.stoppingDistance)
            {
                ChaseTarget();
            }
            else
            {
                AttackTarget();
            }
        }

        private void ChaseTarget()
        {
            myAnimator.SetBool(AnimationTriggers.AttackBoolTrigger, false);
            myAnimator.SetTrigger(AnimationTriggers.MoveTrigger);
            myNavMeshAgent.SetDestination(myTarget.transform.position);
        }

        private void AttackTarget()
        {
            myAnimator.SetBool(AnimationTriggers.AttackBoolTrigger, true);
        }

        private void FaceTarget()
        {
            Vector3 direction = (myTarget.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, turnSpeed * Time.deltaTime);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, chaseRange);
        }
    }
}