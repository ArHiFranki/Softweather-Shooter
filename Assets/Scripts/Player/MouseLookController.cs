using UnityEngine;

namespace Softweather.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class MouseLookController : MonoBehaviour
    {
        [Header("Player mouse look setup")]
        [SerializeField] private Transform playerCamera;
        [SerializeField] private Transform orientation;
        [SerializeField] private float sensitivityX = 2f;
        [SerializeField] private float sensitivityY = 2f;
        [SerializeField] private float multiplier = 0.01f;
        [SerializeField] private float rotationXMin = -90f;
        [SerializeField] private float rotationXMax = 90f;

        private Vector2 playerLookInput;
        private float rotationX;
        private float rotationY;

        private void Start()
        {
            LockCursor();
        }

        private void Update()
        {
            LookRotation();
            RotateView();
            RotatePlayer();
        }

        public void SetLookInput(Vector2 lookInput)
        {
            playerLookInput = lookInput;
        }

        private void LookRotation()
        {
            rotationY += playerLookInput.x * sensitivityX * multiplier;
            rotationX -= playerLookInput.y * sensitivityY * multiplier;

            rotationX = Mathf.Clamp(rotationX, rotationXMin, rotationXMax);
        }

        private void RotateView()
        {
            playerCamera.rotation = Quaternion.Euler(rotationX, rotationY, 0f);
        }

        private void RotatePlayer()
        {
            orientation.rotation = Quaternion.Euler(0f, rotationY, 0f);
        }

        private void LockCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}