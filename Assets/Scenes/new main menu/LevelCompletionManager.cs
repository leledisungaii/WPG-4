using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompletionManager : MonoBehaviour
{
    public static LevelCompletionManager Instance;

    private bool[] levelCompletionStatus;
    public int[] levelIDs; // Level IDs for each level
    public GameObject[] levelButtons; // Buttons for each level
    public int[] alwaysUnlockedLevels; // Always unlocked levels

    private void Awake()
    {
        // Singleton setup
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep this object across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate if any
        }

        // Initialize level completion status based on the number of levels
        int totalLevels = levelIDs.Length;
        levelCompletionStatus = new bool[totalLevels];
        LoadLevelCompletionStatus();
    }

    private void Start()
    {
        // Check the status of all levels when the game starts
        UpdateLevelStatuses();
    }

    // Method to check if the player has completed the level based on the level ID
    public bool IsLevelCompleted(int levelID)
    {
        int index = GetLevelIndex(levelID);
        return index >= 0 && levelCompletionStatus[index];
    }

    // Complete level and save status, update buttons for next levels
    public void CompleteLevel(int levelID)
    {
        int index = GetLevelIndex(levelID);
        if (index >= 0 && !levelCompletionStatus[index])
        {
            levelCompletionStatus[index] = true;
            SaveLevelCompletionStatus();
            UpdateLevelStatuses(); // Update button statuses
        }
    }

    private void UpdateLevelStatuses()
    {
        // Only update button status if we're in the level menu scene
        if (SceneManager.GetActiveScene().name == "LevelMenu")
        {
            LevelButtonManager.Instance.UpdateLevelButtons(); // Trigger the button updates from LevelButtonManager
        }
    }

    public bool IsAlwaysUnlocked(int levelID)
    {
        foreach (int unlockedLevel in alwaysUnlockedLevels)
        {
            if (unlockedLevel == levelID)
            {
                return true; // Level is always unlocked
            }
        }
        return false;
    }


    // Save level completion status to PlayerPrefs
    private void SaveLevelCompletionStatus()
    {
        for (int i = 0; i < levelCompletionStatus.Length; i++)
        {
            PlayerPrefs.SetInt($"Level_{levelIDs[i]}_Completed", levelCompletionStatus[i] ? 1 : 0);
        }
        PlayerPrefs.Save();
    }

    // Load level completion status from PlayerPrefs
    private void LoadLevelCompletionStatus()
    {
        for (int i = 0; i < levelCompletionStatus.Length; i++)
        {
            levelCompletionStatus[i] = PlayerPrefs.GetInt($"Level_{levelIDs[i]}_Completed", 0) == 1;
        }
    }

    private int GetLevelIndex(int levelID)
    {
        for (int i = 0; i < levelIDs.Length; i++)
        {
            if (levelIDs[i] == levelID)
            {
                return i;
            }
        }
        return -1;
    }

    // Load the specific level and mark as complete if appropriate
    public void LoadLevel(string levelName, int levelID)
    {
        // Check if the previous level is completed before loading the next one
        if (levelID == 1 || IsLevelCompleted(levelID - 1)) // Level 1 can always be loaded
        {
            SceneManager.LoadScene(levelName);
            CompleteLevel(levelID); // Mark level as completed
        }
        else
        {
            Debug.Log($"Level {levelID} is locked. Complete the previous level first.");
        }
    }

    // This method can be called when the player collects the required items and transitions to the main menu
    public void ReturnToMainMenu()
    {
        // Make sure level status is saved and transition back to the main menu
        SaveLevelCompletionStatus();
        SceneManager.LoadScene("MainMenu"); // Change "MainMenu" to your actual main menu scene name
    }
}
