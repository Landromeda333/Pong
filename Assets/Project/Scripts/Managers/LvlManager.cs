using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvlManager : MonoBehaviour
{
    public static LvlManager Instance;

    [SerializeField] GameEvent startToPlay, onGameFinshed;                                          //

    public List<BallMovement> balls = new List<BallMovement>();                                     // La bola que se va a instanciar una vez que se destruya

    [SerializeField] GameObject[] aceletationPwrUp, reduceOpPwrUp, incrSelfPwrUp, MultBallsPwrUp;
    [SerializeField] List<GameObject[]> powerUps = new List<GameObject[]>();                        // Lista de PowerUps

    float powerUpsTimer;                                                                            // Tiempo aleatorio de aparición de los PowerUps
    float chronometer;                                                                              // Cronómetro para la aparición de PowerUps

    [SerializeField] int goalsTarget = 10;                                                          // Límite de goles
    int[] playersScore = new int[2];                                                                //

    public bool[] playerStateReady = new bool[2];                                                   // Espera a que el Jugador1 esté listo
    [HideInInspector] public bool gameStarted;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        powerUps.Add(aceletationPwrUp);
        powerUps.Add(reduceOpPwrUp);
        powerUps.Add(incrSelfPwrUp);
        powerUps.Add(MultBallsPwrUp);
        powerUpsTimer = UnityEngine.Random.Range(5f, 20f);                                          // Tiempo aleatorio para la aparición de PowerUps
        chronometer = Time.time;
        balls[0].transform.position = Vector3.zero;
        GameManager.Instance.SetGameState(GameManager.GameState.Preparation);
    }

    private void OnDisable()
    {
        InputsGameManager.Instance.playersMovement.Clear();
    }

    void Update()
    {
        // Aparición de PowerUps
        if (Time.time - chronometer >= powerUpsTimer)                                                                                                                      // Si ha llegado al tiempo definido aleatoriamente
        {
            chronometer = Time.time;                                                                                                                                  // Se reinicia el tiempo
            powerUpsTimer = UnityEngine.Random.Range(5, 20);                                                                                                               // Se define otro tiempo aleatorio
            foreach (GameObject pwrUp in powerUps[UnityEngine.Random.Range(0, powerUps.Count)])
            {
                if (!pwrUp.activeSelf)
                {
                    pwrUp.transform.position = new Vector2(UnityEngine.Random.Range(-7,7), UnityEngine.Random.Range(-4, 4));
                    pwrUp.SetActive(true);
                    return;
                }
            }
        }
    }

    /* Métodos */

    // Cambiar estado de los jugadores
    public void ChangePlayerState(int playerNum)
    {
        if (GameManager.Instance.gameState == GameManager.GameState.Preparation && !gameStarted)
        {
            playerStateReady[playerNum - 1] = true;
            PlayerReady?.Invoke(playerNum);
            for (int i = 0; i < 2; i++)
            {
                if (!playerStateReady[i])
                {
                    return;
                }
                else if (i == 1)
                {
                    startToPlay.Raise();
                    GameManager.Instance.SetGameState(GameManager.GameState.InGame);
                    AudioManager.Instance.PlayClip(AudioManager.Instance.countDownClip);
                }
            }
        }
    }

    // Actualización de puntuación
    public void UpdateScore(int playerScore)
    {
        playersScore[playerScore - 1]++;
        ScoreChanged?.Invoke(playersScore[0], playersScore[1]);
        if (playersScore[playerScore-1] >= goalsTarget)
        {
            GameManager.Instance.SetGameState(GameManager.GameState.GameOver);
        }
        else
        {
            foreach (BallMovement ball in balls)
            {
                if (ball.gameObject.activeSelf)
                {
                    return;
                }
            }
            GameManager.Instance.SetGameState(GameManager.GameState.Preparation);
            if (playerScore == 1)                                                                   // Si han marcado al Jugador1
            {
                balls[0].transform.position = new Vector2(-7, 0);
            }
            else                                                                                    // Si han marcado al Jugador2
            {
                balls[0].transform.position = new Vector2(7, 0);                                    // Se reinicia el estado del gol
            }
            balls[0].gameObject.SetActive(true);
            InputsGameManager.Instance.playersMovement[playerScore - 1].canGoalKick = true;
        }
    }

    // Multiple Balls Power Up
    public void StartMultipleBalls(Vector2 location)
    {
        foreach (BallMovement ball in balls)
        {
            if (!ball.gameObject.activeSelf)
            {
                ball.transform.position = location;
                Vector2 moveDirection = new Vector2(UnityEngine.Random.Range(-5, 5), UnityEngine.Random.Range(-5, 5));                                                                                 // Se asigna una dirección
                while (moveDirection.x == 0)                                                                                                                           // Para que la bola no se quede atrapada en bucle, se le obliga a tener una dirección en X
                {
                    moveDirection.x = UnityEngine.Random.Range(-5, 5);
                }
                ball.gameObject.SetActive(true);                                                                                         // Se instancia una bola
                ball.rb.AddForce(moveDirection, ForceMode2D.Impulse);
            }
        }
    }

    public void StartGame()
    {
        AudioManager.Instance.MakeTransition(AudioManager.Instance.backgroundMusic, AudioManager.Instance.countDownClip.length);
        balls[0].rb.AddForce(new Vector2(-4f, UnityEngine.Random.Range(-5f, 5f)), ForceMode2D.Impulse);
        gameStarted = true;
    }

    // Reinicio del nivel
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Fin de partida
    public void GameOver()
    {
        AudioManager.Instance.MakeTransition(AudioManager.Instance.victoryClip, 0);
        onGameFinshed.Raise();
    }

    /* Eventos */
    public static event Action<int, int> ScoreChanged;
    public static event Action<int> PlayerReady;
}