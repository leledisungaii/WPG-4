using UnityEngine;

public class ToggleUI : MonoBehaviour
{
    [SerializeField] private GameObject uiCanvas; // Reference to the canvas/UI to toggle
    private bool isUIVisible = false;

    void Update()
    {
        // Check if the Tab key is pressed
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // Toggle the UI visibility
            isUIVisible = !isUIVisible;
            uiCanvas.SetActive(isUIVisible); // Show or hide the UI canvas
        }
    }
}
