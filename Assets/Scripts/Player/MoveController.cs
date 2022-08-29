using UnityEngine;

namespace Softweather.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class MoveController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 10f;
        [SerializeField] private float rigidbodyDrag = 6f;
        [SerializeField] private float movementMultiplier = 10f;

        private Vector3 moveDirection;
        private Vector2 playerMoveInput;
        private Rigidbody myRigidbody;

        private void Awake()
        {
            myRigidbody = GetComponent<Rigidbody>();
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
            myRigidbody.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }

        private void DragControl()
        {
            myRigidbody.drag = rigidbodyDrag;
        }
    }
}