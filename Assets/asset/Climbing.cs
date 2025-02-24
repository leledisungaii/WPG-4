using UnityEngine;

public class Climbing : MonoBehaviour
{
    [Header("Climbing Settings")]
    public float climbSpeed = 3.0f;       // Speed of climbing
    public float jumpForce = 5.0f;         // Force applied for the jump after climbing
    public GameObject climbableObject;     // Reference to the climbable object

    private Rigidbody rb;
    private bool isClimbing = false;
    private PlayerController playerController; // Reference to the normal movement script

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (isClimbing)
        {
            // Disable normal player movement
            playerController.enabled = false;

            // Handle climbing movement
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 climbDirection = new Vector3(0, verticalInput * climbSpeed, 0);
            rb.velocity = new Vector3(rb.velocity.x, climbDirection.y, rb.velocity.z);

            // Check for jump input when at the top of the climbable object
            if (transform.position.y >= climbableObject.transform.position.y + climbableObject.transform.localScale.y / 2)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                    isClimbing = false; // Exit climbing state
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == climbableObject)
        {
            isClimbing = true; // Start climbing when entering the climbable object
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == climbableObject)
        {
            isClimbing = false; // Stop climbing when exiting the climbable object
            playerController.enabled = true; // Re-enable normal movement
        }
    }
}
