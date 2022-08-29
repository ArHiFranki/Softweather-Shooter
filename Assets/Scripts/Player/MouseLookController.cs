using UnityEngine;

namespace Softweather.Player
{
    public class MouseLookController : MonoBehaviour
    {
        private Vector2 playerLookInput;

        public void SetLookInput(Vector2 lookInput)
        {
            playerLookInput = lookInput;
            Debug.Log("playerLookInput: " + lookInput);
        }
    }
}