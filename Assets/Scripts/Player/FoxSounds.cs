using UnityEngine;

public class FoxSounds : MonoBehaviour
{
    private AudioSource audioSource;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip walkingSFX;
    [SerializeField] private AudioClip landingSFX;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        if (GameEventsManager.instance != null)
        {
            GameEventsManager.instance.playerSFXEvents.OnPlayWalkSound += PlayWalkSound;
        }
    }

    private void OnDisable()
    {
        if (GameEventsManager.instance != null)
        {
            GameEventsManager.instance.playerSFXEvents.OnPlayWalkSound -= PlayWalkSound;
        }
    }

    private void PlayWalkSound(Vector2 moveDir)
    {
        if (moveDir == Vector2.zero)
        {
            audioSource.Stop();
        }
        else
        {
            audioSource.clip = walkingSFX;
            audioSource.Play();
        }
    }
}
