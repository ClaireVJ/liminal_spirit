using UnityEngine;

public class GameEventsManager : MonoBehaviour
{
    public static GameEventsManager instance {  get; private set; }

    public PlayerInputEvents playerInputEvents;
    public PlayerVisualEvents playerVisualEvents;
    public PlayerSoundsEvents playerSFXEvents;
    public PossessionEvents possessionEvents;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;

        playerInputEvents = new PlayerInputEvents();
        playerVisualEvents = new PlayerVisualEvents();
        playerSFXEvents = new PlayerSoundsEvents();
        possessionEvents = new PossessionEvents();
    }
}
