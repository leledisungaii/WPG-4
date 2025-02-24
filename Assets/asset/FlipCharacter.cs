using UnityEngine;

public class FlipCharacter : MonoBehaviour
{
    private bool facingRight = true; // Start by assuming the character is facing right

    void Update()
    {
        // Get the horizontal input (A/D or Left/Right keys)
        float moveInput = Input.GetAxis("Horizontal");

        // If the input is moving right and the character is facing left, flip the character
        if (moveInput > 0 && !facingRight)
        {
            Flip();
        }
        // If the input is moving left and the character is facing right, flip the character
        else if (moveInput < 0 && facingRight)
        {
            Flip();
        }
    }

    // Flip the character by inverting the local scale on the X-axis
    void Flip()
    {
        facingRight = !facingRight; // Toggle the facing direction
        Vector3 characterScale = transform.localScale; // Get the current scale
        characterScale.x *= -1; // Invert the X-axis scale
        transform.localScale = characterScale; // Apply the new scale
    }
}
