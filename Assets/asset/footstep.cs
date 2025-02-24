using UnityEngine;
using System.Collections;

public class footstep : MonoBehaviour
{
    [System.Serializable]
    public class FootstepMarker
    {
        public GameObject marker;     // The footstep GameObject
        public float stepDistance;    // Distance the character must move before placing this footstep
        public float duration;        // How long this footstep stays active
    }

    public FootstepMarker[] footstepMarkers;  // Array of footstep markers with individual settings
    public Vector3 footstepOffset = new Vector3(0, -0.1f, 0);  // Offset for footstep position relative to character

    private int currentFootstepIndex = 0;  // Index to track which footstep marker to use next
    private Vector3 lastFootstepPosition;  // To track the last footstep position

    private void Start()
    {
        // Disable all footstep markers initially
        foreach (var footstep in footstepMarkers)
        {
            footstep.marker.SetActive(false);
        }

        // Initialize the last footstep position
        lastFootstepPosition = transform.position;
    }

    private void Update()
    {
        // Get the current footstep marker
        FootstepMarker currentFootstep = footstepMarkers[currentFootstepIndex];

        // Check if the character has moved far enough to place a new footstep
        float distanceMoved = Vector3.Distance(lastFootstepPosition, transform.position);

        if (distanceMoved >= currentFootstep.stepDistance)
        {
            PlaceFootstep(currentFootstep);
        }
    }

    private void PlaceFootstep(FootstepMarker currentFootstep)
    {
        // Calculate the footstep position with an offset
        Vector3 footstepPosition = transform.position + footstepOffset;

        // Move the footstep marker to the new position and enable it
        currentFootstep.marker.transform.position = footstepPosition;
        currentFootstep.marker.SetActive(true);

        // Start the coroutine to disable the footstep after its duration
        StartCoroutine(DisableFootstepAfterTime(currentFootstep.marker, currentFootstep.duration));

        // Update the last footstep position to the current one
        lastFootstepPosition = transform.position;

        // Move to the next footstep marker in the array (circular index)
        currentFootstepIndex = (currentFootstepIndex + 1) % footstepMarkers.Length;
    }

    // Coroutine to disable the footstep after a set duration
    private IEnumerator DisableFootstepAfterTime(GameObject footstep, float duration)
    {
        yield return new WaitForSeconds(duration);
        footstep.SetActive(false);
    }
}
