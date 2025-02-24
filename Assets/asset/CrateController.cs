using UnityEngine;

public class CrateController : MonoBehaviour
{
    public float gridSize = 1.0f; // Size of each grid cell
    public float pushForce = 5.0f; // Force applied to the crate

    private Rigidbody rb;
    private Vector3 targetPosition;
    private bool isMoving = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Lock rotation on all axes to prevent rotation
        rb.freezeRotation = true;

        // Initialize target position
        targetPosition = transform.position;
    }

    private void FixedUpdate()
    {
        // Smoothly move the crate to the target position
        if (isMoving)
        {
            Vector3 direction = targetPosition - transform.position;
            if (direction.magnitude > 0.1f) // Threshold to stop moving
            {
                rb.MovePosition(Vector3.Lerp(transform.position, targetPosition, Time.fixedDeltaTime * pushForce));
            }
            else
            {
                transform.position = targetPosition;
                isMoving = false;
                SnapToGrid(); // Ensure crate aligns with the grid after movement
            }
        }
    }

    public void PushCrate(Vector3 direction)
    {
        if (!isMoving)
        {
            // Calculate the target position
            targetPosition = transform.position + direction * gridSize;
            isMoving = true;
        }
    }

    private void SnapToGrid()
    {
        float x = Mathf.Round(transform.position.x / gridSize) * gridSize;
        float z = Mathf.Round(transform.position.z / gridSize) * gridSize;
        transform.position = new Vector3(x, transform.position.y, z);
    }
}
