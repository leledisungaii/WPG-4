using UnityEngine;
using UnityEngine.UI;  // For using UI components
using System.Collections;

public class Dialog : MonoBehaviour
{
    [Header("Dialog Components")]
    public GameObject messageCloud;                 // The GameObject for the message cloud
    public GameObject[] characterObjects;            // Array of GameObjects for different characters
    public Text dialogText;                          // Legacy UI Text component for dialog text
    public string[] dialogTexts;                     // Array of dialog texts to display
    public float textSpeed = 0.05f;                  // Speed for text reveal

    private int currentDialogIndex = 0;

    // Public property to check if the dialog is active
    public bool IsDialogActive { get; private set; } // Public getter, private setter

    private Coroutine currentTextCoroutine;          // Coroutine to handle text reveal

    void Start()
    {
        // Initialize the first dialog if available
        if (dialogTexts.Length > 0)
        {
            ShowNextDialog();
        }
    }

    void Update()
    {
        // Skip to next dialog when Shift is pressed
        if (IsDialogActive && (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)))
        {
            SkipDialog();
        }
    }

    // Shows the next dialog text
    public void ShowNextDialog()
    {
        // Hide the previous dialog if it exists
        if (currentDialogIndex > 0)
        {
            messageCloud.SetActive(false); // Disable the previous dialog box
            dialogText.gameObject.SetActive(false); // Disable the previous text
            // Disable the previous character GameObject
            if (currentDialogIndex - 1 < characterObjects.Length)
            {
                characterObjects[currentDialogIndex - 1].SetActive(false);
            }
        }

        // Check if we still have more dialog texts to show
        if (currentDialogIndex < dialogTexts.Length)
        {
            messageCloud.SetActive(true); // Enable the message cloud
            dialogText.gameObject.SetActive(true); // Enable the dialog text

            // Enable the current character GameObject if available
            if (currentDialogIndex < characterObjects.Length)
            {
                characterObjects[currentDialogIndex].SetActive(true);
            }

            // Start showing the text with the typewriter effect
            if (currentTextCoroutine != null)
            {
                StopCoroutine(currentTextCoroutine); // Stop the current text reveal
            }
            currentTextCoroutine = StartCoroutine(TypeText(dialogTexts[currentDialogIndex]));

            IsDialogActive = true; // Set the dialog active state
            currentDialogIndex++; // Move to the next dialog index after showing the current one
        }
        else
        {
            // End of dialog sequence, deactivate components
            messageCloud.SetActive(false);
            dialogText.gameObject.SetActive(false);
            IsDialogActive = false; // Set dialog state to inactive
        }
    }

    // Coroutine to display text with typewriter effect
    IEnumerator TypeText(string dialog)
    {
        dialogText.text = "";
        foreach (char letter in dialog.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    // Skips the current dialog to the next one
    public void SkipDialog()
    {
        // Complete the current text immediately and move to the next dialog
        if (currentTextCoroutine != null)
        {
            StopCoroutine(currentTextCoroutine);
            dialogText.text = dialogTexts[currentDialogIndex - 1]; // Show the full text immediately
        }
        ShowNextDialog();
    }
}
