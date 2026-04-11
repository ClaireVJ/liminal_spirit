using System;
using UnityEngine;

public class PlayerSoundsEvents
{
    public event Action<Vector2> OnPlayWalkSound;
    public void PlayWalkSound(Vector2 moveDir)
    {
        OnPlayWalkSound?.Invoke(moveDir);
    }
}
