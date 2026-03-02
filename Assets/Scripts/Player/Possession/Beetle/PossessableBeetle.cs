using UnityEngine;

public class PossessableBeetle : MonoBehaviour, IPossessable
{
    [SerializeField] private GameObject possessPrompt;
    private BeetleMovement beetleMovement;

    private void Awake()
    {
        possessPrompt.SetActive(false);

        beetleMovement = GetComponent<BeetleMovement>();
        beetleMovement.enabled = false;
    }

    public void DisplayPossessPrompt(bool display)
    {
        if (display == true)
        {
            possessPrompt.SetActive(true);
        }
        else
        {
            possessPrompt.SetActive(false);
        }
    }

    public void Possess()
    {
        DisplayPossessPrompt(false);
        beetleMovement.enabled = true;
    }

    public void UnPossess()
    {
        DisplayPossessPrompt(false);
        beetleMovement.enabled = false;
    }
}
