using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class StageTransitionController : MonoBehaviour
{
    public string nextStageName;
    public GameObject[] requiredItems;
    public GameObject player;
    public float interactDistance = 2.0f;
    public Text remainingItemsText;
    public float textDisplayDuration = 2.0f;

    [Header("Custom Messages")]
    public string remainingItemsMessage = "You still need to collect {0} object(s)";
    public string allItemsCollectedMessage = "All items collected! Proceed to the next stage";

    private bool itemsObtained = false;
    private Coroutine textCoroutine;

    private void Start()
    {
        if (remainingItemsText != null)
        {
            remainingItemsText.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance <= interactDistance && Input.GetKeyDown(KeyCode.E))
        {
            TryTransition();
        }
    }

    private void TryTransition()
    {
        int remainingItems = CountRemainingItems();

        if (remainingItems == 0)
        {
            GameTimer gameTimer = FindObjectOfType<GameTimer>();
            if (gameTimer != null)
            {
                gameTimer.SaveTimeAndLoadNextScene();
            }
            else
            {
                Debug.LogError("GameTimer instance not found!");
            }
        }
        else
        {
            ShowRemainingItemsText(remainingItems);
        }
    }

    private int CountRemainingItems()
    {
        int remainingItems = 0;
        foreach (GameObject item in requiredItems)
        {
            if (item != null)
            {
                remainingItems++;
            }
        }
        return remainingItems;
    }

    public void MarkItemAsCollected(GameObject collectedItem)
    {
        for (int i = 0; i < requiredItems.Length; i++)
        {
            if (requiredItems[i] == collectedItem)
            {
                requiredItems[i] = null; // Mark the item as collected
                break;
            }
        }
    }

    private void ShowRemainingItemsText(int remainingItems)
    {
        if (remainingItemsText != null)
        {
            remainingItemsText.text = string.Format(remainingItemsMessage, remainingItems);

            if (!remainingItemsText.gameObject.activeSelf)
            {
                remainingItemsText.gameObject.SetActive(true);

                if (textCoroutine != null)
                {
                    StopCoroutine(textCoroutine);
                }

                textCoroutine = StartCoroutine(HideTextAfterDelay());
            }
        }
    }

    private IEnumerator HideTextAfterDelay()
    {
        yield return new WaitForSeconds(textDisplayDuration);

        if (remainingItemsText != null)
        {
            remainingItemsText.gameObject.SetActive(false);
        }
    }
}
