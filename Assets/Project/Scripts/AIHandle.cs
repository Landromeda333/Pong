using UnityEngine;

public class AIHandle : MonoBehaviour
{
    float speed = 1;                                                                                    // Velocidad de movimiento del jugador
    float direction;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.linearVelocityY = direction * speed;
    }

    // Detección de colisiones con las paredes
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Wall"))                                               // Detección de la pared de abajo
        {
            GetComponent<AudioSource>().Play();                                                         // Se reproduce el sonido
        }
    }
}