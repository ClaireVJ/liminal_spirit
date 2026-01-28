using System;
using UnityEngine;

public class PlayerVisualEvents
{
    public event Action<float> OnPlayIdleOrMoveAnim;
    public void PlayMoveOrIdleAnim(float moveInput)
    {
        OnPlayIdleOrMoveAnim?.Invoke(moveInput);
    }

    public event Action OnPlayJumpAnim;
    public void PlayJumpAnim()
    {
        OnPlayJumpAnim?.Invoke();
    }

    public event Action<bool> OnPlayCrouchAnim;
    public void PlayerCrouchAnim(bool isCrouching)
    {
        OnPlayCrouchAnim?.Invoke(isCrouching);
    }
}
