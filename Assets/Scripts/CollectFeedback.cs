using UnityEngine;
using TMPro;
using System.Collections;

public class CollectFeedback : MonoBehaviour
{
    public static CollectFeedback Instance;

    public GameObject messageObject;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowMessage()
    {
        StopAllCoroutines();
        StartCoroutine(ShowRoutine());
    }

    IEnumerator ShowRoutine()
    {
        messageObject.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        messageObject.SetActive(false);
    }
}