using UnityEngine;
using static GameManager;

//# Este script se encarga del comportamiento del jugador #//
public class PlayerBehaviour : MonoBehaviour
{
    /* SO Events */
    [SerializeField] Int_PlayerBehaviourGameEvent registerPlayer;// Registro del jugador en los controles
    [SerializeField] IntGameEvent playerActivationRequest, playerDeactivationRequest;// Activación / Desactivación de los controles del jugador
    [SerializeField] Vector2GameEvent kick;                      // Saque de puerta

    /* Scripts */
    [HideInInspector] public PlayerMovement movement;            // Script del movimiento del jugador

    enum KickDirection
    {
        Right = 1,
        Left = -1
    }
    [SerializeField] KickDirection kickDir;                      // Dirección del saque

    public int playerNum;                                        // Número del jugador

    bool canGoalKick;                                            // Controla cuando puede sacar

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        registerPlayer.Raise(playerNum, this);
    }

    private void OnEnable()
    {
        playerActivationRequest.Raise(playerNum);
    }

    private void OnDisable()
    {
        playerDeactivationRequest.Raise(playerNum);
    }

    /* Métodos */
    // Saque de puerta
    public void GoalKick()
    {
        if (canGoalKick)
        {
            Instance.SetGameState(GameState.InGame);
            kick.Raise(new Vector2(4 * (int)kickDir, movement.direction));
            canGoalKick = false;
        }
    }

    /* Métodos para SO Event StartGoalKick */
    // Se prepara para el saque de puerta. Ya sea suyo o del contrario
    public void Prepare(int playerToGoalKick)
    {
        transform.position = new Vector2(transform.position.x, 0);
        if (playerToGoalKick == playerNum)
        {
            canGoalKick = true;
        }
    }
}