using UnityEngine;

/* Este script se encarga de gestionar el movimiento del jugador */
public class AIMovement : MonoBehaviour
{
    /* Scripts */
    BallsDetection detection;

    /* Rigidbody2D */
    Rigidbody2D rb;

    /* Audio Source */
    AudioSource source;

    [HideInInspector] public float direction;                 // Dirección a la que debe moverse

    [SerializeField] float speed = 6f;                        // Velocidad de movimiento del jugador

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        source = GetComponent<AudioSource>();
        detection = GetComponentInChildren<BallsDetection>();
    }

    /* Movimiento del jugador */
    void FixedUpdate()
    {
        if (GameManager.Instance.gameState == GameManager.GameState.InGame)
        {
            if (detection.balls.Count > 0)
            {
                direction = Mathf.Clamp(detection.balls[0].transform.position.y - transform.position.y, -1, 1);
            }
            else
            {
                direction = Mathf.Clamp(-transform.position.y, -1, 1);
            }
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