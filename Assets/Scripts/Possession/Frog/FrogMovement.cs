using System.Collections;
using UnityEngine;

public class FrogMovement : MonoBehaviour
{
    private PlayerManager playerManager;
    private Rigidbody2D rb;

    private Vector2 moveDirection;

    [SerializeField] private float dragFactor;

    [Header("Speed")]
    [SerializeField] private float walkSpeed;

    [Header("Jump")]
    private bool onJumpCooldown = false;

    [SerializeField] private float jumpCoolDownDur;
    [SerializeField] private float jumpForce;

    private void OnEnable()
    {
        if (GameEventsManager.instance != null)
        {
            GameEventsManager.instance.playerInputEvents.OnInputForHorizontalMove += ChangeMoveDir;
            GameEventsManager.instance.playerInputEvents.OnInputForJump += Jump;
        }
    }

    private void OnDisable()
    {
        if (GameEventsManager.instance != null)
        {
            GameEventsManager.instance.playerInputEvents.OnInputForHorizontalMove -= ChangeMoveDir;
            GameEventsManager.instance.playerInputEvents.OnInputForJump -= Jump;
        }
    }

    private void Start()
    {
        playerManager = GetComponentInChildren<PlayerManager>();
        rb = GetComponent<Rigidbody2D>();
        rb.linearDamping = 0f;
    }

    private void Update()
    {
        SpeedControl();
    }

    // Add in move physics
    private void FixedUpdate()
    {
        rb.AddForce(moveDirection * walkSpeed, ForceMode2D.Impulse);

        if (playerManager.GetIsGrounded())
        {
            GroundDrag();
        }
    }

    private void ChangeMoveDir(float moveInput)
    {
        moveDirection = new Vector2(moveInput, 0f);
        GameEventsManager.instance.playerVisualEvents.PlayMoveOrIdleAnim(moveDirection);

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

    private void Jump()
    {
        if (!onJumpCooldown && playerManager.GetIsGrounded() && !playerManager.GetUnderPlatform())
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            GameEventsManager.instance.playerVisualEvents.PlayJumpAnim();

            onJumpCooldown = true;
            StartCoroutine(JumpCoolDown());
        }
    }

    // Override when they are different speeds (crouch, sprint, etc)
    private void SpeedControl()
    {
        Vector2 speedVel = new Vector2(rb.linearVelocity.x, 0f);

        if (speedVel.magnitude > walkSpeed)
        {
            Vector2 limitedVel = speedVel.normalized * walkSpeed;
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

    private IEnumerator JumpCoolDown()
    {
        yield return new WaitForSeconds(jumpCoolDownDur);
        onJumpCooldown = false;
    }
}
