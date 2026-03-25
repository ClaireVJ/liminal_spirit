using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject[] buttons;
    [SerializeField] private GameObject levelSelect;
    [SerializeField] private GameObject credits;
    [SerializeField] private GameObject quitConfirmation;

    public void OpenLevelSelect()
    {
        SetButtons(false);

        levelSelect.SetActive(true);
    }

    public void OpenCredits()
    {
        SetButtons(false);

        credits.SetActive(true);
    }

    public void OpenQuitConfirmation()
    {
        SetButtons(false);

        quitConfirmation.SetActive(true);
    }

    public void Close()
    {
        SetButtons(true);

        levelSelect.SetActive(false);
        credits.SetActive(false);
        quitConfirmation.SetActive(false);
    }

    private void SetButtons(bool display)
    {
        foreach (GameObject button in buttons)
        {
            button.SetActive(display);
        }
    }
}
