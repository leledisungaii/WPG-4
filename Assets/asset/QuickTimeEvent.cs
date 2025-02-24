using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuickTimeEvent : MonoBehaviour
{
    public GameObject qteCanvas;         // Canvas for the QTE prompt
    public GameObject objectToMove;      // The object that will move (e.g., a cube)
    public Slider qteSlider;             // Slider for the QTE
    public float moveSpeed = 1f;         // Speed at which the object moves
    public float moveAmount = 5f;        // How much the object should move
    public bool moveUp = false;          // Whether the object should move up or down
    public string nextSceneName;         // Name of the next scene to load
    public float greenZoneMin = 0.4f;    // Minimum slider value for success (green zone start)
    public float greenZoneMax = 0.6f;    // Maximum slider value for success (green zone end)
    public float qteSpeed = 2f;          // Speed of the slider moving
    public Transform player;             // Reference to the player character's transform
    public float activationDistance = 5f; // Distance within which QTE activates
    private bool qteActive = false;      // Whether the QTE is active
    private bool playerInRange = false;  // Whether the player is near the object
    private Vector3 originalPosition;    // Original position of the object
    private bool sliderIncreasing = true; // Direction of slider movement

    void Start()
    {
        qteCanvas.SetActive(false);      // Initially hide the QTE canvas
        objectToMove.SetActive(false);   // Disable the object at the start
        qteSlider.gameObject.SetActive(false); // Disable the slider at the start
        originalPosition = objectToMove.transform.position; // Store the original position of the object
        qteSlider.value = 0;             // Initialize slider value
    }

    void Update()
    {
        // Check the distance between player and the specific object (this object)
        if (Vector3.Distance(player.position, transform.position) <= activationDistance)
        {
            playerInRange = true;
        }
        else
        {
            playerInRange = false;
        }

        if (qteActive)
        {
            // Move the slider back and forth
            MoveSlider();

            // Detect when the player presses the Enter key
            if (Input.GetKeyDown(KeyCode.Return))
            {
                CheckQTEResult(); // Check if the player lands in the green zone
            }
        }
        else
        {
            // Start the QTE only when the player presses the E key and is within range
            if (Input.GetKeyDown(KeyCode.E) && playerInRange)
            {
                StartQTE();
            }
        }
    }

    void StartQTE()
    {
        qteActive = true;
        qteCanvas.SetActive(true);   // Show the QTE canvas
        qteSlider.gameObject.SetActive(true); // Enable the slider
        qteSlider.value = 0;         // Reset the slider to the beginning
    }

    void MoveSlider()
    {
        if (sliderIncreasing)
        {
            qteSlider.value += qteSpeed * Time.deltaTime;
            if (qteSlider.value >= 1f)
            {
                sliderIncreasing = false; // Reverse direction at max value
            }
        }
        else
        {
            qteSlider.value -= qteSpeed * Time.deltaTime;
            if (qteSlider.value <= 0f)
            {
                sliderIncreasing = true; // Reverse direction at min value
            }
        }
    }

    void CheckQTEResult()
    {
        qteActive = false;
        qteCanvas.SetActive(false);  // Hide the QTE canvas
        qteSlider.gameObject.SetActive(false); // Hide the slider

        if (qteSlider.value >= greenZoneMin && qteSlider.value <= greenZoneMax)
        {
            // Player succeeded, move the object and load the next scene
            objectToMove.SetActive(true);
            MoveObject();
        }
        else
        {
            // Player failed, QTE will cancel and require reactivation
            Debug.Log("Failed QTE, press E to try again.");
            // QTE will not restart automatically, player must press E again
        }
    }

    void MoveObject()
    {
        Vector3 targetPosition;
        if (moveUp)
        {
            targetPosition = originalPosition + new Vector3(0, moveAmount, 0);  // Move up by moveAmount
        }
        else
        {
            targetPosition = originalPosition - new Vector3(0, moveAmount, 0);  // Move down by moveAmount
        }

        StartCoroutine(MoveOverTime(targetPosition));
    }

    System.Collections.IEnumerator MoveOverTime(Vector3 targetPosition)
    {
        while (objectToMove.transform.position != targetPosition)
        {
            objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // Once the object has finished moving, load the next scene
        SceneManager.LoadScene(nextSceneName);
    }
}
