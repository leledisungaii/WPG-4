using UnityEngine;

public class stopper : MonoBehaviour
{
    private PlayerController playerController; // Reference to the PlayerController script
    private Dialog dialog;                      // Reference to the Dialog script

    void Start()
    {
        playerController = GetComponent<PlayerController>(); // Get the PlayerController component
        dialog = FindObjectOfType<Dialog>(); // Find the Dialog component in the scene
    }

    void Update()
    {
        // Check if the dialog is active and stop the player from moving
        if (dialog != null && dialog.IsDialogActive)
        {
            // Disable player movement logic
            playerController.enabled = false; // Disable the PlayerController
        }
        else
        {
            // Re-enable player movement logic
            playerController.enabled = true; // Enable the PlayerController
        }
    }
}
