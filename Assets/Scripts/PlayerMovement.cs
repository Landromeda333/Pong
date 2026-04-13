using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    AudioSource audSource;
    public float direction;
    float speed = 6f;                                                           // Velocidad de movimiento del jugador
    [SerializeField] int playerNum;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        audSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if (LvlManager.Instance.players.Count > playerNum - 1)
        {
            LvlManager.Instance.players.Insert(playerNum - 1, gameObject);
        }
        else
        {
            LvlManager.Instance.players.Add(gameObject);
        }
    }

    private void OnEnable()
    {
        GameManager.Instance.ActivatePlayerInputs(playerNum, this);
    }

    private void OnDisable()
    {
        GameManager.Instance.DeactivatePlayerInputs(playerNum);
    }

    /* Movimiento de la pala */
    void FixedUpdate()
    {
        if (LvlManager.Instance.gameState == LvlManager.GameState.InGame)
        {
            rb.linearVelocityY = direction * speed;
        }
    }

    // Detección de colisiones con las paredes
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            audSource.Play();
        }
    }
}