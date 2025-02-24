using UnityEngine;

public class shadow : MonoBehaviour
{
    public Transform directionalLight;  // The directional light in the scene

    // Arrays of shadow sprites for different states
    public SpriteRenderer[] idleShadowSprites;    // Array of idle shadow sprites
    public SpriteRenderer[] movingShadowSprites;  // Array of moving shadow sprites
    public SpriteRenderer[] jumpingShadowSprites; // Array of jumping shadow sprites

    public Vector3 shadowOffset = new Vector3(0, -0.1f, 0);  // Customizable position offset for the shadow
    public float maxDistance = 10f;  // The maximum distance for shadow visibility
    public float minOpacity = 0.1f;  // Minimum opacity (when far from the light)
    public float maxOpacity = 1.0f;  // Maximum opacity (when close to the light)

    private SpriteAnimator characterAnimator; // Reference to the SpriteAnimator script
    private int currentShadowFrame = 0; // Track the current shadow frame
    private string lastState = ""; // Track the last player state to check for changes
    private float animationTimer = 0f; // Timer for sprite animation

    public float animationSpeed = 0.1f; // Time interval for changing sprites

    void Start()
    {
        // Get the SpriteAnimator component
        characterAnimator = GetComponent<SpriteAnimator>();

        // Ensure at least one sprite array has been assigned
        if (idleShadowSprites.Length > 0)
        {
            // Set the first idle shadow sprite as a child of the character and apply the offset
            idleShadowSprites[currentShadowFrame].transform.SetParent(transform);
            idleShadowSprites[currentShadowFrame].transform.localPosition = shadowOffset;

            // Disable all shadow sprites at the start
            DisableAllShadowSprites();
            idleShadowSprites[currentShadowFrame].gameObject.SetActive(true); // Enable the default idle shadow sprite
        }
        else
        {
            Debug.LogError("No idle shadow sprites assigned! Please assign the idleShadowSprites in the Inspector.");
        }
    }

    void Update()
    {
        UpdateShadowState();
    }

    // Update shadow state based on player sprite behavior
    private void UpdateShadowState()
    {
        string playerState = characterAnimator.GetCurrentState(); // Assume GetCurrentState() returns "Idle", "Moving", or "Jumping"

        // Only update if the player state has changed
        if (playerState != lastState)
        {
            lastState = playerState; // Update the last state

            // Reset shadow frame based on player state
            if (playerState == "Idle" && idleShadowSprites.Length > 0)
            {
                currentShadowFrame = 0;
                ActivateShadowSprite(idleShadowSprites);
            }
            else if (playerState == "Moving" && movingShadowSprites.Length > 0)
            {
                currentShadowFrame = 0; // Reset to first moving sprite
                ActivateShadowSprite(movingShadowSprites);
            }
            else if (playerState == "Jumping" && jumpingShadowSprites.Length > 0)
            {
                currentShadowFrame = 0; // Reset to first jumping sprite
                ActivateShadowSprite(jumpingShadowSprites);
            }
            else
            {
                DisableAllShadowSprites(); // Disable all shadows if no valid state
            }
        }

        // Update shadow position and opacity regardless of state changes
        if (idleShadowSprites.Length > 0 && lastState == "Idle")
        {
            UpdateShadowPositionAndOpacity(idleShadowSprites);
            AnimateShadowSprite(idleShadowSprites);
        }
        else if (movingShadowSprites.Length > 0 && lastState == "Moving")
        {
            UpdateShadowPositionAndOpacity(movingShadowSprites);
            AnimateShadowSprite(movingShadowSprites);
        }
        else if (jumpingShadowSprites.Length > 0 && lastState == "Jumping")
        {
            UpdateShadowPositionAndOpacity(jumpingShadowSprites);
            AnimateShadowSprite(jumpingShadowSprites);
        }
    }

    // Method to activate the appropriate shadow sprites
    private void ActivateShadowSprite(SpriteRenderer[] shadowSprites)
    {
        DisableAllShadowSprites(); // Disable all shadows first

        // Enable the current shadow sprite
        shadowSprites[currentShadowFrame].gameObject.SetActive(true);
    }

    // Method to update shadow position and opacity
    private void UpdateShadowPositionAndOpacity(SpriteRenderer[] shadowSprites)
    {
        shadowSprites[currentShadowFrame].transform.localPosition = shadowOffset;

        // Calculate distance between character and the directional light
        float distanceToLight = Vector3.Distance(transform.position, directionalLight.position);

        // Adjust the shadow's opacity based on proximity to the light
        float opacity = Mathf.Clamp(1 - (distanceToLight / maxDistance), minOpacity, maxOpacity);
        Color shadowColor = shadowSprites[currentShadowFrame].color;
        shadowColor.a = opacity;
        shadowSprites[currentShadowFrame].color = shadowColor;
    }

    // Method to animate shadow sprites based on player movement
    private void AnimateShadowSprite(SpriteRenderer[] shadowSprites)
    {
        // Update animation timer
        animationTimer += Time.deltaTime;
        if (animationTimer >= animationSpeed)
        {
            animationTimer = 0f; // Reset timer

            // Only animate if the shadow sprite is active
            if (shadowSprites[currentShadowFrame].gameObject.activeSelf)
            {
                // Check if there is more than one sprite
                if (shadowSprites.Length > 1)
                {
                    // Disable the previous shadow sprite
                    shadowSprites[currentShadowFrame].gameObject.SetActive(false);
                }

                // Move to the next sprite
                currentShadowFrame = (currentShadowFrame + 1) % shadowSprites.Length;

                // Enable the current shadow sprite
                shadowSprites[currentShadowFrame].gameObject.SetActive(true);
            }
        }
    }

    // Method to disable all shadow sprites
    private void DisableAllShadowSprites()
    {
        foreach (SpriteRenderer sprite in idleShadowSprites)
        {
            sprite.gameObject.SetActive(false);
        }
        foreach (SpriteRenderer sprite in movingShadowSprites)
        {
            sprite.gameObject.SetActive(false);
        }
        foreach (SpriteRenderer sprite in jumpingShadowSprites)
        {
            sprite.gameObject.SetActive(false);
        }
    }
}
