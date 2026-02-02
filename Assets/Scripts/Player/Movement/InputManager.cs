using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInputActions playerInputActions;

    private PlayerMovement playerMovement;
    private PlayerJump playerJump;
    private PlayerCrouch playerCrouch;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();

        playerMovement = GetComponent<PlayerMovement>();
        playerJump = GetComponent<PlayerJump>();
        playerCrouch = GetComponent<PlayerCrouch>();
    }

    private void OnEnable()
    {
        playerInputActions.Enable();

        playerInputActions.Player.Move.performed += OnMovePerformed;
        playerInputActions.Player.Move.canceled += OnMoveCanceled;

        playerInputActions.Player.Jump.performed += OnJumpPerformed;

        playerInputActions.Player.Crouch.performed += OnCrouchPerformed;

        playerInputActions.Player.Dash.performed += OnDashPerformed;
    }

    private void OnDisable()
    {
        playerInputActions.Player.Move.performed -= OnMovePerformed;
        playerInputActions.Player.Move.canceled -= OnMoveCanceled;

        playerInputActions.Player.Jump.performed -= OnJumpPerformed;

        playerInputActions.Player.Crouch.performed -= OnCrouchPerformed;

        playerInputActions.Player.Dash.performed -= OnDashPerformed;

        playerInputActions.Disable();
    }

    private void OnMovePerformed(InputAction.CallbackContext obj)
    {
        float moveDir = obj.ReadValue<float>();
        playerMovement?.SetMoveInput(moveDir);
    }

    private void OnMoveCanceled(InputAction.CallbackContext obj)
    {
        float moveDir = 0f;
        playerMovement?.SetMoveInput(moveDir);
    }

    private void OnJumpPerformed(InputAction.CallbackContext obj)
    {
        playerJump?.Jump();
    }

    private void OnCrouchPerformed(InputAction.CallbackContext obj)
    {
        playerCrouch?.ToggleCrouch();
    }

    private void OnDashPerformed(InputAction.CallbackContext obj)
    {
        playerMovement?.EnableDash();
    }
}
