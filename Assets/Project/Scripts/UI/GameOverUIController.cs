using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

//# Este script se encarga de gestionar la UI del Game Over #//
public class GameOverUIController : MonoBehaviour
{
    /* SO Event */
    [SerializeField] StringGameEvent loadSceneRequest;  // Solicitud de carga de nivel

    /* Visual Elements */
    VisualElement _gameOverPanel;                       // Panel de Game Over

    /* Labels */
    Label _gameOver;                                    // Título del Game Over

    /* Buttons */
    Button _restart, _exit;                             // Botones de la UI

    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        _gameOverPanel = root.Q<VisualElement>("gameOver-container");

        _gameOver = root.Q<Label>("gameOver-text");

        _restart = root.Q<Button>("restart-button");
        _restart.RegisterCallback<ClickEvent>(Restart);

        _exit = root.Q<Button>("mainMenu-button");
        _exit.RegisterCallback<ClickEvent>(Exit);
    }

    private void OnDisable()
    {
        _restart.UnregisterCallback<ClickEvent>(Restart);
        _exit.UnregisterCallback<ClickEvent>(Exit);
    }

    /* Métodos */
    // Empieza de nuevo el nivel
    void Restart(ClickEvent evt)
    {
        loadSceneRequest.Raise(SceneManager.GetActiveScene().name);
    }

    // Sale al menú principal
    void Exit(ClickEvent evt)
    {
        loadSceneRequest.Raise("MainMenu");
    }

    // Actualiza el texto con la información correspondiente
    void UpdateText(int num)
    {
        if (num == 2)
        {
            _gameOver.text = "Victoria del Jugador 1";
        }
        else
        {
            _gameOver.text = "Victoria del Jugador 2";
        }
    }

    /* Métodos para SO Event OnGameOver */
    // Reacción al final de partida
    public void GameOver(int num)
    {
        UpdateText(num);
        _gameOverPanel.style.display = DisplayStyle.Flex;
    }
}