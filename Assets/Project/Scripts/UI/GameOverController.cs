using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameOverController : MonoBehaviour
{
    VisualElement _gameOverPanel;

    Label _gameOver;

    Button _restart, _exit;

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

    void Restart(ClickEvent evt)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Exit(ClickEvent evt)
    {
        SceneManager.LoadScene("MainMenu");
    }

    /* Métodos para Game Event Listener */
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

    public void GameOver(int num)
    {
        UpdateText(num);
        _gameOverPanel.style.display = DisplayStyle.Flex;
    }
}