using System.Collections;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private PlayerManager playerManager;
    private Rigidbody2D rb;

    private bool onCooldown;

    [SerializeField] private float jumpCoolDown;
    [SerializeField] private float jumpForce;

    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
        rb = GetComponent<Rigidbody2D>();
        onCooldown = false;
    }

    public void Jump()
    {
        if (!onCooldown && playerManager.GetIsGrounded() && !playerManager.GetUnderPlatform())
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            GameEventsManager.instance.playerVisualEvents.PlayJumpAnim();

            onCooldown = true;
            StartCoroutine(JumpCoolDown());
        }
    }

    private IEnumerator JumpCoolDown()
    {
        yield return new WaitForSeconds(jumpCoolDown);
        onCooldown = false;
    }
}
