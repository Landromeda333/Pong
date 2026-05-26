using UnityEngine;
using UnityEngine.UIElements;

//# Este script se encarga de gestionar la UI del menú principal #//
public class MainMenuUIController : MonoBehaviour
{
    /* SO Events */
    [SerializeField] GameEvent onSettingsRequest;                   // Solicitud para el menú de ajustes
    [SerializeField] StringGameEvent loadingSceenRequest;           // Solicitud carga de nivel

    /* Labels */
    Label _versionText;

    /* Buttons */
    Button _jVSjButton, _jVSCPUButton, _settingsButton, _quitButton;// Botones del menú principal

    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        _versionText = root.Q<Label>("version-text");

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
        _jVSCPUButton?.UnregisterCallback<ClickEvent>(OnJvsCPUClicked);
        _settingsButton?.UnregisterCallback<ClickEvent>(OnSettingsClicked);
        _quitButton?.UnregisterCallback<ClickEvent>(OnQuitClicked);
    }


    private void Start()
    {
        GameManager.Instance?.SetGameState(GameManager.GameState.MainMenu);
        AudioManager.Instance?.ChangeMusic(AudioManager.Instance?.mainMenuClip);
        _versionText.text = "Ver. " + Application.version;
    }
 
    /* Métodos para la UI*/
    // Carga el nivel de J1 vs J2
    void OnJvsJClicked(ClickEvent evt)
    {
        GameManager.Instance.playersAmount = 2;
        loadingSceenRequest.Raise("J1vsJ2");
    }

    // Carga el nivel de J1 vs CPU
    void OnJvsCPUClicked(ClickEvent evt)
    {
        GameManager.Instance.playersAmount = 1;
        loadingSceenRequest.Raise("JvsCPU");
    }

    // Abre el menú de ajustes
    void OnSettingsClicked(ClickEvent evt)
    {
        onSettingsRequest.Raise();
    }

    // Sale del programa
    void OnQuitClicked(ClickEvent evt)
    {
        Application.Quit();
    }
}