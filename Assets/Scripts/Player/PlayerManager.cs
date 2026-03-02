using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    [Header("Ground Check")]
    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundedRaycastLength;
    [SerializeField] private LayerMask groundLayers;

    [Header("Platform Check")]
    [SerializeField] private bool underPlatform;
    [SerializeField] private float platformRaycastLength;
    [SerializeField] private LayerMask platformLayers;

    private void Awake()
    {
        rb = GetComponentInParent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        if (GameEventsManager.instance != null)
        {
            GameEventsManager.instance.possessionEvents.OnTransformationComplete += FindReferences;
        }
    }

    private void OnDisable()
    {
        if (GameEventsManager.instance != null)
        {
            GameEventsManager.instance.possessionEvents.OnTransformationComplete -= FindReferences;
        }
    }

    private void FindReferences(GameObject target)
    {
        rb = GetComponentInParent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.down, groundedRaycastLength, groundLayers);
        underPlatform = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.up, platformRaycastLength, platformLayers);
    }

    public bool GetIsGrounded()
    {
        return isGrounded;
    }

    public bool GetUnderPlatform()
    {
        return underPlatform;
    }

    public float GetYVelocity()
    {
        return rb.linearVelocity.y;
    }
}
