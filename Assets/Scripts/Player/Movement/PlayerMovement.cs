using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerManager playerManager;
    private PlayerCrouch playerCrouch;
    private Rigidbody2D rb;

    private Vector2 moveDirection;

    [Header("Speed")]
    [SerializeField] private float normalSpeed;
    [SerializeField] private float crouchSpeed;

    [Header("Dash")]
    private bool isDashing;
    private bool onCooldown;

    private Vector2 dashDirection;
    [SerializeField] private float dashForce;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashCooldown;

    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
        playerCrouch = GetComponent<PlayerCrouch>();
        rb = GetComponent<Rigidbody2D>();

        onCooldown = false;
    }


    private void Update()
    {
        SpeedControl();
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            rb.linearVelocity = dashDirection.normalized * dashForce;

            return;
        }

        if (playerCrouch.GetIsCrouching() == false)
        {
            rb.AddForce(moveDirection * normalSpeed, ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(moveDirection * crouchSpeed, ForceMode2D.Impulse);
        }

    }

    public void SetMoveInput(float moveDir)
    {
        moveDirection = new Vector2(moveDir, 0f);
        GameEventsManager.instance.playerVisualEvents.PlayMoveOrIdleAnim(moveDir);

        if (moveDir < 0f)
        {
            playerManager.FlipCharacter(false);
        }
        else if (moveDir > 0f)
        {
            playerManager.FlipCharacter(true);
        }
    }

    public void EnableDash()
    {
        if (!onCooldown)
        {
            isDashing = true;
            onCooldown = true;

            dashDirection = new Vector2(transform.localScale.x, 0f);

            StartCoroutine(DashDuration());
        }
    }

    private void SpeedControl()
    {
        Vector2 speedVel = new Vector2(rb.linearVelocity.x, 0f);

        if (playerCrouch.GetIsCrouching() == false && speedVel.magnitude > normalSpeed)
        {
            Vector2 limitedVel = speedVel.normalized * normalSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y);
        }
        else if (playerCrouch.GetIsCrouching() == true && speedVel.magnitude > crouchSpeed)
        {
            Vector2 limitedVel = speedVel.normalized * crouchSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y);
        }
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
        onCooldown = false;
    }
}
