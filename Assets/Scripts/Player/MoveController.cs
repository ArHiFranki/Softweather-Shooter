using UnityEngine;

namespace Softweather.Player
{
    public class MoveController : MonoBehaviour
    {
        private Vector2 playerMoveInput;

        public void SetMoveInput(Vector2 moveInput)
        {
            playerMoveInput = moveInput;
            Debug.Log("playerMoveInput: " + moveInput);
        }
    }
}