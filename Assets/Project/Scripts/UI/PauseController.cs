using UnityEngine;
using UnityEngine.UIElements;

//# Este script se encarga de gestionar la UI del menú de pausa #//
public class PauseUIController : MonoBehaviour
{
    /* SO Events */
    [SerializeField] GameEvent onSettingsRequest, continueGame; // Solicitud para el menú de ajustes, Continuación de la partida
    [SerializeField] StringGameEvent loadingSceneRequest;              // Solicitud para la carga de un nivel

    /* Visual Elements */
    VisualElement _pausePanel;                                  // Panel de pausa

    /* Buttons */
    Button _resumeButton, _settingsButton, _mainMenuButton;

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

    /* Métodos para la UI */
    // Sale del menú de pausa
    public void Continue(ClickEvent evt)
    {
       ShowOrHidePauseMenu();
    }

    // Abre el menú de ajustes
    void OnSettingsClicked(ClickEvent evt)
    {
        onSettingsRequest.Raise();
        _pausePanel.style.display = DisplayStyle.None;
    }

    // Vuelve al menú principal
    public void BackToMenu(ClickEvent evt)                                              // Vuelve al menú principal
    {
        loadingSceneRequest.Raise("MainMenu");
    }

    /* Método para SO OnPauseRequest y OnBackPressed */
    //Muestra u oculta el menú de pausa
    public void ShowOrHidePauseMenu()
    {
        if (_pausePanel.style.display == DisplayStyle.Flex)
        {
            _pausePanel.style.display = DisplayStyle.None;
            continueGame.Raise();
        }
        else
        {
            _pausePanel.style.display = DisplayStyle.Flex;
        }
    }
}