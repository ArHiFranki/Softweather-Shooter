using UnityEngine;

namespace Softweather.Core
{
    public class MoveCamera : MonoBehaviour
    {
        [SerializeField] private Transform cameraTransform;

        private void Update()
        {
            FollowCamera();
        }

        private void FollowCamera()
        {
            transform.position = cameraTransform.position;
        }
    }
}