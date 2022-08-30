using UnityEngine;
using UnityEngine.InputSystem;

namespace Softweather.Player
{
    [RequireComponent(typeof(MoveController))]
    [RequireComponent(typeof(FireController))]
    [RequireComponent(typeof(JumpController))]
    [RequireComponent(typeof(MouseLookController))]
    public class PlayerInputController : MonoBehaviour
    {
        private MoveController myMoveController;
        private FireController myFireController;
        private JumpController myJumpController;
        private MouseLookController myMouseLookController;
        private Vector2 moveInput;
        private Vector2 lookInput;

        private void Awake()
        {
            myMoveController = GetComponent<MoveController>();
            myFireController = GetComponent<FireController>();
            myJumpController = GetComponent<JumpController>();
            myMouseLookController = GetComponent<MouseLookController>();
        }

        private void OnMove(InputValue value)
        {
            moveInput = value.Get<Vector2>();
            myMoveController.SetMoveInput(moveInput);
        }

        private void OnLook(InputValue value)
        {
            lookInput = value.Get<Vector2>();
            myMouseLookController.SetLookInput(lookInput);
        }

        private void OnFire(InputValue value)
        {
            if (value.isPressed)
            {
                myFireController.Fire();
            }
        }

        private void OnJump(InputValue value)
        {
            if (value.isPressed)
            {
                myJumpController.Jump();
            }
        }
    }
}