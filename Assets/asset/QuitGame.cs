using UnityEngine;

public class QuitGame : MonoBehaviour
{
    // Call this function to quit the game
    public void Quit()
    {
        // If we are running in the Unity editor
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        // If we are running the game as a build
        Application.Quit();
#endif
    }
}
