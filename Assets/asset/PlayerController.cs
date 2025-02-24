using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;  // Speed of the player movement
    public float pushForce = 5.0f;  // Force applied to push crates
    public float jumpForce = 7.0f;  // Force applied for jumping

    private Rigidbody rb;
    private bool isGrounded;
    private bool isMovementPaused = false;  // Flag to pause movement

    // Public read-only property to access isGrounded
    public bool IsGrounded
    {
        get { return isGrounded; }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Lock rotation on all axes to prevent rotation
        rb.freezeRotation = true;
    }

    private void Update()
    {
        // If movement is paused, ignore player input
        if (isMovementPaused) return;

        // Get input for movement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate movement direction in X and Z plane
        Vector3 movement = new Vector3(horizontal, 0, vertical).normalized;

        // Move the player, only changing X and Z velocity, leaving Y for jump
        rb.velocity = new Vector3(movement.x * moveSpeed, rb.velocity.y, movement.z * moveSpeed);

        // Jumping logic (using spacebar)
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // Push crate if applicable
        PushCrate(movement);
    }

    private void PushCrate(Vector3 direction)
    {
        // Perform a raycast to detect if the player is pushing a crate
        RaycastHit hit;
        float rayDistance = 1.0f; // Adjust this based on the crate size and distance from the player
        if (Physics.Raycast(transform.position, direction, out hit, rayDistance))
        {
            if (hit.collider.CompareTag("Crate"))
            {
                CrateController crateController = hit.collider.GetComponent<CrateController>();
                if (crateController != null)
                {
                    crateController.PushCrate(direction);
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;  // Player is on the ground
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false; // Player left the ground
        }
    }

    // Method to pause player movement
    public void PauseMovement()
    {
        isMovementPaused = true;
        rb.velocity = Vector3.zero; // Stop the player’s movement immediately
    }

    // Method to resume player movement
    public void ResumeMovement()
    {
        isMovementPaused = false;
    }
}
