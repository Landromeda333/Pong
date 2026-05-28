using System.Collections.Generic;
using UnityEngine;

/* Este script se encarga de gestionar el estado del juego */
public class GameManager : MonoBehaviour
{
    /* SO Events */
    [SerializeField] GameEvent uiOnly, inGameOnly, onPreparation;// Pide cambio de controles, Avisa de que hay que prepararse
    [SerializeField] StringGameEvent updateGameStateText;        // Solicitud de cambio en el texto que informa del estado del jugador

    /* Interfaces */
    readonly List<IResettable> resettables = new();

    /* Singleton */
    public static GameManager Instance;

    /* Enum */
    public enum GameState
    {
        MainMenu,
        Preparation,
        InGame,
        Pause,
        GameOver
    }
    public GameState gameState;                                  // Estado actual del juego
    [HideInInspector] public GameState previousGameState;        // Estado anterior del juego

    public int playersAmount;                                    // Cantidad de jugadores

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    /* Métodos */
    public void RegisterResettable(IResettable resettable) => resettables.Add(resettable);

    public void UnregisterResettable(IResettable resettable) => resettables.Remove(resettable);

    // Estado de partida
    public void SetGameState(GameState state)
    {
        // Si no es el mismo estado que el actual lo cambia. Controla el bucle por usar previousGameState cambiando hacia el mismo estado
        if (state != gameState)
        {
            previousGameState = gameState;
        }

        switch (state)
        {
            case GameState.MainMenu:
                Time.timeScale = 1f;
                uiOnly.Raise();
                playersAmount = 0;
                break;

            case GameState.Preparation:
                Time.timeScale = 0;
                onPreparation.Raise();
                updateGameStateText.Raise("Saque");
                inGameOnly.Raise();
                break;

            case GameState.InGame:
                Time.timeScale = 1;
                updateGameStateText.Raise("");
                inGameOnly.Raise();
                break;

            case GameState.Pause:
                Time.timeScale = 0;
                uiOnly.Raise();
                break;

            case GameState.GameOver:
                Time.timeScale = 0;
                updateGameStateText.Raise("Final");
                uiOnly.Raise();
                break;
        }
        gameState = state;
    }

    /* Método para SO Event OnGameOver */
    // Reaciona al fin de partida
    public void GameOver()
    {
        var copy = new List<IResettable>(resettables);
        foreach (IResettable resettable in copy)
        {
            resettable.OnGameOver();
        }
        SetGameState(GameState.GameOver);
    }

    /* Método para SO Event OnPauseRequest */
    // Reacciona a la pausa
    public void OnPauseRequest()
    {
        SetGameState(GameState.Pause);
    }

    /* Método para SO Event ContinueGame */
    // Reacciona a la reanudación de la partida
    public void ContinueGame()
    {
        SetGameState(previousGameState);
    }
}