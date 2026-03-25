using UnityEngine;

public interface IPossessable
{
    public void DisplayPossessPrompt(bool display);

    public void Possess();

    public void UnPossess();
}
