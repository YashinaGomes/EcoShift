using UnityEngine;

public class Itens : MonoBehaviour
{
    string nomePlayer = "Player";
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(nomePlayer))
        {
            Destroy(gameObject);
        }
    }
}
