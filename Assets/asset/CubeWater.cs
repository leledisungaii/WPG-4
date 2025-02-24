using UnityEngine;

public class CubeWater : MonoBehaviour
{
    // Speed of the wave movement
    public float waveSpeed = 0.5f;
    // Height of the wave movement
    public float waveHeight = 0.1f;

    // Update is called once per frame
    void Update()
    {
        // Create a simple wave effect by moving the cube up and down
        transform.position = new Vector3(transform.position.x, Mathf.Sin(Time.time * waveSpeed) * waveHeight, transform.position.z);
    }
}
