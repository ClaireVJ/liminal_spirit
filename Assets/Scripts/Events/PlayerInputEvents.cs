using System;
using UnityEngine;

public class PlayerInputEvents
{
    public event Action<float> OnInputForHorizontalMove;
    public void SendHorizontalMoveInput(float moveInput)
    {
        OnInputForHorizontalMove?.Invoke(moveInput);
    }

    public event Action<float> OnInputForVerticalMove;
    public void SendVerticalMoveInput(float moveInput)
    {
        OnInputForVerticalMove?.Invoke(moveInput);
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

    public event Action OnInputForPause;
    public void SendPauseInput()
    {
        OnInputForPause?.Invoke();
    }
}
