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

        playerInputActions.Player.HorizontalMove.performed += OnHorizontalMovePerformed;
        playerInputActions.Player.HorizontalMove.canceled += OnHorizontalMoveCanceled;

        playerInputActions.Player.VerticalMove.performed += OnVerticalMovePerformed;
        playerInputActions.Player.VerticalMove.canceled += OnVerticalMoveCanceled;

        playerInputActions.Player.Jump.performed += OnJumpPerformed;

        playerInputActions.Player.Crouch.performed += OnCrouchPerformed;

        playerInputActions.Player.Dash.performed += OnDashPerformed;

        playerInputActions.Player.Possess.performed += OnPossessPerformed;

        playerInputActions.Player.Pause.performed += OnPausePerformed;
    }

    private void OnDisable()
    {
        playerInputActions.Player.HorizontalMove.performed -= OnHorizontalMovePerformed;
        playerInputActions.Player.HorizontalMove.canceled -= OnHorizontalMoveCanceled;

        playerInputActions.Player.VerticalMove.performed -= OnVerticalMovePerformed;
        playerInputActions.Player.VerticalMove.canceled -= OnVerticalMoveCanceled;

        playerInputActions.Player.Jump.performed -= OnJumpPerformed;

        playerInputActions.Player.Crouch.performed -= OnCrouchPerformed;

        playerInputActions.Player.Dash.performed -= OnDashPerformed;

        playerInputActions.Player.Possess.performed -= OnPossessPerformed;

        playerInputActions.Player.Pause.performed -= OnPausePerformed;

        playerInputActions.Disable();
    }

    private void OnHorizontalMovePerformed(InputAction.CallbackContext obj)
    {
        if (GameManager.instance.gameIsPaused)
        {
            return;
        }

        float moveInput = obj.ReadValue<float>();
        GameEventsManager.instance.playerInputEvents.SendHorizontalMoveInput(moveInput);
    }
    private void OnHorizontalMoveCanceled(InputAction.CallbackContext obj)
    {
        if (GameManager.instance.gameIsPaused)
        {
            return;
        }

        float moveInput = 0f;
        GameEventsManager.instance.playerInputEvents.SendHorizontalMoveInput(moveInput);
    }

    private void OnVerticalMovePerformed(InputAction.CallbackContext obj)
    {
        if (GameManager.instance.gameIsPaused)
        {
            return;
        }

        float moveInput = obj.ReadValue<float>();
        GameEventsManager.instance.playerInputEvents.SendVerticalMoveInput(moveInput);
    }
    private void OnVerticalMoveCanceled(InputAction.CallbackContext obj)
    {
        if (GameManager.instance.gameIsPaused)
        {
            return;
        }

        float moveInput = 0f;
        GameEventsManager.instance.playerInputEvents.SendVerticalMoveInput(moveInput);
    }

    private void OnJumpPerformed(InputAction.CallbackContext obj)
    {
        if (GameManager.instance.gameIsPaused)
        {
            return;
        }

        GameEventsManager.instance.playerInputEvents.SendJumpInput();
    }

    private void OnCrouchPerformed(InputAction.CallbackContext obj)
    {
        if (GameManager.instance.gameIsPaused)
        {
            return;
        }

        GameEventsManager.instance.playerInputEvents.SendCrouchInput();
    }

    private void OnDashPerformed(InputAction.CallbackContext obj)
    {
        if (GameManager.instance.gameIsPaused)
        {
            return;
        }

        GameEventsManager.instance.playerInputEvents.SendDashInput();
    }

    private void OnPossessPerformed(InputAction.CallbackContext obj)
    {
        if (GameManager.instance.gameIsPaused)
        {
            return;
        }

        GameEventsManager.instance.playerInputEvents.SendPossessionInput();
    }

    private void OnPausePerformed(InputAction.CallbackContext obj)
    {
        GameEventsManager.instance.playerInputEvents.SendPauseInput();
    }
}
