using System.Collections;
using UnityEngine;

public class FoxMovement : MonoBehaviour
{
    private PlayerManager playerManager;
    private Rigidbody2D rb;

    private Vector2 moveDirection;

    [SerializeField] private float dragFactor;

    [Header("Speed")]
    [SerializeField] private float crouchSpeed;
    [SerializeField] private float walkSpeed;

    [Header("Dash")]
    [SerializeField] private float dashForce;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashCooldown;

    private bool isDashing = false;
    private bool onDashCooldown = false;

    private Vector2 dashDirection;

    private bool isCrouching = false;

    private void OnEnable()
    {
        if (GameEventsManager.instance != null)
        {
            GameEventsManager.instance.playerInputEvents.OnInputForHorizontalMove += ChangeMoveDir;
            GameEventsManager.instance.playerInputEvents.OnInputForDash += EnableDash;
            GameEventsManager.instance.playerInputEvents.OnInputForCrouch += Crouch;
        }
    }

    private void OnDisable()
    {
        if (GameEventsManager.instance != null)
        {
            GameEventsManager.instance.playerInputEvents.OnInputForHorizontalMove -= ChangeMoveDir;
            GameEventsManager.instance.playerInputEvents.OnInputForDash -= EnableDash;
            GameEventsManager.instance.playerInputEvents.OnInputForCrouch -= Crouch;
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
        if (!isDashing)
        {
            SpeedControl();
        }
    }

    // Add in move physics
    private void FixedUpdate()
    {
        if (isDashing)
        {
            rb.linearVelocity = dashDirection.normalized * dashForce;
            return;
        }

        if (isCrouching)
        {
            rb.AddForce(moveDirection * crouchSpeed, ForceMode2D.Impulse);
        }
        else if (!isCrouching)
        {
            rb.AddForce(moveDirection * walkSpeed, ForceMode2D.Impulse);
        }

        if (playerManager.GetIsGrounded() == true)
        {
            GroundDrag();
        }
    }

    private void ChangeMoveDir(float moveInput)
    {
        moveDirection = new Vector2(moveInput, 0f);
        GameEventsManager.instance.playerVisualEvents.PlayMoveOrIdleAnim(moveDirection);
        GameEventsManager.instance.playerSFXEvents.PlayWalkSound(moveDirection);

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

    private void EnableDash()
    {
        if (!onDashCooldown)
        {
            isDashing = true;
            onDashCooldown = true;

            dashDirection = new Vector2(transform.localScale.x, 0f);

            StartCoroutine(DashDuration());
        }
    }

    private void Crouch()
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

    // Override when they are different speeds (crouch, sprint, etc)
    private void SpeedControl()
    {
        Vector2 speedVel = new Vector2(rb.linearVelocity.x, 0f);

        if (!isCrouching && speedVel.magnitude > walkSpeed)
        {
            Vector2 limitedVel = speedVel.normalized * walkSpeed;
            rb.linearVelocity = new Vector2(limitedVel.x, rb.linearVelocity.y);
        }
        else if (isCrouching && speedVel.magnitude > crouchSpeed)
        {
            Vector2 limitedVel = speedVel.normalized * crouchSpeed;
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

    private IEnumerator DashDuration()
    {
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;

        StartCoroutine(DashCooldown());
    }
    private IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(dashCooldown);
        onDashCooldown = false;
    }
}
