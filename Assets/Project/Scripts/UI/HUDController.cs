using UnityEngine;
using UnityEngine.UIElements;

public class HUDController : MonoBehaviour
{
    public static HUDController Instance;

    [SerializeField] GameEvent onPauseRequest;

    Label _scoreLeft, _scoreRight, _countdownText, _gameState;
    Label[] _playerStates;

    public float countDown = 3.4f;                                      // Cuenta atrás

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            return;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        _scoreLeft = root.Q<Label>("score-left");
        _scoreRight = root.Q<Label>("score-right");
        _playerStates = new[] { root.Q<Label>("player1-state"), root.Q<Label>("player2-state") };
        _countdownText = root.Q<Label>("countdown-text");
        _gameState = root.Q<Label>("gameState-text");

        LvlManager.ScoreChanged += UpdateScore;
    }

    private void OnDisable()
    {
        LvlManager.ScoreChanged -= UpdateScore;
    }

    private void Update()
    {
        if (_countdownText.visible)
        {
            if (countDown > 0)
            {
                countDown -= Time.deltaTime;
                _countdownText.text = countDown.ToString("0");
            }
            else
            {
                _countdownText.visible = false;
                foreach (Label playerState in _playerStates)
                {
                    playerState.visible = false;
                }
                LvlManager.Instance.StartGame();
            }
        }
    }

    /* Métodos */
    public void UpdateScore(int left, int right)
    {
        _scoreLeft.text = right.ToString();
        _scoreRight.text = left.ToString();
    }

    public void UpdateGameState(string stateName)
    {
        if (stateName == "")
        {
            _gameState.visible = false;
        }
        else
        {
            _gameState.text = stateName;
            _gameState.visible = true;
        }
    }

    /* Métodos para Int Game Event Listener */
    public void UpdatePlayerState(int playerNum)
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

    /* Métodos para Game Event Listener */

    public void StartCountdown()
    {
        _countdownText.visible = true;
    }
}