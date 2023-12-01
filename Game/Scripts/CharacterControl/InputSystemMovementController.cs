using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.CharacterControl
{

    [RequireComponent(typeof(CharacterMovement)), DisallowMultipleComponent]
    public class InputSystemMovementController : MonoBehaviour
    {

        private CharacterMovement movement;

        private void Awake()
        {
            movement = GetComponent<CharacterMovement>();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            movement.MoveDirection = context.ReadValue<Vector2>();
        }

    }

}