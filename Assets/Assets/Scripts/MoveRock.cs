using UnityEngine;

public class MoveRock : MonoBehaviour
{
    [SerializeField] private float speed = 7f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        // Movimento automático para a direita
        rb.linearVelocity = new Vector2(
            -speed,
            rb.linearVelocity.y
        );
    }
}
