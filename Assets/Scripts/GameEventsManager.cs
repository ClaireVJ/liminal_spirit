using UnityEngine;

public class GameEventsManager : MonoBehaviour
{
    public static GameEventsManager instance {  get; private set; }

    public PlayerVisualEvents playerVisualEvents;
    public PlayerSFXEvents playerSFXEvents;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;

        playerVisualEvents = new PlayerVisualEvents();
        playerSFXEvents = new PlayerSFXEvents();
    }
}
