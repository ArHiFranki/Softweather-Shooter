using UnityEngine;

namespace Softweather.Enemy
{
    public class CollidersController : MonoBehaviour
    {
        [SerializeField] private CapsuleCollider enemyHeadCollider;
        [SerializeField] private CapsuleCollider enemyBodyCollider;
        [SerializeField] private Transform enemyHeadTransform;
        [SerializeField] private Transform enemyBodyTransform;

        private void Update()
        {
            SetCollidersPositionAndRotation();
        }

        public void SetColliderCondition(bool condition)
        {
            enemyHeadCollider.enabled = condition;
            enemyBodyCollider.enabled = condition;
        }

        private void SetCollidersPositionAndRotation()
        {
            enemyHeadCollider.transform.position = enemyHeadTransform.position;
            enemyHeadCollider.transform.rotation = enemyHeadTransform.rotation;

            enemyBodyCollider.transform.position = enemyBodyTransform.position;
            enemyBodyCollider.transform.rotation = enemyBodyTransform.rotation;
        }
    }
}