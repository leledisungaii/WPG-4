using UnityEngine;
using UnityEngine.UI;

public class ButtonSetup : MonoBehaviour
{
    public Button skipButton; // Reference to the button
    public Dialog dialog; // Reference to the Dialog script

    private void Start()
    {
        // Ensure the button is assigned and set it to active
        if (skipButton != null)
        {
            skipButton.gameObject.SetActive(true); // Enable the button by default
            skipButton.onClick.AddListener(OnSkipButtonPressed);
        }
        else
        {
            Debug.LogError("Skip Button is not assigned in the Inspector!");
        }

        // Disable the button initially
        skipButton.interactable = false;
    }

    private void Update()
    {
        // Enable or disable the button based on the dialog state
        if (dialog.IsDialogActive)
        {
            skipButton.interactable = true; // Enable the button when dialog is active
        }
        else
        {
            skipButton.interactable = false; // Disable the button when dialog is not active
            skipButton.gameObject.SetActive(false); // Hide the button when dialog is finished
        }
    }

    private void OnSkipButtonPressed()
    {
        // Trigger the dialog to skip to the next part
        dialog.SkipDialog();
    }
}
