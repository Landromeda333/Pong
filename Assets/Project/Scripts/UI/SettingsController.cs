using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

//# Este script se encarga de gestionar la UI de los ajustes #//
public class SettingsController : MonoBehaviour
{
    /* SO Events */
    [SerializeField] GameEvent onBackPressed;            // El botón back de la UI ha sido pulsado

    /* Visual Elements */
    VisualElement _settingsPanel;                        // Panel de ajustes

    /* DropdownFields */
    DropdownField _fpsLimit, _screenMode;                // Limitador de FPS, Modo de pantalla

    /* Sliders */
    Slider _musicVolume, _sfxVolume;                     // Volumen de la música, Volumen de los efectos especiales

    /* Buttons */
    Button _backButton;                                  // Botón de volver

    /* Dictionaries */
    Dictionary<string, int> _fpsOptions = new() { {"30", 30 }, { "60", 60 }, { "120", 120 }, { "Sin Límite", -1 }}; // Diccionario con las opciones para los límites de FPS

    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        _settingsPanel = root.Q<VisualElement>("settings-panel");

        _backButton = root.Q<Button>("back-button");
        _backButton.RegisterCallback<ClickEvent>(OnBackClicked);

        _fpsLimit = root.Q<DropdownField>("fps-limit");
        _fpsLimit.choices = new List<string> { "30", "60", "120", "Sin Límite" };
        _fpsLimit.value = PlayerPrefs.GetInt("FPS").ToString("0");
        _fpsLimit.RegisterValueChangedCallback(OnFPSChanged);

        _screenMode = root.Q<DropdownField>("screen-mode");
        _screenMode.choices = new List<string> { "Pantalla completa", "Ventana", "Sin bordes" };
        _screenMode.value = PlayerPrefs.GetString("ScreenMode", "Pantalla completa");
        _screenMode.RegisterValueChangedCallback(OnScreenModeChanged);

        _musicVolume = root.Q<Slider>("music-slider");
        _musicVolume.value = PlayerPrefs.GetFloat("MusicVolume", 10);
        _musicVolume.RegisterValueChangedCallback(OnMusicVolumeChanged);

        _sfxVolume = root.Q<Slider>("sfx-slider");
        _sfxVolume.value = PlayerPrefs.GetFloat("SFXVolume", 10);
        _sfxVolume.RegisterValueChangedCallback(OnSFXVolumeChanged);
    }

    private void OnDisable()
    {
        _backButton?.UnregisterCallback<ClickEvent>(OnBackClicked);

        _fpsLimit?.UnregisterValueChangedCallback(OnFPSChanged);
        _screenMode?.UnregisterValueChangedCallback(OnScreenModeChanged);

        _musicVolume?.UnregisterValueChangedCallback(OnMusicVolumeChanged);
        _sfxVolume?.UnregisterValueChangedCallback(OnSFXVolumeChanged);
    }

    /* Métodos */
    // Botón Volver pulsado
    void OnBackClicked(ClickEvent evt)
    {
        onBackPressed.Raise();                           // Avisa
        _settingsPanel.style.display = DisplayStyle.None;// Oculta el panel
    }

    // FPS Cambiados
    void OnFPSChanged(ChangeEvent<string> evt)
    {
        // Si la opción está dentro de lo establecido
        if (_fpsOptions.TryGetValue(evt.newValue, out int fps))
        {
            SettingsManager.Instance.ChangeFPSLimit(fps);
        }
        else
        {
            SettingsManager.Instance.ChangeFPSLimit(60); // Valor por defecto
        }
    }

    // Modo de pantalla cambiado
    void OnScreenModeChanged(ChangeEvent<string> evt)
    {
        SettingsManager.Instance.ChangeScreenMode(evt.newValue);
    }

    // Volumen de la música cambiado
    void OnMusicVolumeChanged(ChangeEvent<float> evt)
    {
        SettingsManager.Instance.ChangeMusicVolume(evt.newValue);
    }

    // Volumen de los efectos especiales cambiado
    void OnSFXVolumeChanged(ChangeEvent<float> evt)
    {
        SettingsManager.Instance.ChangeSFXVolume(evt.newValue);
    }

    /* Método para SO Event OnBackPressed */
    // Ocultar el botón de ajustes
    public void Back()
    {
        _settingsPanel.style.display = DisplayStyle.None;
    }

    /* Método para SO Event OnSettingsRequest */
    // Mostrar el panel de ajustes
    public void Show()
    {
        _settingsPanel.style.display = DisplayStyle.Flex;
    }
}