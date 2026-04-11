using System.Collections.Generic;
using UnityEngine;

public class ThoughtTrigger : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";

    [SerializeField] private string id;

    [TextArea(1,3)]
    [SerializeField] private List<string> thoughts;
    [SerializeField] private float thoughtLength = 2f;

    private bool seenThought = false;

    private void OnEnable()
    {
        if (GameEventsManager.instance != null)
        {
            GameEventsManager.instance.gameEvents.OnThoughtFinished += FinishThought;
        }
    }

    private void OnDisable()
    {
        if (GameEventsManager.instance != null)
        {
            GameEventsManager.instance.gameEvents.OnThoughtFinished -= FinishThought;
        }
    }

    private void FinishThought(string thoughtID)
    {
        if (thoughtID == id)
        {
            seenThought = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!seenThought)
        {
            if (collision.gameObject.tag == PLAYER_TAG)
            {
                GameEventsManager.instance.gameEvents.TriggerThought(id, thoughts, thoughtLength);
            }
        }

    }
}
