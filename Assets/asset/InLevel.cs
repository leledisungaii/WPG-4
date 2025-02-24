using UnityEngine;

public class InLevel : MonoBehaviour
{
    public string inLevelID; // Unique ID for this InLevel object
    public float interactionRadius = 3f; // Radius within which the player can interact
    public Transform playerTransform; // Reference to the player's transform

    void Update()
    {
        if (IsPlayerInRange() && Input.GetKeyDown(KeyCode.E))
        {
            CollectItem();
        }
    }

    bool IsPlayerInRange()
    {
        // Check if the player's position is within the interaction radius
        return Vector3.Distance(playerTransform.position, transform.position) <= interactionRadius;
    }

    void CollectItem()
    {
        // Make the object disappear
        gameObject.SetActive(false);

        // Notify the Loop that this specific InLevel has been triggered
        Loop.Instance.TriggerInLevel(inLevelID);
    }

    // For better visualization in the Editor
    private void OnDrawGizmosSelected()
    {
        // Draw the interaction radius in the scene view
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}
