using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private Rigidbody2D rb;

    [Header("Ground Check")]
    private bool isGrounded;
    [SerializeField] private float groundedRaycastLength;
    [SerializeField] private LayerMask groundLayers;

    [Header("Platform Check")]
    private bool underPlatform;
    [SerializeField] private float platformRaycastLength;
    [SerializeField] private LayerMask platformLayers;

    [Header("Wall Check")]
    private bool nearLeftWall;
    private bool nearRightWall;
    [SerializeField] private float wallRaycastLength;
    [SerializeField] private LayerMask wallLayers;

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

        nearLeftWall = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.left, wallRaycastLength, wallLayers);
        nearRightWall = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.right, wallRaycastLength, wallLayers);
    }

    public bool GetIsGrounded()
    {
        return isGrounded;
    }

    public bool GetUnderPlatform()
    {
        return underPlatform;
    }

    public bool GetNearLeftWall()
    {
        return nearLeftWall;
    }

    public bool GetNearRightWall()
    {
        return nearRightWall;
    }

    public float GetYVelocity()
    {
        return rb.linearVelocity.y;
    }
}
