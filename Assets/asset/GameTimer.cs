using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public float timeLimit = 300f; // Total time in seconds (e.g., 300 seconds = 5 minutes)
    public string nextSceneName;    // Name of the scene to load after time runs out
    public Text timerText;          // Optional: UI Text to display the time
    public GameObject[] pauseMenus; // Array of GameObjects that can pause the timer
    public int levelID;             // Unique ID for the current level
    public bool saveBasedOnTimeLeft; // Option to save based on time left (true) or time spent (false)

    private float currentTime;      // Tracks the current countdown time
    private bool isPaused = false;  // Internal pause state

    void Start()
    {
        currentTime = timeLimit;     // Initialize the timer to the time limit
    }

    void Update()
    {
        // Check if any of the pauseMenu GameObjects are active, and pause the timer accordingly
        if (AreAnyPauseMenusActive())
        {
            PauseTimer(true);          // If any pause menu is enabled, pause the timer
        }
        else
        {
            PauseTimer(false);         // Otherwise, resume the timer
        }

        // Only countdown if the game is not paused
        if (!isPaused && currentTime > 0)
        {
            currentTime -= Time.deltaTime; // Decrease current time based on frame time

            // Update the timer display
            if (timerText != null)
            {
                timerText.text = FormatTime(currentTime);
            }

            // Check if the timer has run out
            if (currentTime <= 0)
            {
                SaveTimeAndLoadNextScene(); // Save time and transition if timer runs out
            }
        }
    }

    // Method to check if any of the specified pauseMenus are active
    bool AreAnyPauseMenusActive()
    {
        foreach (GameObject pauseMenu in pauseMenus)
        {
            if (pauseMenu != null && pauseMenu.activeSelf)
            {
                return true; // If any of the pauseMenus are active, return true
            }
        }
        return false; // If none are active, return false
    }

    // Public method to pause or unpause the timer
    public void PauseTimer(bool pauseState)
    {
        isPaused = pauseState; // Set pause state (true to pause, false to resume)
    }

    // Format the time to show minutes and seconds
    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60); // Get minutes
        int seconds = Mathf.FloorToInt(time % 60); // Get remaining seconds
        return string.Format("{0:00}:{1:00}", minutes, seconds); // Format as MM:SS
    }

    public void SaveTimeAndLoadNextScene()
    {
        float timeRecorded;

        if (saveBasedOnTimeLeft)
        {
            timeRecorded = currentTime; // Time left
            Debug.Log("Saving based on time left: " + timeRecorded);
        }
        else
        {
            timeRecorded = timeLimit - currentTime; // Time spent
            Debug.Log("Saving based on time spent: " + timeRecorded);
        }

        // Debug log for levelID
        Debug.Log($"Current Level ID before saving: {levelID}");

        // Save time and check if it's successful
        bool saveSuccess = PersistentGameData.Instance.SaveTimeForLevel(levelID, timeRecorded);
        if (saveSuccess)
        {
            Debug.Log("Time Recorded for Level ID " + levelID + ": " + timeRecorded);
            SceneManager.LoadScene(nextSceneName); // Load the next scene only after saving
        }
        else
        {
            Debug.LogError("Failed to save time for Level ID " + levelID);
        }
    }
}
