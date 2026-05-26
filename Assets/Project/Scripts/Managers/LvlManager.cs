using UnityEngine;

//# Este script se encarga de gestionar el estado del nivel #//
public class LvlManager : MonoBehaviour
{
    /* SO Events */
    [SerializeField] GameEvent onPlayersReady;                                  // Avisa de que todos los jugadores están preparados
    [SerializeField] IntGameEvent canGoalKick, onGameOver;                      // Avisa quien saca, Avisa del fin de partida
    [SerializeField] StringGameEvent loadSceneRequest;                          // Solicitud de carga de nivel

    [SerializeField] int goalsTarget = 10;                                      // Límite de goles

    [HideInInspector] public bool[] playerStateReady;                           // Estados de los jugadores
    [HideInInspector] public bool gameStarted;                                  // Controla si la partida ha comenzado

    private void Start()
    {
        playerStateReady = new bool[GameManager.Instance.playersAmount];                             // Crea un array con el tamaño correspondiente a la cantidad de jugadores
        BallsPool.Instance.GetPooledObject().transform.position = Vector3.zero; // Pone la bola en el centro
        GameManager.Instance.SetGameState(GameManager.GameState.Preparation);   // Cambia el estado de juego
    }

    /* Método para SO Event OnPlayerReady */
    // Cambiar estado de los jugadores
    public void ChangePlayerState(int playerNum)
    {
        if (GameManager.Instance.gameState == GameManager.GameState.Preparation && !gameStarted)
        {
            playerStateReady[playerNum - 1] = true;
            for (int i = 0; i < playerStateReady.Length; i++)
            {
                if (!playerStateReady[i])
                {
                    return;
                }
            }
            onPlayersReady.Raise();                                             // Avisa de que los jugadores están listos
            GameManager.Instance.SetGameState(GameManager.GameState.InGame);    // Cambia el estado del juego
            AudioManager.Instance.PlayClip(AudioManager.Instance.countDownClip);// Reproduce el audio de la cuenta atrás
        }
    }

    /* Método para SO Event StartToPlay */
    // Reacciona al comienzo de la partida
    public void StartGame()
    {
        AudioManager.Instance.MakeTransition(AudioManager.Instance.backgroundMusic, AudioManager.Instance.countDownClip.length);
        gameStarted = true;
    }

    /* Método para SO Event OnGameOver */
    // Fin de partida
    public void GameOver()
    {
        AudioManager.Instance.MakeTransition(AudioManager.Instance.victoryClip, 0);
    }

    /* Método para SO Event PlayerScored */
    // Comprueba que si ha llegado al final o no
    public void CheckScore(int playerNum, int score)
    {
        // Si ha conseguido los puntos necesarios
        if (score >= goalsTarget)
        {
            onGameOver.Raise(playerNum);
        }
        // Si ya no hay pelotas activas (para el caso en el que se haya activado el power up de las bolas múltiples)
        else if (!BallsPool.Instance.CheckActiveBalls())
        {
            GameManager.Instance.SetGameState(GameManager.GameState.Preparation);               // Cambia el estado
            if (playerNum == 1)                                                                 // Si han marcado al Jugador1
            {
                BallsPool.Instance.GetPooledObject().transform.position = new Vector2(-7, 0);   // Reposiciona la pelota
            }
            else                                                                                // Si han marcado al Jugador2
            {
                BallsPool.Instance.GetPooledObject().transform.position = new Vector2(7, 0);
            }
            canGoalKick.Raise(playerNum);                                                       // Avisa quien puede hacer el saque
        }
    }
}