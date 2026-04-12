using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField] AudioClip collisionSound;                                                            // Audio para cada vez que choque

    public Rigidbody2D rb;

    public float speed = 6;                                                                               // Velocidad de la bola
    public int lastPlayerTouched;                                                                      // Se guarda el último toque para aplicarlo a los Power Ups
    public int lastTouched;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnDisable()
    {
        rb.linearVelocity = Vector2.zero;
    }

    // Función para los rebotes y sonidos de la pelota
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))                                                         // Si choca contra un jugador
        {
            if (transform.position.x < 0)
            {
                lastPlayerTouched = 1;                                                                // Se guarda el último jugador que ha tocado la bola
            }
            else
            {
                lastPlayerTouched = 2;
            }

            if (speed < 30)
            {
                speed += 0.5f;                                                                            // Se aumenta la velocidad cada vez que interactúa con un jugador
            }
            GetComponent<AudioSource>().PlayOneShot(collisionSound);                                      // Emite un sonido al chocar
        }
    }
}