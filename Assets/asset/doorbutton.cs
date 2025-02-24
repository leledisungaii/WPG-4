using System.Collections;
using UnityEngine;

public class doorbutton : MonoBehaviour
{
    public GameObject player;          // Reference to the player (cube)
    public GameObject door;            // Reference to the door (cube)
    public float activationDistance = 3f;  // Distance within which the player can press the button
    public float doorMoveDistance = 5f;    // How much the door moves up/down
    public float doorMoveSpeed = 2f;       // How fast the door moves
    public KeyCode interactionKey = KeyCode.I;  // Changed the key to 'I' for interaction

    private Vector3 doorInitialPosition;
    private bool isDoorOpen = false;
    private bool isMoving = false;

    void Start()
    {
        doorInitialPosition = door.transform.position; // Save the initial position of the door
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        // Check if the player is near the button and presses the 'I' key
        if (distanceToPlayer <= activationDistance && Input.GetKeyDown(interactionKey) && !isMoving)
        {
            isMoving = true;
            StartCoroutine(MoveDoor());
        }
    }

    IEnumerator MoveDoor()
    {
        Vector3 targetPosition = isDoorOpen ? doorInitialPosition : doorInitialPosition + new Vector3(0, doorMoveDistance, 0);
        float elapsedTime = 0f;

        while (elapsedTime < doorMoveSpeed)
        {
            door.transform.position = Vector3.Lerp(door.transform.position, targetPosition, elapsedTime / doorMoveSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        door.transform.position = targetPosition; // Ensure final position is correct
        isDoorOpen = !isDoorOpen;
        isMoving = false;
    }

    private void OnDrawGizmosSelected()
    {
        // Visualize the activation distance in the editor
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, activationDistance);
    }
}
