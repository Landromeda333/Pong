using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class HUDController : MonoBehaviour
{
    public static HUDController Instance { get; private set; }
    Label _scoreLeft;
    Label _scoreRight;
    Label _countdownText;
    Label _gameState;
    Label[] _playerStates;
    VisualElement _pausePanel;
    Button _resumeButton;
    Button _settingsButton;
    Button _mainMenuButton;
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

    private void Start()
    {
        AudioManager.Instance.uiAudioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        _scoreLeft = root.Q<Label>("score-left");
        _scoreRight = root.Q<Label>("score-right");
        _playerStates = new[] { root.Q<Label>("player1-state"), root.Q<Label>("player2-state") };
        _countdownText = root.Q<Label>("countdown-text");
        _gameState = root.Q<Label>("gameState-text");
        _pausePanel = root.Q<VisualElement>("pause-panel");

        _resumeButton = root.Q<Button>("resume-button");
        _resumeButton.RegisterCallback<ClickEvent>(BackToGame);

        _settingsButton = root.Q<Button>("settings-button");
        _settingsButton.RegisterCallback<ClickEvent>(ViewSettings);

        _mainMenuButton = root.Q<Button>("mainMenu-button");
        _mainMenuButton.RegisterCallback<ClickEvent>(BackToMenu);

        LvlManager.StartToPlay += StartCountdown;
        LvlManager.ScoreChanged += UpdateScore;
        LvlManager.PlayerReady += UpdatePlayerState;
    }

    private void OnDisable()
    {
        _resumeButton?.UnregisterCallback<ClickEvent>(BackToGame);
        _settingsButton?.UnregisterCallback<ClickEvent>(ViewSettings);
        _mainMenuButton?.UnregisterCallback<ClickEvent>(BackToMenu);

        LvlManager.StartToPlay -= StartCountdown;
        LvlManager.ScoreChanged -= UpdateScore;
        LvlManager.PlayerReady -= UpdatePlayerState;
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

    public void StartCountdown()
    {
        _countdownText.visible = true;
    }

    public bool ShowPause()
    {
        if (_pausePanel.style.display == DisplayStyle.Flex)
        {
            _pausePanel.style.display = DisplayStyle.None;
            return false;
        }
        else
        {
            _pausePanel.style.display = DisplayStyle.Flex;
            return true;
        }
    }

    public void BackToGame(ClickEvent evt)                                                                                // Vuelve al juego y quita los controles
    {
        Debug.Log("Volver a la partida");
    }

    public void BackToMenu(ClickEvent evt)                                                                                // Vuelve al menú principal
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ViewSettings(ClickEvent evt)                                                                              // Muestra los controles
    {
        Debug.Log("Activar panel de controles");
    }

    
}