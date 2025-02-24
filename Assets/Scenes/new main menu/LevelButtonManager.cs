using UnityEngine;

public class LevelButtonManager : MonoBehaviour
{
    public static LevelButtonManager Instance;
    public GameObject[] levelButtons; // The buttons for each level
    public Vector3 offScreenOffset;   // The offset to move the button off-screen (editable in Inspector)
    public float moveSpeed = 5f;      // Speed at which the button moves on-screen

    private Vector3[] originalPositions; // Store original positions for each button

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate if any
        }

        // Store original positions of all level buttons
        originalPositions = new Vector3[levelButtons.Length];
        for (int i = 0; i < levelButtons.Length; i++)
        {
            originalPositions[i] = levelButtons[i].transform.localPosition;
        }
    }

    // Update level buttons based on the completion status of the previous level
    public void UpdateLevelButtons()
    {
        if (LevelCompletionManager.Instance == null) return;

        for (int i = 0; i < levelButtons.Length; i++)
        {
            int levelID = LevelCompletionManager.Instance.levelIDs[i];
            bool isCompleted = LevelCompletionManager.Instance.IsLevelCompleted(levelID);

            // Always unlock certain levels
            if (LevelCompletionManager.Instance.IsAlwaysUnlocked(levelID))
            {
                // Reset to original position for always unlocked levels
                MoveButtonOnScreen(levelButtons[i], originalPositions[i]);
            }
            else
            {
                // Check if the previous level is completed or if it's the first level
                if (i == 0 || LevelCompletionManager.Instance.IsLevelCompleted(LevelCompletionManager.Instance.levelIDs[i - 1]))
                {
                    // Move button to original position (on-screen)
                    MoveButtonOnScreen(levelButtons[i], originalPositions[i]);
                }
                else
                {
                    // Move button off-screen if the previous level is incomplete
                    MoveButtonOffScreen(levelButtons[i], originalPositions[i] + offScreenOffset);
                }
            }
        }
    }

    // Move button to original position
    private void MoveButtonOnScreen(GameObject button, Vector3 targetPosition)
    {
        button.transform.localPosition = Vector3.Lerp(button.transform.localPosition, targetPosition, moveSpeed * Time.deltaTime);
    }

    // Move button off screen
    private void MoveButtonOffScreen(GameObject button, Vector3 offScreenPosition)
    {
        button.transform.localPosition = Vector3.Lerp(button.transform.localPosition, offScreenPosition, moveSpeed * Time.deltaTime);
    }
}
