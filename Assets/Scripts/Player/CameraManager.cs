using System.Collections;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private PlayerManager playerManager;
    private Camera mainCamera;

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

    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        if (playerTransform == null)
        {
            return;
        }

        if (playerTransform.localScale.x == -1f)
        {
            xOffset = Mathf.Lerp(xOffset, -lookAheadDistance, lookSpeed * Time.deltaTime);
            
        }
        else if(playerTransform.localScale.x == 1f)
        {
            xOffset = Mathf.Lerp(xOffset, lookAheadDistance, lookSpeed * Time.deltaTime);
        }

        Vector3 targetPosition = new Vector3
            (
                playerTransform.position.x + xOffset, 
                playerTransform.position.y + yOffset, 
                transform.position.z
            );

        if (playerManager.GetYVelocity() < fallVelocity)
        {
            isFalling = true;
        }

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
