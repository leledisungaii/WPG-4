using UnityEngine;
using UnityEngine.UI; // Use this if you're using the normal Text component
// using TMPro; // Uncomment if using TextMeshPro

public class ShowUIText : MonoBehaviour
{
    public Text uiText; // For regular Text component
    // public TextMeshProUGUI uiText; // Uncomment if using TextMeshPro

    void Start()
    {
        // Ensure the text is visible at the start of the scene
        uiText.gameObject.SetActive(true);
    }

    void Update()
    {
        // Check if Shift is pressed
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            // Hide the UI text
            uiText.gameObject.SetActive(false);
        }
    }
}