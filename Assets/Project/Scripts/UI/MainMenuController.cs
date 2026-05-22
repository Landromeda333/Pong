using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] GameEvent onSettingsRequest;

    UIDocument _document;

    Button _jVSjButton, _jVSCPUButton, _settingsButton, _quitButton;

    private void OnEnable()
    {
        _document = GetComponent<UIDocument>();
        var root = _document.rootVisualElement;

        _jVSjButton = root.Q<Button>("jVSj-button");
        _jVSjButton.RegisterCallback<ClickEvent>(OnJvsJClicked);

        _jVSCPUButton = root.Q<Button>("jVSCPU-button");
        _jVSCPUButton.RegisterCallback<ClickEvent>(OnJvsCPUClicked);

        _settingsButton = root.Q<Button>("settings-button");
        _settingsButton.RegisterCallback<ClickEvent>(OnSettingsClicked);

        _quitButton = root.Q<Button>("quit-button");
        _quitButton.RegisterCallback<ClickEvent>(OnQuitClicked);
    }

    private void OnDisable()
    {
        _jVSjButton?.UnregisterCallback<ClickEvent>(OnJvsJClicked);
        _jVSCPUButton.UnregisterCallback<ClickEvent>(OnJvsCPUClicked);
        _settingsButton?.UnregisterCallback<ClickEvent>(OnSettingsClicked);
        _quitButton?.UnregisterCallback<ClickEvent>(OnQuitClicked);

    }

    /* Métodos */
    void OnJvsJClicked(ClickEvent evt)
    {
        SceneManager.LoadScene("J1vsJ2");
    }

    void OnJvsCPUClicked(ClickEvent evt)
    {
        SceneManager.LoadScene("JvsCPU");
    }

    void OnSettingsClicked(ClickEvent evt)
    {
        onSettingsRequest.Raise();
    }

    void OnQuitClicked(ClickEvent evt)
    {
        Application.Quit();
    }
}