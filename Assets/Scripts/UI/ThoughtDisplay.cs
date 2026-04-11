using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ThoughtDisplay : MonoBehaviour
{
    private Animator animator;
    private TextMeshProUGUI thoughtText;
    private bool hasThought;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        thoughtText = GetComponentInChildren<TextMeshProUGUI>();

        thoughtText.gameObject.SetActive(false);
        hasThought = false;
    }

    private void OnEnable()
    {
        if (GameEventsManager.instance != null)
        {
            GameEventsManager.instance.gameEvents.OnThoughtTriggered += GetThoughtInfo;
        }
    }

    private void OnDisable()
    {
        if (GameEventsManager.instance != null)
        {
            GameEventsManager.instance.gameEvents.OnThoughtTriggered -= GetThoughtInfo;
        }
    }

    private void GetThoughtInfo(string id, List<string> thought, float thoughtLength)
    {
        if (hasThought)
        {
            return;
        }

        StartCoroutine(DisplayThought(id, thought, thoughtLength));
    }

    private IEnumerator DisplayThought(string id, List<string> thoughts, float thoughtLength)
    {
        hasThought = true;
        thoughtText.gameObject.SetActive(true);

        foreach (string thought in thoughts)
        {
            thoughtText.text = thought;

            animator.Play("TextFadeIn");

            yield return new WaitForSeconds(thoughtLength);

            animator.Play("TextFadeOut");

            yield return new WaitForSeconds(thoughtLength);
        }

        thoughtText.text = null;
        thoughtText.gameObject.SetActive(false);

        GameEventsManager.instance.gameEvents.FinishThought(id);
        hasThought = false;
    }
}
