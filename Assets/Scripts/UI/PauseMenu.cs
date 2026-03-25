using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private bool isShown;

    [SerializeField] private GameObject contentParent;
    [SerializeField] private GameObject[] buttons;
    [SerializeField] private GameObject quitConfirmation;

    private void Awake()
    {
        isShown = false;
    }

    private void OnEnable()
    {
        if (GameEventsManager.instance != null)
        {
            GameEventsManager.instance.playerInputEvents.OnInputForPause += TogglePauseMenu;
        }
    }

    private void OnDisable()
    {
        if (GameEventsManager.instance != null)
        {
            GameEventsManager.instance.playerInputEvents.OnInputForPause -= TogglePauseMenu;
        }
    }

    private void TogglePauseMenu()
    {
        if (isShown)
        {
            isShown = false;
            GameEventsManager.instance.gameEvents.PauseGame(isShown);

            Close();
            contentParent.SetActive(false);
        }
        else
        {
            isShown = true;
            GameEventsManager.instance.gameEvents.PauseGame(isShown);

            contentParent.SetActive(true);
            Close();
        }
    }

    public void OpenQuitConfirmation()
    {
        SetButtons(false);

        quitConfirmation.SetActive(true);
    }

    public void Close()
    {
        SetButtons(true);

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
