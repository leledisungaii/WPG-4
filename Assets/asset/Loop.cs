using UnityEngine;
using System.Collections.Generic;

public class Loop : MonoBehaviour
{
    // Singleton instance
    public static Loop Instance;

    // Dictionary to track multiple inLevel triggers
    private Dictionary<string, bool> inLevelTriggers = new Dictionary<string, bool>();

    private void Awake()
    {
        // Implement Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject); // Ensure only one instance exists
        }
    }

    // Trigger an InLevel with a specific ID
    public void TriggerInLevel(string id)
    {
        if (!inLevelTriggers.ContainsKey(id))
        {
            inLevelTriggers.Add(id, true); // Mark the InLevel with this ID as triggered
        }
    }

    // Reset a specific InLevel trigger
    public void ResetTrigger(string id)
    {
        if (inLevelTriggers.ContainsKey(id))
        {
            inLevelTriggers[id] = false; // Reset trigger for the given ID
        }
    }


    // Check if a specific InLevel has been triggered
    public bool IsInLevelTriggered(string id)
    {
        return inLevelTriggers.ContainsKey(id) && inLevelTriggers[id];
    }

    // Move OutLevel objects based on the InLevel ID
    public void MoveOutLevelObject(OutLevel outLevelScript, string id)
    {
        if (IsInLevelTriggered(id))
        {
            outLevelScript.MoveObject(); // Move the object if the corresponding InLevel was triggered
        }
    }
}
