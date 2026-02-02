using System;
using UnityEngine;

public class PlayerSFXEvents
{
    public event Action OnPlayWalkSound;
    public void PlayWalkSound()
    {
        OnPlayWalkSound?.Invoke();
    }
}
