using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerSceneChange : MonoBehaviour
{
    // Name of the scene to switch to
    public string targetScene;

    // This method will be called when another collider enters the trigger
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the object that hit has a Rigidbody (optional)
        if (collision.rigidbody != null)
        {
            // Load the target scene
            SceneManager.LoadScene(targetScene);
        }
    }

    // This method will be called when another collider stays in contact with this collider
    private void OnCollisionStay(Collision collision)
    {
        // Load the scene when the collision persists, if needed
        if (collision.rigidbody != null)
        {
            SceneManager.LoadScene(targetScene);
        }
    }

    // This method will be called when another collider leaves the trigger
    private void OnCollisionExit(Collision collision)
    {
        // Optional: You could add a condition to load the scene on exit
    }
}
