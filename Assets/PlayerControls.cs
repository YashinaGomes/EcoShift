using UnityEngine;

public class MovimentoSetas : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float speedBoost = 10f;

    private Rigidbody2D _rb;

    private Vector2 movimento;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        movimento = new Vector2(x, y).normalized;

        PlayerFlip();

        if (Input.GetKey(KeyCode.RightShift))
        {
            speed = speedBoost;
        }
        else
        {
            speed = 5f;
        }

    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + movimento * speed * Time.fixedDeltaTime);
    }

    void PlayerFlip()
    {
        if (movimento.x > 0)
        {
            transform.eulerAngles = new Vector3(0, 0);
        }
        else if (movimento.x < 0)
        {
            transform.eulerAngles = new Vector3(0, 180);
        }
    }
}
