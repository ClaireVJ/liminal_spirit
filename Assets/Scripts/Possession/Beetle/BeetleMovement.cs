using System.Collections;
using UnityEngine;

public class BeetleMovement : MonoBehaviour
{
    private PlayerManager playerManager;
    private Rigidbody2D rb;

    private Vector2 horizontalDirection;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float dragFactor;

    [Header("Climb")]
    [SerializeField] private float climbSpeed;
    [SerializeField] private float climbDelay;
    [SerializeField] private float normalGravityScale;


    private Vector2 verticalDirection;

    private void Start()
    {
        playerManager = GetComponentInChildren<PlayerManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        if (GameEventsManager.instance != null)
        {
            GameEventsManager.instance.playerInputEvents.OnInputForHorizontalMove += HorizontalMove;
            GameEventsManager.instance.playerInputEvents.OnInputForVerticalMove += VerticalMove;
        }
    }

    private void OnDisable()
    {
        if (GameEventsManager.instance != null)
        {
            GameEventsManager.instance.playerInputEvents.OnInputForHorizontalMove -= HorizontalMove;
            GameEventsManager.instance.playerInputEvents.OnInputForVerticalMove -= VerticalMove;
        }
    }

    private void HorizontalMove(float moveInput)
    {
        horizontalDirection.x = moveInput;

        if (moveInput < 0)
        {
            Vector3 newScale = new Vector3(-1, 1, 1);
            playerManager.transform.localScale = newScale;
            transform.localScale = newScale;
        }
        else if (moveInput > 0)
        {
            Vector3 newScale = new Vector3(1, 1, 1);
            playerManager.transform.localScale = newScale;
            transform.localScale = newScale;
        }
    }

    private void VerticalMove(float moveInput)
    {
        verticalDirection.y = moveInput;
    }

    private void Update()
    {
        SpeedControl();
    }

    private void FixedUpdate()
    {
        if (CheckWall())
        {
            rb.gravityScale = 0;
            rb.linearVelocity = verticalDirection.normalized * climbSpeed;
        }
        else
        {
            rb.gravityScale = normalGravityScale;
        }

        rb.AddForce(horizontalDirection * moveSpeed, ForceMode2D.Impulse);

        if (playerManager.GetIsGrounded())
        {
            GroundDrag();
        }
    }

    // Override when they are different speeds (crouch, sprint, etc)
    private void SpeedControl()
    {
        Vector2 speedVel = new Vector2(rb.linearVelocity.x, 0f);

        if (speedVel.magnitude > moveSpeed)
        {
            Vector2 limitedVel = speedVel.normalized * moveSpeed;
            rb.linearVelocity = new Vector2(limitedVel.x, rb.linearVelocity.y);
        }
    }

    private void GroundDrag()
    {
        var horizontalVelocity = rb.linearVelocity;
        horizontalVelocity.y = 0f;

        // Calculate and apply a counter-force
        var dragForce = -horizontalVelocity * horizontalVelocity.magnitude * dragFactor;
        rb.AddForce(dragForce, ForceMode2D.Force);
    }

    private bool CheckWall()
    {
        bool canClimb = false;

        if (playerManager.GetNearLeftWall())
        {
            canClimb = true;
        }
        else if (playerManager.GetNearRightWall())
        {
            canClimb = true;
        }

        return canClimb;
    }
}
