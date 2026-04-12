using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuController : MonoBehaviour
{
    UIDocument _document;
    Button _playButton, _settingsButton, _quitButton;

    private void OnEnable()
    {
        _document = GetComponent<UIDocument>();
        var root = _document.rootVisualElement;

        _playButton = root.Q<Button>("play-button");
        _playButton.RegisterCallback<ClickEvent>(OnPlayClicked);

        _settingsButton = root.Q<Button>("settings-button");
        _settingsButton.RegisterCallback<ClickEvent>(OnSettingsClicked);

        _quitButton = root.Q<Button>("quit-button");
        _quitButton.RegisterCallback<ClickEvent>(OnQuitClicked);
    }

    private void OnDisable()
    {
        _playButton?.UnregisterCallback<ClickEvent>(OnPlayClicked);
        _settingsButton?.UnregisterCallback<ClickEvent>(OnSettingsClicked);
        _quitButton?.UnregisterCallback<ClickEvent>(OnQuitClicked);
    }

    /* Métodos */
    void OnPlayClicked(ClickEvent evt)
    {
        SceneManager.LoadScene("J1vsJ2");
    }

    void OnSettingsClicked(ClickEvent evt)
    {
        Debug.Log("Abrir ajustes");
    }

    void OnQuitClicked(ClickEvent evt)
    {
        Application.Quit();
    }
}