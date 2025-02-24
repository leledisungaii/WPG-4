using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MouseClickMenu : MonoBehaviour
{
    public Button uiButton; // Reference to the UI Button
    public GameObject menuPanel; // Reference to the menu panel to toggle
    private GameTimer gameTimer;  // Reference to GameTimer

    void Start()
    {
        // Ensure the button is assigned and add a listener to it
        if (uiButton != null)
        {
            uiButton.onClick.AddListener(ToggleMenu);
            Debug.Log("UI Button assigned and listener added.");
        }
        else
        {
            Debug.LogError("UI Button is not assigned in the Inspector!");
        }

        // Find the GameTimer object in the scene
        gameTimer = FindObjectOfType<GameTimer>();
    }

    private void ToggleMenu()
    {
        // Toggle the visibility of the menu panel
        if (menuPanel != null)
        {
            bool isMenuOpen = menuPanel.activeSelf;

            // Toggle menu visibility
            menuPanel.SetActive(!isMenuOpen);
            Debug.Log("Menu visibility toggled.");

            // Pause or resume the game
            if (menuPanel.activeSelf)
            {
                Time.timeScale = 0; // Pause the game
                if (gameTimer != null)
                {
                    gameTimer.PauseTimer(true); // Pause the timer
                }
            }
            else
            {
                Time.timeScale = 1; // Resume the game
                if (gameTimer != null)
                {
                    gameTimer.PauseTimer(false); // Unpause the timer
                }
            }
        }
        else
        {
            Debug.LogError("Menu Panel is not assigned in the Inspector!");
        }
    }

    public void LoadScene(string sceneToLoad)
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            // Resume game time before loading a new scene
            Time.timeScale = 1;
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogWarning("Scene name is empty! Please set it in the Inspector.");
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
