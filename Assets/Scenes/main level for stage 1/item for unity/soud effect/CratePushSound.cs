using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CratePushSound : MonoBehaviour
{
    public AudioClip pushSound;          // Drag and drop the AudioClip from the assets here
    public float soundVolume = 0.5f;     // Volume of the push sound

    private AudioSource audioSource;
    private Rigidbody rb;
    private bool isPlaying = false;      // Track if sound is currently playing

    private void Start()
    {
        // Initialize Rigidbody and AudioSource components
        rb = GetComponent<Rigidbody>();
        audioSource = gameObject.AddComponent<AudioSource>();

        // Assign the audio clip to the AudioSource
        audioSource.clip = pushSound;
        audioSource.loop = true;            // Set loop to true for continuous playback while pushing
        audioSource.volume = soundVolume;
        audioSource.playOnAwake = false;   // Don't play the sound on start
    }

    private void Update()
    {
        // Check if the crate is moving (velocity is greater than a small threshold)
        if (rb.velocity.magnitude > 0.1f)
        {
            // Start playing the sound if it's not already playing
            if (!isPlaying)
            {
                audioSource.Play();
                isPlaying = true;
            }
        }
        else
        {
            // Stop playing the sound if the crate has stopped moving
            if (isPlaying)
            {
                audioSource.Stop();
                isPlaying = false;
            }
        }
    }
}
