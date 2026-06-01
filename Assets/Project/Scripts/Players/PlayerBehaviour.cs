using UnityEngine;

//# Este script se encarga del comportamiento del jugador #//
public class PlayerBehaviour : MonoBehaviour, IPreparable
{
    /* SO Events */
    [SerializeField] Int_PlayerBehaviourGameEvent registerPlayer;                    // Registro del jugador en los controles
    [SerializeField] IntGameEvent playerActivationRequest, playerDeactivationRequest;// Activación / Desactivación de los controles del jugador
    [SerializeField] Vector2GameEvent kick;                                          // Saque de puerta

    /* Scripts */
    [HideInInspector] public PlayerMovement movement;                                // Script del movimiento del jugador

    PlayerData data;

    bool canGoalKick;                                                                // Controla cuando puede sacar

    private void Awake()
    {
        data = GetComponent<PlayerData>();
        movement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        registerPlayer.Raise(data.playerNum, this);
    }

    private void OnEnable()
    {
        GameManager.Instance.RegisterPreparable(this);
        playerActivationRequest.Raise(data.playerNum);
    }

    private void OnDisable()
    {
        GameManager.Instance.UnregisterPreparable(this);
        playerDeactivationRequest.Raise(data.playerNum);
    }

    /* Métodos */
    // Saque de puerta
    public void GoalKick()
    {
        if (canGoalKick)
        {
            GameManager.Instance.SetGameState(GameManager.GameState.InGame);
            kick.Raise(new Vector2(4 * (int)data.kickDir, movement.direction));
            canGoalKick = false;
        }
    }

    /* Métodos para IPreparable */
    // Se prepara para el saque de puerta. Ya sea suyo o del contrario
    public void OnPreparation(int playerToGoalKick)
    {
        transform.position = new Vector2(transform.position.x, 0);
        canGoalKick = playerToGoalKick == data.playerNum;
    }
}