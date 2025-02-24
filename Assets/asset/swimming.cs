using UnityEngine;
using UnityEngine.UI;  // For legacy UI Text
using UnityEngine.SceneManagement;  // For loading scenes

public class swimming : MonoBehaviour
{
    [Header("Swimming Settings")]
    public float swimUpForce = 5.0f;          // Force applied when moving up
    public float swimDownSpeed = 2.0f;        // Speed at which the player moves down automatically
    public float timeBeforeMovingDown = 1.0f; // Time after moving up before moving down automatically
    public float swimHorizontalSpeed = 3.0f;  // Speed for horizontal swimming movement

    [Header("Auto-Jump Settings")]
    public float autoJumpForce = 7.0f;        // Force applied when auto-jumping out of water

    [Header("Water Surface Settings")]
    public float waterSurfaceY = 5.0f;        // Y position of the water surface

    [Header("Scene Settings")]
    public float maxTimeInWater = 10.0f;      // Maximum time player can stay in water before scene changes
    public string nextSceneName;              // Name of the next scene to load

    public GameObject waterObject;            // Reference to the water object

    [Header("UI Settings")]
    public Text waterTimerText;               // Reference to legacy Text UI for displaying the timer

    private Rigidbody rb;
    private bool isSwimming = false;
    private float moveDownTimer = 0.0f;
    private float swimmingTime = 0.0f;        // Timer to track how long the player is in water
    private PlayerController playerController;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerController = GetComponent<PlayerController>();
        UpdateTimerDisplay();  // Update timer at the start (if necessary)
    }

    private void Update()
    {
        if (isSwimming)
        {
            // Increase the swimming time
            swimmingTime += Time.deltaTime;

            // Update the UI text to show the time remaining
            UpdateTimerDisplay();

            // Check if the player has been in the water too long
            if (swimmingTime >= maxTimeInWater)
            {
                // Load the next scene
                SceneManager.LoadScene(nextSceneName);
            }

            // Disable the PlayerController movement
            playerController.enabled = false;

            // Handle vertical swimming movement
            if (Input.GetKey(KeyCode.Space))
            {
                // Move up
                rb.velocity = new Vector3(rb.velocity.x, swimUpForce, rb.velocity.z);
                moveDownTimer = 0.0f; // Reset the timer
            }
            else
            {
                // After some time, move down automatically
                moveDownTimer += Time.deltaTime;
                if (moveDownTimer >= timeBeforeMovingDown)
                {
                    // Move down
                    rb.velocity = new Vector3(rb.velocity.x, -swimDownSpeed, rb.velocity.z);
                }
            }

            // Handle horizontal swimming movement
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            // Apply horizontal swimming movement based on input
            Vector3 swimMovement = new Vector3(horizontal * swimHorizontalSpeed, rb.velocity.y, vertical * swimHorizontalSpeed);
            rb.velocity = swimMovement;

            // Check if player has reached the surface (top of the water cube)
            if (transform.position.y >= waterSurfaceY)
            {
                // Auto jump out of water
                rb.AddForce(Vector3.up * autoJumpForce, ForceMode.Impulse);
                isSwimming = false; // Exit swimming state

                // Re-enable the PlayerController
                playerController.enabled = true;
                swimmingTime = 0.0f; // Reset swimming timer when out of water
                UpdateTimerDisplay();  // Update the timer when out of the water
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == waterObject)
        {
            isSwimming = true;
            moveDownTimer = 0.0f;
            swimmingTime = 0.0f; // Reset the swimming time when entering water
            // Disable the PlayerController movement
            playerController.enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == waterObject)
        {
            isSwimming = false;
            // Re-enable the PlayerController
            playerController.enabled = true;
            swimmingTime = 0.0f; // Reset swimming timer when out of water
            UpdateTimerDisplay();  // Clear the timer when exiting water
        }
    }

    // Function to update the UI timer text
    private void UpdateTimerDisplay()
    {
        if (isSwimming)
        {
            float timeRemaining = maxTimeInWater - swimmingTime;
            waterTimerText.text = "waktu di air: " + Mathf.Clamp(timeRemaining, 0, maxTimeInWater).ToString("F2") + "s";
        }
        else
        {
            waterTimerText.text = "";  // Clear the timer when not in the water
        }
    }
}
