using UnityEngine;

public class camerafollow : MonoBehaviour
{
    public Transform player;             // Reference to the player's transform
    public float smoothSpeed = 0.125f;   // Smoothing speed for camera movement
    public Vector3 offset;               // Offset to maintain between the camera and the player
    public Vector3 initialRotation;       // To set and modify the camera's initial rotation

    void Start()
    {
        // Set the camera's initial rotation from the public variable
        transform.eulerAngles = initialRotation;
    }

    void LateUpdate()
    {
        // Calculate the desired position based on the player's position and the offset
        Vector3 desiredPosition = new Vector3(player.position.x + offset.x, player.position.y + offset.y, player.position.z + offset.z);

        // Smoothly transition to the new position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Maintain the modified initial rotation
        transform.eulerAngles = initialRotation;
    }
}
