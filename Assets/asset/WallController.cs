using UnityEngine;

public class WallController : MonoBehaviour
{
    // Optional: Define behavior when the player hits the wall
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player hit the wall!");
            // Implement any additional logic for when the player hits the wall
        }
    }
}

