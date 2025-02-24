using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    // Name of the scene to switch to
    public string targetScene;

    // This method will be called when the player collides with another collider
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the object has a specific tag (optional)
        if (collision.gameObject.CompareTag("SwitchSceneObject"))
        {
            // Load the target scene
            SceneManager.LoadScene(targetScene);
        }
    }
}
