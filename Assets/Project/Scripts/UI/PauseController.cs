using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PauseController : MonoBehaviour
{
    [SerializeField] GameEvent onSettingsRequest;

    Button _resumeButton, _settingsButton, _mainMenuButton;

    VisualElement _pausePanel;

    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        _pausePanel = root.Q<VisualElement>("pause-panel");

        _resumeButton = root.Q<Button>("resume-button");
        _resumeButton.RegisterCallback<ClickEvent>(Continue);

        _settingsButton = root.Q<Button>("settings-button");
        _settingsButton.RegisterCallback<ClickEvent>(OnSettingsClicked);

        _mainMenuButton = root.Q<Button>("mainMenu-button");
        _mainMenuButton.RegisterCallback<ClickEvent>(BackToMenu);
    }

    private void OnDisable()
    {
        _resumeButton?.UnregisterCallback<ClickEvent>(Continue);
        _settingsButton?.UnregisterCallback<ClickEvent>(OnSettingsClicked);
        _mainMenuButton?.UnregisterCallback<ClickEvent>(BackToMenu);
    }

    /* Métodos */

    public void ShowOrHidePauseMenu()
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

    public void Continue(ClickEvent evt)
    {
       ShowOrHidePauseMenu();
    }

    void OnSettingsClicked(ClickEvent evt)
    {
        onSettingsRequest.Raise();
        _pausePanel.style.display = DisplayStyle.None;
    }

    public void BackToMenu(ClickEvent evt)                                              // Vuelve al menú principal
    {
        SceneManager.LoadScene("MainMenu");
    }

}