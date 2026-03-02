using System;
using UnityEngine;

public class PlayerSoundsEvents
{
    public event Action OnPlayWalkSound;
    public void PlayWalkSound()
    {
        OnPlayWalkSound?.Invoke();
    }
}
