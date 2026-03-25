using UnityEngine;

public class FrogVisual : MonoBehaviour
{
    private PlayerManager playerManager;
    private Animator animator;

    // Hashs for animator
    private int isMovingHash;
    private int jumpHash;
    private int isGroundedHash;
    private int yVelocityHash;
    private int underPlatformHash;

    private void Awake()
    {
        playerManager = FindFirstObjectByType<PlayerManager>();
        animator = GetComponent<Animator>();

        isMovingHash = Animator.StringToHash("IsMoving");
        jumpHash = Animator.StringToHash("Jump");
        isGroundedHash = Animator.StringToHash("IsGrounded");
        yVelocityHash = Animator.StringToHash("YVelocity");
        underPlatformHash = Animator.StringToHash("UnderPlatform");
    }

    private void OnEnable()
    {
        if (GameEventsManager.instance != null)
        {
            GameEventsManager.instance.playerVisualEvents.OnPlayIdleOrMoveAnim += PlayIdleOrMoveAnimation;
            GameEventsManager.instance.playerVisualEvents.OnPlayJumpAnim += PlayJumpAnimation;
        }
    }

    private void OnDisable()
    {
        if (GameEventsManager.instance != null)
        {
            GameEventsManager.instance.playerVisualEvents.OnPlayIdleOrMoveAnim -= PlayIdleOrMoveAnimation;
            GameEventsManager.instance.playerVisualEvents.OnPlayJumpAnim -= PlayJumpAnimation;
        }
    }

    private void Update()
    {
        animator.SetBool(isGroundedHash, playerManager.GetIsGrounded());
        animator.SetBool(underPlatformHash, playerManager.GetUnderPlatform());
        animator.SetFloat(yVelocityHash, playerManager.GetYVelocity());
    }

    private void PlayIdleOrMoveAnimation(Vector2 moveInput)
    {
        if (moveInput == Vector2.zero)
        {
            animator.SetBool(isMovingHash, false);
        }
        else
        {
            animator.SetBool(isMovingHash, true);
        }

    }

    private void PlayJumpAnimation()
    {
        animator.SetTrigger(jumpHash);
    }
}
