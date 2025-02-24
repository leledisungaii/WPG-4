using UnityEngine;
using UnityEngine.UI; // Include the Unity UI namespace

public class ScoreManager : MonoBehaviour
{
    public int levelID; // Unique ID for the current level

    public GameObject oneStarObject;        // GameObject for 1 star rating
    public GameObject twoStarObject;        // GameObject for 2 star rating
    public GameObject threeStarObject;      // GameObject for 3 star rating
    public GameObject finalTimeDisplay;     // GameObject to display the final time

    public float thresholdThreeStars = 180f; // Time threshold for 3 stars (3:00)
    public float thresholdTwoStars = 120f;   // Time threshold for 2 stars (2:00)
    public float thresholdOneStar = 60f;     // Time threshold for 1 star (1:00)

    private void Start()
    {
        // Retrieve the time taken from PersistentGameData
        float timeTaken = PersistentGameData.Instance.GetTimeForLevel(levelID);

        // Debug log to check the retrieved time
        Debug.Log("Retrieved Time Taken: " + timeTaken);

        // Display the final time
        DisplayFinalTime(timeTaken);

        // Call the method to display the appropriate star based on the time taken
        ShowStarsBasedOnTime(timeTaken);
    }

    void DisplayFinalTime(float timeTaken)
    {
        // Check if finalTimeDisplay is a Text component
        Text timeText = finalTimeDisplay.GetComponent<Text>();

        if (timeText != null)
        {
            timeText.text = FormatTime(timeTaken); // Set the text to the formatted time
            Debug.Log("Final Time Displayed: " + timeText.text); // Debug log for displayed time
        }
        else
        {
            Debug.LogWarning("FinalTimeDisplay does not have a Text component!");
        }
    }

    void ShowStarsBasedOnTime(float timeTaken)
    {
        // Enable the star GameObject based on the time taken
        if (timeTaken >= thresholdThreeStars) // Award 3 stars if time is greater than or equal to 3 minutes
        {
            Debug.Log("Enabling 3-Star Object");
            EnableStar(threeStarObject);
        }
        else if (timeTaken >= thresholdTwoStars) // Award 2 stars if time is greater than or equal to 2 minutes
        {
            Debug.Log("Enabling 2-Star Object");
            EnableStar(twoStarObject);
        }
        else if (timeTaken >= thresholdOneStar) // Award 1 star if time is greater than or equal to 1 minute
        {
            Debug.Log("Enabling 1-Star Object");
            EnableStar(oneStarObject);
        }
        else
        {
            Debug.Log("No stars to enable based on time taken: " + timeTaken);
        }
    }

    void EnableStar(GameObject starObject)
    {
        if (starObject != null)
        {
            starObject.SetActive(true); // Activate the star object
            Debug.Log(starObject.name + " has been enabled.");
        }
        else
        {
            Debug.LogWarning("Star object is null!");
        }
    }

    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds); // Format as MM:SS
    }
}
