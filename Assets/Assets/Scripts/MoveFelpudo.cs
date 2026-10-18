using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movimentação")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 12f;


    private Rigidbody2D rb;
    [Header("Colisão")]
    private bool isGrounded;
    [SerializeField] private GameOverManager gameOverManager;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Pular ao pressionar Espaço
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        // Movimento automático para a direita
        rb.linearVelocity = new Vector2(
            speed,
            rb.linearVelocity.y
        );
    }

    void Jump()
    {
        rb.linearVelocity = new Vector2(
            rb.linearVelocity.x,
            jumpForce
        );

        isGrounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        if (collision.gameObject.CompareTag("Rock"))
        {
            gameOverManager.GameOver();
        }
        if (collision.gameObject.CompareTag("Box"))
        {
            gameOverManager.GameOver();
        }
        if (collision.gameObject.CompareTag("Fruit"))
        {
            gameOverManager.AddPoint();

            Destroy(collision.gameObject);
        }
    }
}
