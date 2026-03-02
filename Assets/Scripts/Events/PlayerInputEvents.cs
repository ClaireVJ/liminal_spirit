using System;
using UnityEngine;

public class PlayerInputEvents
{
    public event Action<Vector2> OnInputForMove;
    public void SendMoveInput(Vector2 moveInput)
    {
        OnInputForMove?.Invoke(moveInput);
    }

    public event Action OnInputForDash;
    public void SendDashInput()
    {
        OnInputForDash?.Invoke();
    }

    public event Action OnInputForJump;
    public void SendJumpInput()
    {
        OnInputForJump?.Invoke();
    }

    public event Action OnInputForCrouch;
    public void SendCrouchInput()
    {
        OnInputForCrouch?.Invoke();
    }

    public event Action OnInputForPossession;
    public void SendPossessionInput()
    {
        OnInputForPossession?.Invoke();
    }
}
