using UnityEngine;
using System.Collections;
using System;
public class PlayerMovement : MonoBehaviour
{
    [Header("Movimentação")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 12f;

    [Header("Colisão")]
    private bool isGrounded;
    private bool hasWon = false;
    [SerializeField] private CollisionParticles collisionParticles;
    [SerializeField] private GameManager gameManager;

    private Rigidbody2D rb;


    #region MOVE AND JUMP
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
    #endregion

    #region COLLISIONS
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 point = collision.contacts[0].point;
        HandleCollision(collision.gameObject, point);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Vector2 point = other.ClosestPoint(transform.position);
        HandleCollision(other.gameObject, point);
    }

    private void HandleCollision(GameObject obj, Vector2 point)
    {
        if (obj.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else if (obj.CompareTag("Finish"))
        {
            speed = 3.5f;
        }
        else if (obj.CompareTag("Rock"))
        {
            if (collisionParticles != null)
                collisionParticles.PlayParticles("Rock", point);

            if (gameManager != null)
                gameManager.GameOver();
        }
        else if (obj.CompareTag("Box"))
        {
            if (collisionParticles != null)
                collisionParticles.PlayParticles("Box", point);

            if (gameManager != null)
                gameManager.GameOver();
        }
        else if (obj.CompareTag("Fruit"))
        {
            if (collisionParticles != null)
                collisionParticles.PlayParticles("Fruit", point);

            if (gameManager != null)
                gameManager.AddPoint();

            Destroy(obj);
        }
        else if (obj.CompareTag("Love"))
        {
            if (hasWon)
                return;
            hasWon = true;

            speed = 0;
                
            if (collisionParticles != null)
                collisionParticles.PlayParticles("Love", point);
            //gameManager.WinGame();
            StartCoroutine(VictorySequence());
        }
    }
    private IEnumerator VictorySequence()
    {
        yield return new WaitForSeconds(1f);

        if (gameManager != null)
            gameManager.WinGame();
    }
    #endregion
}
