using UnityEngine;

/* Este script se encarga de gestionar el movimiento de la bola */
public class BallMovement : MonoBehaviour
{
    /* Audio Source */
    AudioSource source;

    /* Audio Clip */
    [SerializeField] AudioClip collisionSound;      // Audio para cada vez que choque

    /* Rigidbody */
    public Rigidbody2D rb;

    public float speed;                             // Velocidad de la bola
    public float maxSpeed;                          // Velocidad máxima a la que puede llegar la bola
    [HideInInspector] public int lastPlayerTouched; // Guarda el último toque para aplicarlo a los Power Ups

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        source = GetComponent<AudioSource>();
    }

    // Reseteo de valores
    private void OnDisable()
    {
        rb.linearVelocity = Vector2.zero;
        lastPlayerTouched = 0;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))                                           // Si choca contra un jugador
        {
            SetLastPlayerTouched(collision.gameObject.GetComponent<PlayerBehaviour>().playerNum);// Guarda el jugador

            // Dependiendo de donde golpee añade velocidad hacia un lado u otro
            if (transform.position.x < 0)
            {
                rb.AddForceX(speed, ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForceX(-speed, ForceMode2D.Impulse);
            }
            source.PlayOneShot(collisionSound);
        }
    }

    /* Métodos para SO Event GoalKick */
    // Saque de puerta
    public void GoalKick(Vector2 kick)
    {
        rb.AddForce(kick, ForceMode2D.Impulse);
    }

    /* Método para SO Event StartToPlay */
    // Saque desde el centro
    public void MidleKick()
    {
        rb.AddForce(new Vector2(-4f, Random.Range(-5f, 5f)), ForceMode2D.Impulse);
    }

    /* Método para SO Event OnGameOver */
    // Reacción al fin de partida
    public void OnGameOver()
    {
        gameObject.SetActive(false);
    }

    /* Métodos para SO Event CanGoalKick */
    // Guardado del último jugador tocado
    public void SetLastPlayerTouched(int playerNumber)
    {
        lastPlayerTouched = playerNumber;
    }
}