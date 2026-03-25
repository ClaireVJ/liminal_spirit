using UnityEngine;

public class PossessableFrog : MonoBehaviour, IPossessable
{
    [SerializeField] private GameObject possessPrompt;
    private FrogMovement frogMovement;
    private FrogVisual frogVisual;
    private Rigidbody2D rb;

    private void Awake()
    {
        possessPrompt.SetActive(false);

        frogMovement = GetComponent<FrogMovement>();
        frogMovement.enabled = false;

        frogVisual = GetComponentInChildren<FrogVisual>();
        frogVisual.enabled = false;

        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
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
        frogMovement.enabled = true;
        frogVisual.enabled = true;

        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    public void UnPossess()
    {
        DisplayPossessPrompt(false);
        frogMovement.enabled = false;
        frogVisual.enabled = false;

        rb.bodyType = RigidbodyType2D.Kinematic;
    }
}
