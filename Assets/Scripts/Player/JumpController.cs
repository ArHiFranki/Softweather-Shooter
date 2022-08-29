using UnityEngine;
using static UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController;

namespace Softweather.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class JumpController : MonoBehaviour
    {
        [Header("Player jump setup")]
        [SerializeField] private CapsuleCollider playerCollider;
        [SerializeField] private float groundCheckDistance;
        [SerializeField] private float jumpForce = 5f;

        [SerializeField] private bool isGrounded;
        private bool isJumping;
        private bool previouslyGrounded;
        private Rigidbody myRigidbody;

        public bool IsGrounded => isGrounded;

        private void Awake()
        {
            myRigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            GroundCheck();
        }

        public void Jump()
        {
            if (isGrounded && !isJumping)
            {
                myRigidbody.velocity = new Vector3(myRigidbody.velocity.x, 0f, myRigidbody.velocity.z);
                myRigidbody.AddForce(new Vector3(0f, jumpForce, 0f), ForceMode.Impulse);
                isJumping = true;
            }
        }

        private void GroundCheck()
        {
            previouslyGrounded = isGrounded;
            RaycastHit hitInfo;
            if (Physics.SphereCast(transform.position, playerCollider.radius, Vector3.down, out hitInfo,
                                   ((playerCollider.height / 2f) - playerCollider.radius) + groundCheckDistance, 
                                   Physics.AllLayers, QueryTriggerInteraction.Ignore))
            {
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }
            if (!previouslyGrounded && isGrounded && isJumping)
            {
                isJumping = false;
            }
        }
    }
}