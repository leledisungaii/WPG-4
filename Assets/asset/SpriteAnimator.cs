using UnityEngine;

public class SpriteAnimator : MonoBehaviour  // Rename to avoid conflict with Unity's Sprite class
{
    public GameObject[] idleObjects;   // Array of GameObjects for idle animation
    public GameObject[] movingObjects; // Array of GameObjects for moving animation
    public GameObject[] jumpObjects;   // Array of GameObjects for jump animation
    public float animationSpeed = 0.1f; // Time between frames for animation
    private PlayerController playerController; // Reference to PlayerController script

    private float animationTimer;  // Timer to control frame switching
    private int currentFrame;      // Current frame of the animation
    private bool isMoving;         // Track whether the player is moving or not

    private string currentState; // Track current state of the player

    private void Start()
    {
        // Find the PlayerController script attached to the player
        playerController = GetComponent<PlayerController>();

        // Initially disable all idle, moving, and jump objects
        DisableAllObjects();
        currentState = "Idle"; // Initialize current state to idle
    }

    private void Update()
    {
        // Check if the player is grounded and moving
        if (playerController.IsGrounded && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
        {
            // Animate moving objects
            AnimateObjects(movingObjects);
            currentState = "Moving";
            isMoving = true;
        }
        // Check if the player is grounded and not moving
        else if (playerController.IsGrounded && Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            // Animate idle objects
            AnimateObjects(idleObjects);
            currentState = "Idle";
            isMoving = false;
        }
        // Check if the player is jumping
        else if (!playerController.IsGrounded)
        {
            // Animate jump objects
            AnimateObjects(jumpObjects);
            currentState = "Jumping";
        }
        else
        {
            // If neither condition is true, disable all objects
            DisableAllObjects();
        }
    }

    // Method to handle object animation
    private void AnimateObjects(GameObject[] objects)
    {
        if (objects.Length == 0) return; // Return if no objects assigned

        // Increment the timer
        animationTimer += Time.deltaTime;

        // If enough time has passed, switch to the next object
        if (animationTimer >= animationSpeed)
        {
            // Reset the timer
            animationTimer = 0f;

            // Disable all objects first
            DisableAllObjects();

            // Update the current frame index
            currentFrame++;

            // Loop back to the first frame if we've reached the end of the object array
            if (currentFrame >= objects.Length)
            {
                currentFrame = 0;
            }

            // Enable the current object
            objects[currentFrame].SetActive(true);
        }
    }

    // Method to disable all objects
    private void DisableAllObjects()
    {
        // Disable all idle objects
        foreach (GameObject obj in idleObjects)
        {
            obj.SetActive(false);
        }

        // Disable all moving objects
        foreach (GameObject obj in movingObjects)
        {
            obj.SetActive(false);
        }

        // Disable all jump objects
        foreach (GameObject obj in jumpObjects)
        {
            obj.SetActive(false);
        }
    }

    // Method to get the current state of the player
    public string GetCurrentState()
    {
        return currentState;
    }
}
