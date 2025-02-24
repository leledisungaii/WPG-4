using UnityEngine;
using UnityEngine.UI;

public class ResolutionManager : MonoBehaviour
{
    public Text resolutionText;  // Tambahkan referensi ke UI Text di inspector
    public Button resolutionButton; // Tambahkan referensi ke Button Resolusi

    private int currentResolutionIndex = 0;
    private Resolution[] availableResolutions = {
        new Resolution { width = 1920, height = 1080 },
        new Resolution { width = 1440, height = 900 },
        new Resolution { width = 1280, height = 960 }
    };

    void Start()
    {
        // Set default resolution and add listener to the button
        SetResolution(0); 
        if (resolutionButton != null)
        {
            resolutionButton.onClick.AddListener(ChangeResolution); // Assign function to button click event
            Debug.Log("Button listener successfully added.");
        }
        else
        {
            Debug.LogError("Resolution button is not assigned in the inspector!");
        }
    }

    void ChangeResolution()
    {
        currentResolutionIndex = (currentResolutionIndex + 1) % availableResolutions.Length;
        Debug.Log("Changing resolution to index: " + currentResolutionIndex);
        SetResolution(currentResolutionIndex);
    }

    void SetResolution(int index)
    {
        if (index >= 0 && index < availableResolutions.Length)
        {
            Resolution res = availableResolutions[index];
            Screen.SetResolution(res.width, res.height, FullScreenMode.FullScreenWindow);
            resolutionText.text = "Resolution: " + res.width + "x" + res.height;  // Update UI Text
            Debug.Log("Resolution set to: " + res.width + "x" + res.height);
        }
        else
        {
            Debug.LogError("Resolution index is out of range: " + index);
        }
    }
}
