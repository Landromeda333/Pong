using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum GameState
    {
        MainMenu,
        Preparation,
        InGame,
        Pause,
        GameOver
    }
    public GameState gameState;
    [HideInInspector] public GameState previousGameState;

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
        SetGameState(GameState.MainMenu);
    }

    /* Métodos */

    // Estado de partida
    public void SetGameState(GameState state)
    {
        previousGameState = gameState;
        switch (state)
        {
            case GameState.MainMenu:
                InputsGameManager.Instance.UIOnly();
                break;

            case GameState.Preparation:
                Time.timeScale = 0;
                foreach (PlayerMovement player in InputsGameManager.Instance.playersMovement)
                {
                    player.transform.position = new Vector2(player.transform.position.x, 0);
                }
                HUDController.Instance.UpdateGameState("Saque");
                InputsGameManager.Instance.InGameOnly();
                break;

            case GameState.InGame:
                Time.timeScale = 1;
                HUDController.Instance.UpdateGameState("");
                InputsGameManager.Instance.InGameOnly();
                break;

            case GameState.Pause:
                Time.timeScale = 0;
                InputsGameManager.Instance.UIOnly();
                break;

            case GameState.GameOver:
                Time.timeScale = 0;
                HUDController.Instance.UpdateGameState("Final");
                InputsGameManager.Instance.UIOnly();
                LvlManager.Instance.GameOver();
                break;
        }
        gameState = state;
    }
}