using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ObjectPickupController : MonoBehaviour
{
    public GameObject uiElement;
    public float displayTime = 2.0f;
    public Transform player;
    public float pickupDistance = 2.0f;
    public GameObject objectToDisappear;
    public StageTransitionController stageTransitionController; // Reference to StageTransitionController

    private bool isNearObject = false;

    private void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= pickupDistance)
        {
            isNearObject = true;

            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(DisplayPickupUI());
            }
        }
        else
        {
            isNearObject = false;
        }
    }

    private IEnumerator DisplayPickupUI()
    {
        if (uiElement != null)
        {
            uiElement.SetActive(true);
            yield return new WaitForSeconds(displayTime);
            uiElement.SetActive(false);
        }

        if (objectToDisappear != null)
        {
            // Notify StageTransitionController that this item is collected
            if (stageTransitionController != null)
            {
                stageTransitionController.MarkItemAsCollected(gameObject);
            }
            Destroy(objectToDisappear);
        }
    }
}
