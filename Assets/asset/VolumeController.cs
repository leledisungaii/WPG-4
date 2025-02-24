using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider; // Reference to the UI slider
    [SerializeField] private AudioSource audioSource; // The AudioSource component to control

    private void Start()
    {
        // Load saved volume or set default volume (1.0f)
        float savedVolume = PlayerPrefs.GetFloat("volume", 1.0f);

        if (audioSource != null)
        {
            audioSource.volume = savedVolume; // Apply saved volume to the AudioSource
        }

        if (volumeSlider != null)
        {
            volumeSlider.value = savedVolume; // Set slider to reflect the current volume
            volumeSlider.onValueChanged.AddListener(SetVolume); // Add listener for slider changes
        }
    }

    // Method to adjust the volume
    public void SetVolume(float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = volume; // Set the AudioSource volume
        }

        PlayerPrefs.SetFloat("volume", volume); // Save the volume setting
    }
}
