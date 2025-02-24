using UnityEngine;

public class OutLevel : MonoBehaviour
{
    public string outLevelID; // Unique ID corresponding to the InLevel trigger
    public Transform objectToMove; // The object that will be moved
    public Vector3 targetPosition; // The target position to move the object to

    void Start()
    {
        // Check with Loop to see if we need to move the object corresponding to this ID
        Loop.Instance.MoveOutLevelObject(this, outLevelID);
    }

    public void MoveObject()
    {
        if (objectToMove != null)
        {
            objectToMove.position = targetPosition;
        }
    }
}
