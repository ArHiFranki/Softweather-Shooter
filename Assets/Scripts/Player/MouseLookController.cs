using UnityEngine;

namespace Softweather.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class MouseLookController : MonoBehaviour
    {
        [SerializeField] private Camera playerCamera;
        [SerializeField] private float sensitivityX = 2f;
        [SerializeField] private float sensitivityY = 2f;
        [SerializeField] private float minX = -90f;
        [SerializeField] private float maxX = 90f;
        [SerializeField] private float smoothTime = 5f;
        [SerializeField] private bool isSmooth = true;
        [SerializeField] private bool clampVerticalRotation = true;

        private Quaternion playerTargetRotation;
        private Quaternion cameraTargetRotation;
        private Vector2 playerLookInput;
        private Rigidbody myRigidbody;

        private void Awake()
        {
            myRigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            InitMouseLook(transform, playerCamera.transform);
        }

        private void Update()
        {
            RotateView();
        }

        public void SetLookInput(Vector2 lookInput)
        {
            playerLookInput = lookInput;
        }

        private void RotateView()
        {
            if (Mathf.Abs(Time.timeScale) < float.Epsilon) return;

            float oldRotationY = transform.eulerAngles.y;
            LookRotation(transform, playerCamera.transform);
            Quaternion velocityRotation = Quaternion.AngleAxis(transform.eulerAngles.y - oldRotationY, Vector3.up);
            myRigidbody.velocity = velocityRotation * myRigidbody.velocity;
        }

        private void InitMouseLook(Transform playerTransform, Transform cameraTransform)
        {
            playerTargetRotation = playerTransform.localRotation;
            cameraTargetRotation = cameraTransform.localRotation;
        }

        private void LookRotation(Transform character, Transform camera)
        {
            float rotationY = playerLookInput.x * sensitivityX;
            float rotationX = playerLookInput.y * sensitivityY;

            playerTargetRotation *= Quaternion.Euler(0f, rotationY, 0f);
            cameraTargetRotation *= Quaternion.Euler(-rotationX, 0f, 0f);

            if (clampVerticalRotation)
            {
                cameraTargetRotation = ClampRotationAroundXAxis(cameraTargetRotation);
            }

            if (isSmooth)
            {
                character.localRotation = Quaternion.Slerp(character.localRotation, playerTargetRotation, smoothTime * Time.deltaTime);
                camera.localRotation = Quaternion.Slerp(camera.localRotation, cameraTargetRotation, smoothTime * Time.deltaTime);
            }
            else
            {
                character.localRotation = playerTargetRotation;
                camera.localRotation = cameraTargetRotation;
            }
        }

        private Quaternion ClampRotationAroundXAxis(Quaternion quaternion)
        {
            quaternion.x /= quaternion.w;
            quaternion.y /= quaternion.w;
            quaternion.z /= quaternion.w;
            quaternion.w = 1.0f;

            float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(quaternion.x);

            angleX = Mathf.Clamp(angleX, minX, maxX);

            quaternion.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

            return quaternion;
        }
    }
}