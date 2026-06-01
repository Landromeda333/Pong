using System.Collections;
using UnityEngine;

public class AIBehaviour : MonoBehaviour,IPreparable
{
    /* SO Events */
    [SerializeField] IntGameEvent onPlayerReady;
    [SerializeField] Vector2GameEvent kick;                                          // Saque de puerta

    /* Scripts */
    PlayerData data;
    [HideInInspector] public AIMovement movement;                                // Script del movimiento del jugador

    private void Awake()
    {
        data = GetComponent<PlayerData>();
        movement = GetComponent<AIMovement>();
    }

    private void OnEnable()
    {
        GameManager.Instance.RegisterPreparable(this);
    }

    private void OnDisable()
    {
        GameManager.Instance.UnregisterPreparable(this);
    }

    private void Start()
    {
        onPlayerReady.Raise(data.playerNum);
    }

    /* Métodos para IPreparable */
    // Se prepara para el saque de puerta. Ya sea suyo o del contrario
    public void OnPreparation(int playerToGoalKick)
    {
        transform.position = new Vector2(transform.position.x, 0);
        if (playerToGoalKick == data.playerNum)
        {
            StartCoroutine(GoalKick());
        }
    }

    /* Corrutinas */
    // Saque de puerta
    IEnumerator GoalKick()
    {
        yield return new WaitForSecondsRealtime(Random.Range(1,4));
        GameManager.Instance.SetGameState(GameManager.GameState.InGame);
        kick.Raise(new Vector2(4 * (int)data.kickDir, movement.direction));
    }
}