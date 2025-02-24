using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager Instance; // Singleton instance

    public List<GameObject> collectedItems = new List<GameObject>(); // List to hold collected items

    private void Awake()
    {
        // Ensure only one instance of AchievementManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool IsItemCollected(GameObject item)
    {
        return collectedItems.Contains(item); // Check if the item is in the collected list
    }

    public void CollectItem(GameObject item)
    {
        if (!IsItemCollected(item))
        {
            collectedItems.Add(item); // Add the item to the collected list
            Debug.Log("Collected item: " + item.name);
        }
    }
}
