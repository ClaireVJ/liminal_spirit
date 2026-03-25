using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance {  get; private set; }

    public bool gameIsPaused { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
    }

    private void OnEnable()
    {
        if (GameEventsManager.instance != null)
        {
            GameEventsManager.instance.gameEvents.OnPlayerFinishLevel += LoadScene;
            GameEventsManager.instance.gameEvents.OnGamePaused += SetGameIsPaused;
        }
    }

    private void OnDisable()
    {
        if (GameEventsManager.instance != null)
        {
            GameEventsManager.instance.gameEvents.OnPlayerFinishLevel -= LoadScene;
            GameEventsManager.instance.gameEvents.OnGamePaused -= SetGameIsPaused;
        }
    }

    public void LoadScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }

    private void SetGameIsPaused(bool isPaused)
    {
        gameIsPaused = isPaused;
    }

    public void QuitGame()
    {
        Debug.Log("Quit the game");
        Application.Quit();
    }
}
