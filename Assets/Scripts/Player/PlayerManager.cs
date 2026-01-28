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

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.down, groundedRaycastLength, groundLayers);
        underPlatform = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.up, platformRaycastLength, platformLayers);
    }

    public void FlipCharacter(bool turnRight)
    {
        if (turnRight)
        {
            Vector3 newScale = new Vector3(1, 1, 1);
            transform.localScale = newScale;
        }
        else
        {
            Vector3 newScale = new Vector3(-1, 1, 1);
            transform.localScale = newScale;
        }
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
