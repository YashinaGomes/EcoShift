using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayController : MonoBehaviour
{
    [Header("Play componente referencias")]
    [SerializeField] Rigidbody2D rb;

    [Header("Play settings")]
    [SerializeField] float speed = 10;

    [Header("Hp controller")]
    [SerializeField] int vida;
    [SerializeField] int vidaMax;
    [SerializeField] Image[] hp;

    float horizontal;
    float vertical;

    private void FixedUpdate()
    {
        //movimentação do play utilizando o play input
        rb.linearVelocity = new Vector2(horizontal * speed, vertical * speed);
    }

    public void Move(InputAction.CallbackContext context)
    {
        //lê as movimentações do input
        horizontal = context.ReadValue<Vector2>().x;
        vertical = context.ReadValue<Vector2>().y;
    }

    void UpdateHpDisplay()
    {
        // impede valor negativo
        if (vida < 1)
            vida = 1;

        // desativa todas
        for (int i = 0; i < hp.Length; i++)
        {
            hp[i].gameObject.SetActive(false);
        }

        // ativa a imagem correta
        hp[vidaMax - vida].gameObject.SetActive(true);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Finish"))
        {
            vida--;

            UpdateHpDisplay();

            if (vida <= 1)
            {
                GameManager.Instance.GameOver();
                Debug.Log("Play morreu");
            }
        }
    }
    }