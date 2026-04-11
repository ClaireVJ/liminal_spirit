using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PossessionManager : MonoBehaviour
{
    private PlayerManager playerManager;
    private List<Transform> possessablesWithinReach;

    [SerializeField] private PossessionModes currentPossessionMode;
    [SerializeField] private Transform currentPossessedBeing;

    [SerializeField] private GameObject ghostPrefab;
    [SerializeField] private float managerOffset;
    [SerializeField] private float instantiateXOffset;
    [SerializeField] private float instantiateYOffset;

    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();

        possessablesWithinReach = new List<Transform>();
        currentPossessionMode = PossessionModes.NON_POSSESSED;
    }

    private void OnEnable()
    {
        if (GameEventsManager.instance != null)
        {
            GameEventsManager.instance.playerInputEvents.OnInputForPossession += AttemptPossession;
        }
    }

    private void OnDisable()
    {
        if (GameEventsManager.instance != null)
        {
            GameEventsManager.instance.playerInputEvents.OnInputForPossession -= AttemptPossession;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collider2D currentPossessedCollider = null;
        if (currentPossessedBeing != null)
        {
            currentPossessedCollider = currentPossessedBeing.GetComponent<Collider2D>();
        }

        if (collision != currentPossessedCollider && collision.TryGetComponent<IPossessable>(out IPossessable possessable))
        {
            possessable.DisplayPossessPrompt(true);
            possessablesWithinReach.Add(collision.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IPossessable>(out IPossessable possessable))
        {
            possessable.DisplayPossessPrompt(false);
            possessablesWithinReach.Remove(collision.transform);
        }
    }

    private void AttemptPossession()
    {
        if (possessablesWithinReach != null)
        {
            // When you default fox while possessables are nearby, possess possessables
            if (currentPossessionMode == PossessionModes.NON_POSSESSED && possessablesWithinReach.Count <= 0)
            {
                Debug.Log("No possessables nearby");
                return;
            }
            // When you default fox while no possessables are nearby, nothing (stay fox)
            else if (currentPossessionMode == PossessionModes.NON_POSSESSED && possessablesWithinReach.Count >= 1)
            {
                DestroyGhost();

                currentPossessedBeing = GetClosestPossessable();
                PossessObject(currentPossessedBeing);
            }
            // When you possess something then press possess while no possessables are nearby, become fox
            else if (currentPossessionMode == PossessionModes.POSSESSED && possessablesWithinReach.Count <= 0)
            {
                // Make sure you are not in the air, not under platform, not against the wall
                if (playerManager.GetIsGrounded() == true && playerManager.GetUnderPlatform() == false)
                {
                    UnposessObject(currentPossessedBeing);

                    BecomeGhost();
                }
            }
            // When you posses something the press posses when there are possessables, become other possessables
            else if (currentPossessionMode == PossessionModes.POSSESSED && possessablesWithinReach.Count >= 1)
            {
                UnposessObject(currentPossessedBeing);

                currentPossessedBeing = GetClosestPossessable();
                PossessObject(currentPossessedBeing);
            }
        }
    }

    private void PossessObject(Transform possessableTransform)
    {
        transform.position = possessableTransform.position - new Vector3(0, managerOffset);
        transform.parent = possessableTransform;

        if (possessableTransform.TryGetComponent(out IPossessable possessable))
        {
            possessable.Possess();
        }

        currentPossessionMode = PossessionModes.POSSESSED;
        GameEventsManager.instance.possessionEvents.CompleteTransformation(possessableTransform.gameObject);
    }

    private void UnposessObject(Transform currentPossessable)
    {
        if (currentPossessable.TryGetComponent(out IPossessable possessable))
        {
            possessable.UnPossess();
        }

        currentPossessedBeing = null;
        transform.parent = null;
    }

    private void BecomeGhost()
    {
        Vector2 instantiatePos = transform.position += GetInstaniateOffset();

        GameObject ghostForm = GameObject.Instantiate(ghostPrefab, instantiatePos, Quaternion.identity);

        transform.position = ghostForm.transform.position - new Vector3(0, managerOffset);
        transform.parent = ghostForm.transform;

        currentPossessionMode = PossessionModes.NON_POSSESSED;
        GameEventsManager.instance.possessionEvents.CompleteTransformation(ghostForm);
    }

    private void DestroyGhost()
    {
        GameObject ghostForm = transform.parent.gameObject;
        transform.parent = null;

        Destroy(ghostForm);
    }

    private Transform GetClosestPossessable()
    {
        Transform chosenPossessable = null;

        if (possessablesWithinReach.Count >= 2)
        {
            Transform closestPossessableTransform = null;
            float closestDistance = Mathf.Infinity;

            foreach (Transform possessableTransform in possessablesWithinReach)
            {
                float possessableDistance = Vector3.Distance(transform.position, possessableTransform.position);

                if (possessableDistance < closestDistance)
                {
                    closestPossessableTransform = possessableTransform;
                    closestDistance = possessableDistance;
                }
            }

            chosenPossessable = closestPossessableTransform;
        }
        else
        {
            chosenPossessable = possessablesWithinReach[0];
        }

        possessablesWithinReach.Clear();
        return chosenPossessable;
    }

    private Vector3 GetInstaniateOffset()
    {
        Vector3 instantiatePos = new Vector3(instantiateXOffset, instantiateYOffset, 0f);

        if (playerManager.GetNearRightWall())
        {
            instantiatePos = new Vector3(-instantiateXOffset, instantiateYOffset, 0f);
        }

        return instantiatePos;
    }
}
