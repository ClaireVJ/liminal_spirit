using UnityEngine;

public class UIAudio : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField] private AudioClip buttonPressed;
    [SerializeField] private float buttonPressedVolScale;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudio()
    {
        audioSource.PlayOneShot(buttonPressed, buttonPressedVolScale);
    }
}
