using UnityEngine;

public class Bush : MonoBehaviour
{
    public AudioClip soundEffect;   // Select the AudioClip directly from your assets
    public float soundDuration = 1f; // Adjustable duration in seconds

    private AudioSource audioSource;

    private void Start()
    {
        // Create a temporary AudioSource component to play the sound
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false; // Prevent it from playing on startup
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player collided with the object
        if (other.CompareTag("Player") && soundEffect != null)
        {
            // Assign the audio clip to the AudioSource
            audioSource.clip = soundEffect;
            audioSource.Play();

            // Stop the sound after the specified duration
            Invoke("StopSound", soundDuration);
        }
    }

    private void StopSound()
    {
        audioSource.Stop();
    }
}
