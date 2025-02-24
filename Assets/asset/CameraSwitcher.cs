using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] private Camera[] cameras; // Array to hold multiple cameras
    private int currentCameraIndex = 0; // Index of the currently active camera
    [SerializeField] private Transform player; // Reference to the player character
    private Vector3 cameraOffset; // Offset from the player's position

    private void Start()
    {
        // Disable all cameras and enable the first one
        foreach (Camera cam in cameras)
        {
            cam.enabled = false;
        }
        cameras[currentCameraIndex].enabled = true; // Enable the first camera

        // Store the initial offset for the second camera
        if (cameras.Length > 1)
        {
            cameraOffset = cameras[1].transform.position - player.position; // Calculate offset
        }
    }

    private void Update()
    {
        // Switch camera view when the player presses the "C" key
        if (Input.GetKeyDown(KeyCode.C))
        {
            SwitchCamera();
        }

        // Follow the player with the active camera
        FollowPlayer();
    }

    private void SwitchCamera()
    {
        // Disable the current camera
        cameras[currentCameraIndex].enabled = false;

        // Update the index to the next camera
        currentCameraIndex = (currentCameraIndex + 1) % cameras.Length;

        // Enable the next camera
        cameras[currentCameraIndex].enabled = true;
    }

    private void FollowPlayer()
    {
        // Follow the player with the active camera
        if (currentCameraIndex == 1) // Assuming the second camera is at index 1
        {
            // Update the camera's position using the stored offset
            cameras[currentCameraIndex].transform.position = player.position + cameraOffset;
            cameras[currentCameraIndex].transform.LookAt(player.position);
        }
    }
}
