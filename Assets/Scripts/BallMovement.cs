using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField] AudioClip collisionSound;                                                            // Audio para cada vez que choque

    public Rigidbody2D rb;

    public float speed;                                                                               // Velocidad de la bola
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
        if (collision.gameObject.CompareTag("Player"))              // Si choca contra un jugador
        {
            if (transform.position.x < 0)
            {
                lastPlayerTouched = 1;                              // Se guarda el último jugador que ha tocado la bola
                rb.AddForceX(speed, ForceMode2D.Impulse);
            }
            else
            {
                lastPlayerTouched = 2;
                rb.AddForceX(-speed, ForceMode2D.Impulse);
            }
            GetComponent<AudioSource>().PlayOneShot(collisionSound);// Emite un sonido al chocar
        }
    }
}