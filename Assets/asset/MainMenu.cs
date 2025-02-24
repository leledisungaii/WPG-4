using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    private GameTimer gameTimer;  // Reference to GameTimer

    void Start()
    {
        // Find the GameTimer object in the scene
        gameTimer = FindObjectOfType<GameTimer>();
    }

    public void ToggleMenu()
    {
        bool isMenuOpen = Time.timeScale == 0;

        // Toggle between pausing and unpausing
        if (isMenuOpen)
        {
            // Resume the game
            Time.timeScale = 1;
            if (gameTimer != null)
            {
                gameTimer.PauseTimer(false);  // Unpause the timer
            }
        }
        else
        {
            // Pause the game
            Time.timeScale = 0;
            if (gameTimer != null)
            {
                gameTimer.PauseTimer(true);   // Pause the timer
            }
        }
    }

    public void LoadScene()
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
