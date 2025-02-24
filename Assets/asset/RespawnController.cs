using UnityEngine;

public class RespawnController : MonoBehaviour
{
    public Transform respawnPoint; // Assign the respawn point in the inspector

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the player or any other respawnable object
        if (other.CompareTag("Player") || other.CompareTag("Respawnable"))
        {
            // Teleport the object to the respawn point
            other.transform.position = respawnPoint.position;

            // Optionally reset velocity if the object has a Rigidbody
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
    }
}
