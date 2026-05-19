using UnityEngine;

public class CollectItem : MonoBehaviour
{
    public int points = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            Debug.Log("Item coletado!");

            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddScore(points);
            }

            CollectFeedback.Instance.ShowMessage();

            Destroy(gameObject);
        }
    }
}