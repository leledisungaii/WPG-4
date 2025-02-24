using UnityEngine;

public class InteractableText : MonoBehaviour
{
    public GameObject textMeshObject; // The 3D TextMesh object
    public float interactionDistance = 3f; // Distance for player to be able to interact
    private bool isPlayerNear = false; // To track if the player is near
    private bool isTextVisible = false; // To track text visibility
    private Transform player; // Player's transform

    void Start()
    {
        // Assume the player has the "Player" tag
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Initially, hide the 3D text
        textMeshObject.SetActive(false);
    }

    void Update()
    {
        // Check if the player is within interaction distance
        if (Vector3.Distance(player.position, transform.position) <= interactionDistance)
        {
            isPlayerNear = true;

            // If the player presses 'E', toggle the text visibility
            if (Input.GetKeyDown(KeyCode.E))
            {
                isTextVisible = !isTextVisible; // Toggle text visibility
                textMeshObject.SetActive(isTextVisible); // Show/hide the text
            }
        }
        else
        {
            isPlayerNear = false;

            // Optionally, hide the text if the player moves away
            if (isTextVisible)
            {
                textMeshObject.SetActive(false);
                isTextVisible = false;
            }
        }
    }
}
