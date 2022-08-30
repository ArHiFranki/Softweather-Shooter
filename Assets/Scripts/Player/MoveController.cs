using UnityEngine;

namespace Softweather.Player
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(JumpController))]
    public class MoveController : MonoBehaviour
    {
        [Header("Player movement setup")]
        [SerializeField] private float moveSpeed = 10f;
        [SerializeField] private float groundDrag = 6f;
        [SerializeField] private float airDrag = 2f;
        [SerializeField] private float movementMultiplier = 10f;
        [SerializeField] private float airMultiplier = 0.4f;
        [SerializeField] private float slopeOffset = 0.5f;
        [SerializeField] private Transform orientation;
        [SerializeField] private CapsuleCollider playerCollider;

        private Vector3 moveDirection;
        private Vector3 slopeMoveDirection;
        private Vector2 playerMoveInput;
        private Rigidbody myRigidbody;
        private JumpController myJumpController;
        private RaycastHit slopeHit;

        private void Awake()
        {
            myRigidbody = GetComponent<Rigidbody>();
            myJumpController = GetComponent<JumpController>();
        }

        private void Start()
        {
            myRigidbody.freezeRotation = true;
        }

        private void Update()
        {
            DragControl();

            if (OnSlope())
            {
                CalculateSlopeDirection();
            }
        }

        private void FixedUpdate()
        {
            MovePlayer();
        }

        public void SetMoveInput(Vector2 moveInput)
        {
            playerMoveInput = moveInput;
        }

        private void MovePlayer()
        {
            moveDirection = orientation.forward * playerMoveInput.y + orientation.right * playerMoveInput.x;

            if (myJumpController.IsGrounded && !OnSlope())
            {
                myRigidbody.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
            }
            else if (myJumpController.IsGrounded && OnSlope())
            {
                myRigidbody.AddForce(slopeMoveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
            }
            else if (!myJumpController.IsGrounded)
            {
                myRigidbody.AddForce(moveDirection.normalized * moveSpeed * airMultiplier, ForceMode.Acceleration);
            }
        }

        private void DragControl()
        {
            if (myJumpController.IsGrounded)
            {
                myRigidbody.drag = groundDrag;
            }
            else
            {
                myRigidbody.drag = airDrag;
            }
        }

        private void CalculateSlopeDirection()
        {
            slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);
        }

        private bool OnSlope()
        {
            if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerCollider.height / 2 + slopeOffset))
            {
                if (slopeHit.normal != Vector3.up)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
    }
}