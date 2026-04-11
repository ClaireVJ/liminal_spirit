using System;
using System.Collections.Generic;
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

    public event Action<string, List<string>, float> OnThoughtTriggered;
    public void TriggerThought(string thoughtID, List<string> thoughts, float thoughtLength)
    {
        OnThoughtTriggered?.Invoke(thoughtID, thoughts, thoughtLength);
    }

    public event Action<string> OnThoughtFinished;
    public void FinishThought(string thoughtID)
    {
        OnThoughtFinished?.Invoke(thoughtID);
    }
}
