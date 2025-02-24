using UnityEngine;

public class GroundController : MonoBehaviour
{
    // Optional: Use for collision detection
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Collided with {collision.gameObject.name}");
    }

    // Optional: Use for trigger detection
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Trigger entered by {other.gameObject.name}");
    }
}
