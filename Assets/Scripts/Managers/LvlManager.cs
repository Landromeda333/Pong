using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvlManager : MonoBehaviour
{
    public static LvlManager Instance;

    public enum GameState
    {
        MainMenu,
        Preparation,
        InGame,
        Pause,
        GameOver
    }
    public GameState gameState;

    [SerializeField] List<BallMovement> balls = new List<BallMovement>();                                                                                                                              // La bola que se va a instanciar una vez que se destruya

    [SerializeField] GameObject[] aceletationPwrUp, reduceOpPwrUp, incrSelfPwrUp, MultBallsPwrUp;
    [SerializeField] List<GameObject[]> powerUps = new List<GameObject[]>();                                                                                                               // Lista de PowerUps
    public GameObject[] players;

    float powerUpsTimer;                                                                                                                                           // Tiempo aleatorio de aparición de los PowerUps
    float chronometer;                                                                                                                                             // Cronómetro para la aparición de PowerUps

    [SerializeField] int goalsLimit = 10;                                                                                                                          // Límite de goles
    int[] playersScore = new int[2];

    public bool[] playerStateReady = new bool[2];                                                                                                                  // Espera a que el Jugador1 esté listo
    bool gameStarted;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        powerUps.Add(aceletationPwrUp);
        powerUps.Add(reduceOpPwrUp);
        powerUps.Add(incrSelfPwrUp);
        powerUps.Add(MultBallsPwrUp);
        powerUpsTimer = UnityEngine.Random.Range(5f, 20f);                                                                                                         // Tiempo aleatorio para la aparición de PowerUps
        chronometer = Time.time;
        SetGameState(GameState.Preparation);
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
                    pwrUp.SetActive(true);
                    return;
                }
            }
        }
    }

    /* Métodos */

    public void ChangePlayerState(int playerNum)
    {
        if (gameState == GameState.Preparation)
        {   
            if (!gameStarted)
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
                        SetGameState(GameState.InGame);
                        AudioManager.Instance.PlayClip(AudioManager.Instance.coundDownClip);
                        AudioManager.Instance.MakeTransition(AudioManager.Instance.backgroundMusic, AudioManager.Instance.coundDownClip.length);
                        balls[0].rb.linearVelocity = new Vector2(-4f, UnityEngine.Random.Range(-5f, 5f));
                    }
                }
            }
            else
            {
                GoalKick(playerNum);
            }
        }
    }

    public void SetGameState(GameState state)
    {
        switch (state)
        {
            case GameState.MainMenu:
                GameManager.Instance.DeactivatePlayerInputs(1);
                GameManager.Instance.DeactivatePlayerInputs(2);
                GameManager.Instance.ActivateUIInput();
                break;
            case GameState.Preparation:
                Time.timeScale = 0;
                balls[0].transform.position = Vector3.zero;
                GameManager.Instance.DeactivateUIInput();
                break;
            case GameState.InGame:
                Time.timeScale = 1;
                GameManager.Instance.DeactivateUIInput();
                break;
            case GameState.Pause:
                Time.timeScale = 0;
                GameManager.Instance.ActivateUIInput();
                break;
            case GameState.GameOver:
                Time.timeScale = 0;
                GameManager.Instance.ActivateUIInput();
                GameOver();
                break;
        }
    }

    public void UpdateScore(int playerScore)
    {
        playersScore[playerScore - 1]++;
        ScoreChanged?.Invoke(playersScore[0], playersScore[1]);
        if (playersScore[playerScore-1] >= goalsLimit)
        {
            SetGameState(GameState.GameOver);
        }
        else
        {
            foreach (BallMovement ball in balls)
            {
                if (ball.gameObject.activeSelf)
                {
                    return;
                }
                SetGameState(GameState.Preparation);
            }
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
                ball.rb.linearVelocity = moveDirection;
                ball.gameObject.SetActive(true);                                                                                         // Se instancia una bola
            }
        }
    }

    // Reinicio del nivel
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /* Corrutinas */
    void GoalKick(int playerNum)
    {
        if (playerNum == 1)                                                                                                                   // Si han marcado al Jugador1
        {
            balls[0].transform.position = new Vector2(-7, 0);
        }
        else                                                                                                              // Si han marcado al Jugador2
        {
            balls[0].transform.position = new Vector2(7, 0);                                                                                                                 // Se reinicia el estado del gol
        }
        balls[0].gameObject.SetActive(true);
    }

    void GameOver()
    {
        AudioManager.Instance.MakeTransition(AudioManager.Instance.victoryClip, 0);
        foreach (GameObject[] powerUp in powerUps)                                                                                                            // Se eliminan los Power Ups
        {
            foreach (GameObject pwrUp in powerUp)
            {
                pwrUp.SetActive(false);

            }
        }
        foreach (BallMovement ball in balls)                                                                                                                  // Se eliminan las bolas
        {
            ball.gameObject.SetActive(false);
        }
        GameFinished?.Invoke();
    }

    /* Eventos */
    public static event Action<int, int> ScoreChanged;
    public static event Action<int> PlayerReady;
    public static event Action GameFinished;
}