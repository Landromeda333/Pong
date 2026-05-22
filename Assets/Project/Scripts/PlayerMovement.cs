using UnityEngine;
using static GameManager;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;

    AudioSource audSource;

    enum KickDirection
    {
        Right = 1,
        Left = -1
    }
    [SerializeField] KickDirection kickDir;

    public float direction;
    float speed = 6f;                                                           // Velocidad de movimiento del jugador

    [SerializeField] int playerNum;

    [HideInInspector] public bool canGoalKick;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        audSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if (InputsGameManager.Instance.playersMovement.Count > playerNum-1)
        {
            InputsGameManager.Instance.playersMovement.Insert(playerNum - 1, this);
        }
        else
        {
            InputsGameManager.Instance.playersMovement.Add(this);
        }
    }

    private void OnEnable()
    {
        InputsGameManager.Instance.ActivatePlayerInputs(playerNum);
    }

    private void OnDisable()
    {
        InputsGameManager.Instance.DeactivatePlayerInputs(playerNum);
    }

    /* Movimiento de la pala */
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
            audSource.Play();
        }
    }

    // Saque de puerta
    public void GoalKick()
    {
        if (canGoalKick)
        {
            Instance.SetGameState(GameState.InGame);
            LvlManager.Instance.balls[0].rb.AddForce(new Vector2(4 * (int)kickDir, direction), ForceMode2D.Impulse);
            canGoalKick = false;
        }
    }
}