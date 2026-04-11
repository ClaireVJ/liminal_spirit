using System.Collections;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private PlayerManager playerManager;

    [Header("Offsets")]
    private float xOffset;
    [SerializeField] private float lookAheadDistance;
    [SerializeField] private float lookSpeed;

    [SerializeField] private float yOffset;

    [Header("Damping")]
    [SerializeField] private float normalDamping;
    [SerializeField] private float fallingDamping;

    private Vector3 velocity = Vector3.zero;

    [Header("Falling")]
    private bool isFalling;
    [SerializeField] private float fallVelocity;

    // Called after the Update() method. 
    private void LateUpdate()
    {
        if (playerTransform == null)
        {
            return;
        }

        // Transition the X axis offset to the left
        if (playerTransform.localScale.x == -1f)
        {
            xOffset = Mathf.Lerp(xOffset, -lookAheadDistance, lookSpeed * Time.deltaTime);            
        }
        // Transition the X axis offset to the right
        else if(playerTransform.localScale.x == 1f)
        {
            xOffset = Mathf.Lerp(xOffset, lookAheadDistance, lookSpeed * Time.deltaTime);
        }

        // Set where the camera is looking at. Player's current position + offsets (to display more of whats above and in front of the player)
        Vector3 targetPosition = new Vector3
            (
                playerTransform.position.x + xOffset, 
                playerTransform.position.y + yOffset, 
                transform.position.z
            );

        // Check if the player is falling
        if (playerManager != null && playerManager.GetYVelocity() < fallVelocity)
        {
            isFalling = true;
        }

        // If the player is falling, remove the Y axis offset
        if (isFalling)
        {
            targetPosition.y = playerTransform.position.y;

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, fallingDamping);

            if (playerManager.GetIsGrounded() == true)
            {
                isFalling = false;
            }
        }
        else
        {
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, normalDamping);
        }
    }
}
