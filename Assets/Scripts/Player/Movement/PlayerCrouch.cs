using UnityEngine;

public class PlayerCrouch : MonoBehaviour
{
    private PlayerManager playerManager;

    private bool isCrouching = false;

    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
    }

    public void ToggleCrouch()
    {
        if (playerManager.GetUnderPlatform() == true)
        {
            Debug.Log("Under Platform Check - PlayerCrouch");
            return;
        }

        if (isCrouching)
        {
            GameEventsManager.instance.playerVisualEvents.PlayerCrouchAnim(false);
            isCrouching = false;
        }
        else
        {
            GameEventsManager.instance.playerVisualEvents.PlayerCrouchAnim(true);
            isCrouching = true;
        }
    }

    public bool GetIsCrouching()
    {
        return isCrouching;
    }
}
