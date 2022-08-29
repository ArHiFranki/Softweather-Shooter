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

        private float forceMultiplier;
        private Vector3 moveDirection;
        private Vector2 playerMoveInput;
        private Rigidbody myRigidbody;
        private JumpController myJumpController;

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
        }

        private void FixedUpdate()
        {
            MovePlayer();
        }

        public void SetMoveInput(Vector2 moveInput)
        {
            playerMoveInput = moveInput;
            moveDirection = transform.forward * playerMoveInput.y + transform.right * playerMoveInput.x;
        }

        private void MovePlayer()
        {
            if (myJumpController.IsGrounded)
            {
                forceMultiplier = movementMultiplier;
            }
            else
            {
                forceMultiplier = airMultiplier;
            }

            myRigidbody.AddForce(moveDirection.normalized * moveSpeed * forceMultiplier, ForceMode.Acceleration);
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
    }
}