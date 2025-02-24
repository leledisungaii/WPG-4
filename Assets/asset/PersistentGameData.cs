using UnityEngine;
using System.Collections.Generic;

public class PersistentGameData : MonoBehaviour
{
    public static PersistentGameData Instance { get; private set; } // Singleton instance

    // Dictionary to store times for each level by its unique ID
    private Dictionary<int, float> levelTimes = new Dictionary<int, float>();

    private void Awake()
    {
        // Ensure that this GameObject persists between scene loads
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep this object alive
        }
        else
        {
            Destroy(gameObject); // Prevent duplicates
        }
    }

    // Save time taken for a specific level by its unique ID
    public bool SaveTimeForLevel(int levelID, float time)
    {
        try
        {
            if (levelTimes.ContainsKey(levelID))
            {
                levelTimes[levelID] = time; // Update time if level already exists
                Debug.Log($"Updated time for Level ID {levelID}: {time}");
            }
            else
            {
                levelTimes.Add(levelID, time); // Add new entry for this level
                Debug.Log($"Saved new time for Level ID {levelID}: {time}");
            }
            return true; // Indicate success
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Failed to save time for Level ID {levelID}. Error: {ex.Message}");
            return false; // Indicate failure
        }
    }

    // Retrieve time taken for a specific level by its unique ID
    public float GetTimeForLevel(int levelID)
    {
        if (levelTimes.ContainsKey(levelID))
        {
            Debug.Log($"Retrieved time for Level ID {levelID}: {levelTimes[levelID]}");
            return levelTimes[levelID];
        }
        Debug.LogWarning($"Level ID {levelID} not found. Returning 0.");
        return 0f; // Return 0 if level not found
    }
}
