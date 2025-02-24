using UnityEngine;

public class Visibility : MonoBehaviour
{
    public Transform player; // Assign your player object in the inspector
    public SpriteRenderer spriteRenderer; // Attach the SpriteRenderer component of the object
    public float proximityRange = 3f; // Distance to trigger visibility change
    public float reducedVisibility = 0.5f; // Reduced visibility (0 is invisible, 1 is fully visible)

    private float originalVisibility;

    void Start()
    {
        // Store the original visibility
        originalVisibility = spriteRenderer.color.a;
    }

    void Update()
    {
        // Calculate the distance between the player and the object
        float distance = Vector3.Distance(player.position, transform.position);

        // If the player is within range, reduce visibility
        if (distance <= proximityRange)
        {
            Color color = spriteRenderer.color;
            color.a = reducedVisibility; // Reduce visibility
            spriteRenderer.color = color;
        }
        else
        {
            // Reset visibility when the player moves away
            Color color = spriteRenderer.color;
            color.a = originalVisibility; // Reset visibility
            spriteRenderer.color = color;
        }
    }
}
