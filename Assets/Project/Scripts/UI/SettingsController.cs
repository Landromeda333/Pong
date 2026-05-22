using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SettingsController : MonoBehaviour
{
    public static SettingsController Instance;

    [SerializeField] GameEvent onPauseRequested;

    DropdownField _fpsLimit, _screenMode;

    Slider _musicVolume, _sfxVolume;

    Button _backButton;

    UIDocument doc;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        doc = GetComponent<UIDocument>();
        gameObject.SetActive(false);
        doc.enabled = true;
    }

    private void OnEnable()
    {
        var root = doc.rootVisualElement;

        _backButton = root.Q<Button>("back-button");
        _backButton.RegisterCallback<ClickEvent>(ShowOrHide);

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
        _backButton?.UnregisterCallback<ClickEvent>(ShowOrHide);

        _fpsLimit?.UnregisterValueChangedCallback(OnFPSChanged);
        _screenMode?.UnregisterValueChangedCallback(OnScreenModeChanged);

        _musicVolume?.UnregisterValueChangedCallback(OnMusicVolumeChanged);
        _sfxVolume?.UnregisterValueChangedCallback(OnSFXVolumeChanged);

        onPauseRequested.Raise();
    }

    /* Métodos */
    void Back()
    {
        gameObject.SetActive(false);
    }

    void Show()
    {
        gameObject.SetActive(true);
    }

    public void ShowOrHide(ClickEvent evt)
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    void OnFPSChanged(ChangeEvent<string> evt)
    {
        switch (evt.newValue)
        {
            case "30": SettingsManager.Instance.ChangeFPSLimit(30); break;

            case "60": SettingsManager.Instance.ChangeFPSLimit(60); break;

            case "120": SettingsManager.Instance.ChangeFPSLimit(120); break;

            case "Sin límite": SettingsManager.Instance.ChangeFPSLimit(-1); break;

            default: SettingsManager.Instance.ChangeFPSLimit(60); break;
        }
    }

    void OnScreenModeChanged(ChangeEvent<string> evt)
    {
        SettingsManager.Instance.ChangeScreenMode(evt.newValue);
    }

    void OnMusicVolumeChanged(ChangeEvent<float> evt)
    {
        SettingsManager.Instance.ChangeMusicVolume(evt.newValue);
    }

    void OnSFXVolumeChanged(ChangeEvent<float> evt)
    {
        SettingsManager.Instance.ChangeSFXVolume(evt.newValue);
    }
}