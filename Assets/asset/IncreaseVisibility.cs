using UnityEngine;

public class IncreaseVisibility : MonoBehaviour
{
    public Transform player; // Assign your player object in the inspector
    public SpriteRenderer spriteRenderer; // Option 1: Attach the SpriteRenderer component for 2D objects
    public Renderer objectRenderer; // Option 2: Attach the Renderer for 3D objects like a cube (with material)
    public float proximityRange = 3f; // Distance to start reducing visibility
    public float initialVisibility = 0.5f; // Starting visibility (0 is invisible, 1 is fully visible)
    public float reducedVisibility = 0.2f; // Visibility when the player is close

    private float originalVisibility;

    void Start()
    {
        // Set initial visibility at the start
        if (spriteRenderer != null) // For 2D objects
        {
            Color color = spriteRenderer.color;
            color.a = initialVisibility;
            spriteRenderer.color = color;
            originalVisibility = initialVisibility;
        }
        else if (objectRenderer != null) // For 3D objects
        {
            Color color = objectRenderer.material.color;
            color.a = initialVisibility;
            objectRenderer.material.color = color;
            originalVisibility = initialVisibility;
        }
    }

    void Update()
    {
        // Calculate the distance between the player and the object
        float distance = Vector3.Distance(player.position, transform.position);

        // Check if the player is below the object
        bool isPlayerBelow = player.position.y < transform.position.y;

        // Only reduce visibility if the player is within proximityRange and below the object
        if (distance <= proximityRange && isPlayerBelow)
        {
            if (spriteRenderer != null) // 2D object
            {
                Color color = spriteRenderer.color;
                color.a = reducedVisibility; // Reduce visibility
                spriteRenderer.color = color;
            }
            else if (objectRenderer != null) // 3D object with material
            {
                Color color = objectRenderer.material.color;
                color.a = reducedVisibility; // Reduce visibility
                objectRenderer.material.color = color;
            }
        }
        else
        {
            // Gradually restore visibility as the player moves away or is above the object
            float normalizedDistance = Mathf.Clamp01((distance - proximityRange) / proximityRange);

            if (spriteRenderer != null) // 2D object
            {
                Color color = spriteRenderer.color;
                color.a = Mathf.Lerp(reducedVisibility, originalVisibility, normalizedDistance); // Restore visibility
                spriteRenderer.color = color;
            }
            else if (objectRenderer != null) // 3D object with material
            {
                Color color = objectRenderer.material.color;
                color.a = Mathf.Lerp(reducedVisibility, originalVisibility, normalizedDistance); // Restore visibility
                objectRenderer.material.color = color;
            }
        }
    }
}
