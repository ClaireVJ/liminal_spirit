using System;
using UnityEngine;

public class GameEvents
{
    public event Action<int> OnPlayerFinishLevel;
    public void GoNextLevel(int buildIndex)
    {
        OnPlayerFinishLevel?.Invoke(buildIndex);
    }

    public event Action<bool> OnGamePaused;
    public void PauseGame(bool isPaused)
    {
        OnGamePaused?.Invoke(isPaused);
    }
}
