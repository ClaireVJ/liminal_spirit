using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        playerInputActions.Enable();

        playerInputActions.Player.Move.performed += OnMovePerformed;
        playerInputActions.Player.Move.canceled += OnMoveCanceled;

        playerInputActions.Player.Jump.performed += OnJumpPerformed;

        playerInputActions.Player.Crouch.performed += OnCrouchPerformed;

        playerInputActions.Player.Dash.performed += OnDashPerformed;

        playerInputActions.Player.Possess.performed += OnPossessPerformed;
    }

    private void OnDisable()
    {
        playerInputActions.Player.Move.performed -= OnMovePerformed;
        playerInputActions.Player.Move.canceled -= OnMoveCanceled;

        playerInputActions.Player.Jump.performed -= OnJumpPerformed;

        playerInputActions.Player.Crouch.performed -= OnCrouchPerformed;

        playerInputActions.Player.Dash.performed -= OnDashPerformed;

        playerInputActions.Player.Possess.performed -= OnPossessPerformed;

        playerInputActions.Disable();
    }

    private void OnMovePerformed(InputAction.CallbackContext obj)
    {
        Vector2 moveDir = obj.ReadValue<Vector2>();
        GameEventsManager.instance.playerInputEvents.SendMoveInput(moveDir);
    }

    private void OnMoveCanceled(InputAction.CallbackContext obj)
    {
        Vector2 moveDir = Vector2.zero;
        GameEventsManager.instance.playerInputEvents.SendMoveInput(moveDir);
    }

    private void OnJumpPerformed(InputAction.CallbackContext obj)
    {
        GameEventsManager.instance.playerInputEvents.SendJumpInput();
    }

    private void OnCrouchPerformed(InputAction.CallbackContext obj)
    {
        GameEventsManager.instance.playerInputEvents.SendCrouchInput();
    }

    private void OnDashPerformed(InputAction.CallbackContext obj)
    {
        GameEventsManager.instance.playerInputEvents.SendDashInput();
    }

    private void OnPossessPerformed(InputAction.CallbackContext obj)
    {
        GameEventsManager.instance.playerInputEvents.SendPossessionInput();
    }
}
