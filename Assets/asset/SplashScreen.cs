using UnityEngine;
using UnityEngine.UI; // Include this for UI elements

public class SplashScreen : MonoBehaviour
{
    public GameObject splashScreen; // Assign your splash screen GameObject in the inspector
    public KeyCode skipKey = KeyCode.Space; // Hotkey to skip the splash screen
    public Button skipButton; // Assign your UI Button in the inspector

    [Header("Splash Screen Settings")]
    public bool showOnce = true; // Checkbox to determine if the splash screen should only show once

    private bool hasShownSplashScreen = false; // Track if the splash screen has been shown

    private void Start()
    {
        // Ensure the splash screen is disabled at the start
        splashScreen.SetActive(false);

        // Disable the button by default
        if (skipButton != null)
        {
            skipButton.gameObject.SetActive(false); // Hide the button initially
            skipButton.interactable = false; // Ensure the button is not interactable initially
            skipButton.onClick.AddListener(SkipSplashScreen); // Add the listener to the button
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player collides with the trigger and if the splash screen has not been shown
        if (other.CompareTag("Player") && (!showOnce || !hasShownSplashScreen)) // Ensure your player GameObject has the "Player" tag
        {
            ActivateSplashScreen();
        }
    }

    private void Update()
    {
        // Check for the skip key press
        if (splashScreen.activeSelf && Input.GetKeyDown(skipKey))
        {
            SkipSplashScreen();
        }
    }

    private void ActivateSplashScreen()
    {
        splashScreen.SetActive(true);

        if (skipButton != null)
        {
            skipButton.gameObject.SetActive(true); // Show the button when the splash screen is active
            skipButton.interactable = true; // Ensure the button is interactable
        }

        // Set the splash screen shown flag to true if it should only show once
        if (showOnce)
        {
            hasShownSplashScreen = true;
        }
    }

    private void SkipSplashScreen()
    {
        splashScreen.SetActive(false);

        if (skipButton != null)
        {
            skipButton.gameObject.SetActive(false); // Hide the button again when skipping
            skipButton.interactable = false; // Disable the button when skipping
        }
        // Optionally, resume the game or load the next scene here
    }
}
