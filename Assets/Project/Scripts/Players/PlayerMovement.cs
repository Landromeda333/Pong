using UnityEngine;
using static GameManager;

/* Este script se encarga de gestionar el movimiento del jugador */
public class PlayerMovement : MonoBehaviour
{
    /* Rigidbody2D */
    Rigidbody2D rb;

    /* Audio Source */
    AudioSource source;

    [HideInInspector] public float direction;// Dirección a la que debe moverse
    float speed = 6f;                        // Velocidad de movimiento del jugador

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        source = GetComponent<AudioSource>();
    }

    /* Movimiento del jugador */
    void FixedUpdate()
    {
        if (Instance.gameState == GameState.InGame)
        {
            rb.linearVelocityY = direction * speed;
        }
    }

    // Detección de colisiones con las paredes
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            source.Play();
        }
    }

    /* Métodos para SO Event OnPauseRequest */
    public void StopMovement()
    {
        direction = 0;
    }
}