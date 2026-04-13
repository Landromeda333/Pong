using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class HUDController : MonoBehaviour
{
    public static HUDController Instance { get; private set; }
    Label _scoreLeft;
    Label _scoreRight;
    Label _countdownText;
    Label[] _playerStates;

    public float countDown = 3.4f;                                                                          // Cuenta atrás

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        _scoreLeft = root.Q<Label>("score-left");
        _scoreRight = root.Q<Label>("score-right");
        _playerStates = new[] { root.Q<Label>("player1-state"), root.Q<Label>("player2-state") };
        _countdownText = root.Q<Label>("countdown-text");
        LvlManager.ScoreChanged += UpdateScore;
        LvlManager.PlayerReady += UpdatePlayerState;
    }

    private void OnDisable()
    {
        LvlManager.ScoreChanged -= UpdateScore;
        LvlManager.PlayerReady -= UpdatePlayerState;
    }

    private void Update()
    {
        if (countDown > 0)
        {
            countDown -= Time.deltaTime;
            Debug.Log(countDown);
        }
    }


    /* Métodos */

    public void UpdateScore(int left, int right)
    {
        _scoreLeft.text = left.ToString();
        _scoreRight.text = right.ToString();
    }

    void UpdatePlayerState(int playerNum)
    {
        if (playerNum == 1)
        {
            _playerStates[0].text = "¡Preparado!";
        }
        else
        {
            _playerStates[1].text = "¡Preparado!";
        }
    }

    public void BackToMenu()                                                                                // Vuelve al menú principal
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ViewControls(ClickEvent evt)                                                                              // Muestra los controles
    {
        Debug.Log("Activar panel de controles");
    }

    public void BackToGame(ClickEvent evt)                                                                                // Vuelve al juego y quita los controles
    {
        Debug.Log("Volver a la partida");
    }
}