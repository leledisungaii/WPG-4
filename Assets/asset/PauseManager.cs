using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;  // Reference to the pause menu UI
    [SerializeField] private PlayerController playerController;  // Reference to your PlayerController script

    private bool isPaused = false;

    void Update()
    {
        // Check if the player presses the pause button (Tab key or Escape key)
        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        // Toggle the pause state
        isPaused = !isPaused;

        // Show or hide the pause menu
        pauseMenuUI.SetActive(isPaused);

        // Pause or resume game time
        Time.timeScale = isPaused ? 0f : 1f;

        // Pause or resume player movement
        if (isPaused)
        {
            playerController.PauseMovement();
        }
        else
        {
            playerController.ResumeMovement();
        }
    }
}
