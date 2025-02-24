using UnityEngine;

public class bird : MonoBehaviour
{
    public GameObject birdSprite1;             // The first sprite (as a GameObject)
    public GameObject birdSprite2;             // The second sprite (as a GameObject)
    public float spriteChangeInterval = 0.5f;  // Time between sprite changes
    public float flightSpeed = 2f;             // Speed of the bird flying
    public bool moveHorizontally = true;       // Toggle for horizontal or vertical movement
    public Vector2 loopEndPosition;            // End position for the loop (X or Y)
    public float loopInterval = 1f;            // Time interval before resetting to start position

    private Vector2 initialPosition;           // Remember the bird's starting position
    private bool useSprite1 = true;            // To toggle between sprites
    private float timeSinceLastChange = 0f;
    private bool waitingToLoop = false;        // To manage the loop interval wait time
    private float timeSinceLoopEnd = 0f;       // Track time after reaching the end of the loop

    void Start()
    {
        // Store the bird's initial position at the start of the game
        initialPosition = transform.position;

        // Start with the first sprite
        birdSprite1.SetActive(true);
        birdSprite2.SetActive(false);
    }

    void Update()
    {
        // Change the sprite after the specified interval
        timeSinceLastChange += Time.deltaTime;
        if (timeSinceLastChange >= spriteChangeInterval)
        {
            // Toggle between the two sprites
            if (useSprite1)
            {
                birdSprite1.SetActive(false);
                birdSprite2.SetActive(true);
            }
            else
            {
                birdSprite1.SetActive(true);
                birdSprite2.SetActive(false);
            }

            useSprite1 = !useSprite1;  // Flip the toggle
            timeSinceLastChange = 0f;
        }

        if (!waitingToLoop)
        {
            // Move the bird horizontally or vertically based on the toggle
            if (moveHorizontally)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(loopEndPosition.x, transform.position.y), flightSpeed * Time.deltaTime);
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, loopEndPosition.y), flightSpeed * Time.deltaTime);
            }

            // If the bird reaches the loop end position, start waiting for the interval
            if ((moveHorizontally && transform.position.x <= loopEndPosition.x) || (!moveHorizontally && transform.position.y <= loopEndPosition.y))
            {
                waitingToLoop = true;
                timeSinceLoopEnd = 0f;
            }
        }
        else
        {
            // Track how long we wait before resetting the position
            timeSinceLoopEnd += Time.deltaTime;
            if (timeSinceLoopEnd >= loopInterval)
            {
                // Reset the bird to its initial position after the loop interval
                transform.position = initialPosition;
                waitingToLoop = false;
            }
        }
    }
}
