using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class HUDController : MonoBehaviour
{
    public static HUDController Instance;

    [SerializeField] GameEvent onSettingsRequest;

    Label _scoreLeft, _scoreRight, _countdownText, _gameState;
    Label[] _playerStates;

    [HideInInspector] public VisualElement _pausePanel;

    Button _resumeButton, _settingsButton, _mainMenuButton;

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
        _pausePanel = root.Q<VisualElement>("pause-panel");

        _resumeButton = root.Q<Button>("resume-button");
        _resumeButton.RegisterCallback<ClickEvent>(Continue);

        _settingsButton = root.Q<Button>("settings-button");
        _settingsButton.RegisterCallback<ClickEvent>(OnSettingsClicked);

        _mainMenuButton = root.Q<Button>("mainMenu-button");
        _mainMenuButton.RegisterCallback<ClickEvent>(BackToMenu);

        LvlManager.ScoreChanged += UpdateScore;
        LvlManager.PlayerReady += UpdatePlayerState;

        if (LvlManager.Instance)
        {
            for (int i = 0; i < LvlManager.Instance.playerStateReady.Length; i++)
            {
                if (LvlManager.Instance.playerStateReady[i])
                {
                    UpdatePlayerState(i + 1);
                }
            }
        }
    }

    private void OnDisable()
    {
        _resumeButton?.UnregisterCallback<ClickEvent>(Continue);
        _settingsButton?.UnregisterCallback<ClickEvent>(OnSettingsClicked);
        _mainMenuButton?.UnregisterCallback<ClickEvent>(BackToMenu);

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
    public void OnPauseRequested()
    {
        gameObject.SetActive(true);
    }

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

    public void Continue(ClickEvent evt)
    {
        ShowOrHidePauseMenu();
    }

    void OnSettingsClicked(ClickEvent evt)
    {
        onSettingsRequest.Raise();
        gameObject.SetActive(false);
    }

    public void ShowOrHidePauseMenu()
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
            if (_pausePanel.style.display == DisplayStyle.Flex)
            {
                _pausePanel.style.display = DisplayStyle.None;
            }
            else
            {
                _pausePanel.style.display = DisplayStyle.Flex;
            }
        }
        else
        {
            if (_pausePanel.style.display == DisplayStyle.Flex)
            {
                _pausePanel.style.display = DisplayStyle.None;
                GameManager.Instance.SetGameState(GameManager.Instance.previousGameState);
            }
            else
            {
                _pausePanel.style.display = DisplayStyle.Flex;
                GameManager.Instance.SetGameState(GameManager.GameState.Pause);
            }
        }
    }

    public void BackToMenu(ClickEvent evt)                                              // Vuelve al menú principal
    {
        SceneManager.LoadScene("MainMenu");
    }
}