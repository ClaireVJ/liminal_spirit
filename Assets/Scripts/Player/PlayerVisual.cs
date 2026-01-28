using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    private PlayerManager playerManager;
    private Animator animator;

    // Hashs for animator
    private int moveInputHash;
    private int jumpHash;
    private int isCrouchingHash;

    private int isGroundedHash;
    private int underPlatformHash;
    private int yVelocityHash;

    private void Awake()
    {
        playerManager = GetComponentInParent<PlayerManager>();
        animator = GetComponent<Animator>();

        moveInputHash = Animator.StringToHash("MoveInput");
        jumpHash = Animator.StringToHash("Jump");
        isCrouchingHash = Animator.StringToHash("IsCrouching");

        isGroundedHash = Animator.StringToHash("IsGrounded");
        underPlatformHash = Animator.StringToHash("UnderPlatform");
        yVelocityHash = Animator.StringToHash("YVelocity");
    }

    private void OnEnable()
    {
        if (GameEventsManager.instance != null)
        {
            GameEventsManager.instance.playerVisualEvents.OnPlayIdleOrMoveAnim += PlayIdleOrMoveAnimation;
            GameEventsManager.instance.playerVisualEvents.OnPlayJumpAnim += PlayJumpAnimation;
            GameEventsManager.instance.playerVisualEvents.OnPlayCrouchAnim += PlayCrouchAnimation;
        }
    }

    private void OnDisable()
    {
        if (GameEventsManager.instance != null)
        {
            GameEventsManager.instance.playerVisualEvents.OnPlayIdleOrMoveAnim -= PlayIdleOrMoveAnimation;
            GameEventsManager.instance.playerVisualEvents.OnPlayJumpAnim -= PlayJumpAnimation;
            GameEventsManager.instance.playerVisualEvents.OnPlayCrouchAnim -= PlayCrouchAnimation;
        }
    }

    private void Update()
    {
        animator.SetBool(isGroundedHash, playerManager.GetIsGrounded());
        animator.SetBool(underPlatformHash, playerManager.GetUnderPlatform());
        animator.SetFloat(yVelocityHash, playerManager.GetYVelocity());
    }

    private void PlayIdleOrMoveAnimation(float moveInput)
    {
        animator.SetFloat(moveInputHash, Mathf.Abs(moveInput));
    }

    private void PlayJumpAnimation()
    {
        animator.SetTrigger(jumpHash);
    }

    private void PlayCrouchAnimation(bool isCrouching)
    {
        animator.SetBool(isCrouchingHash, isCrouching);
    }
}
