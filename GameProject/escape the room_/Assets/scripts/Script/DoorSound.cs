using UnityEngine;

public class DoorSound : MonoBehaviour
{
    public AudioClip openSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayOpenSound()
    {
        if (openSound != null)
        {
            audioSource.PlayOneShot(openSound);
        }
    }
}
