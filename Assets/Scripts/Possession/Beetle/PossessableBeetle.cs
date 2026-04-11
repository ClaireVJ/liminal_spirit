using UnityEngine;

public class PossessableBeetle : MonoBehaviour, IPossessable
{
    [SerializeField] private GameObject possessPrompt;
    private BeetleMovement beetleMovement;
    private Rigidbody2D rb;

    private void Awake()
    {
        possessPrompt.SetActive(false);

        beetleMovement = GetComponent<BeetleMovement>();
        beetleMovement.enabled = false;

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

        beetleMovement.enabled = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    public void UnPossess()
    {
        DisplayPossessPrompt(false);

        transform.localScale = Vector3.one;

        beetleMovement.enabled = false;
        rb.bodyType = RigidbodyType2D.Kinematic;
    }
}
