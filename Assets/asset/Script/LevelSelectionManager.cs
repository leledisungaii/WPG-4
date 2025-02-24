// using UnityEngine;
// using UnityEngine.UI;

// public class LevelSelectionManager : MonoBehaviour
// {
//     public Button level1Button;
//     public Button level2Button;
//     public Button level3Button;

//     private void Start()
//     {
//         // Set button interactivity based on the completion status stored in PlayerPrefs
//         SetupLevelButtons();
//     }

//     private void SetupLevelButtons()
//     {
//         // Check if levels are completed using PlayerPrefs
//         bool level1Completed = IsLevelCompleted(1);
//         bool level2Completed = IsLevelCompleted(2);
//         bool level3Completed = IsLevelCompleted(3);

//         // Level 1 is always accessible, others depend on previous level completion
//         level1Button.interactable = true;
//         level2Button.interactable = level1Completed;
//         level3Button.interactable = level2Completed;

//         // Add event listeners to the buttons
//         level1Button.onClick.AddListener(() => LoadLevel("Stage1-Level1"));
//         level2Button.onClick.AddListener(() => LoadLevel("Stage1-Level2"));
//         level3Button.onClick.AddListener(() => LoadLevel("Stage1-Level3"));
//     }

//     private bool IsLevelCompleted(int levelIndex)
//     {
//         // Check PlayerPrefs to determine if the level is completed
//         return PlayerPrefs.GetInt($"Level_{levelIndex}_Completed", 0) == 1;
//     }

//     private void LoadLevel(string levelName)
//     {
//         // Load the scene corresponding to the level
//         UnityEngine.SceneManagement.SceneManager.LoadScene(levelName);
//     }

//     // Method to mark a level as completed when the player finishes it
//     public void CompleteLevel(int levelIndex)
//     {
//         // Mark the level as completed in PlayerPrefs
//         PlayerPrefs.SetInt($"Level_{levelIndex}_Completed", 1);
//         PlayerPrefs.Save();

//         // Update the level buttons' interactivity (optional)
//         SetupLevelButtons();
//     }
// }
