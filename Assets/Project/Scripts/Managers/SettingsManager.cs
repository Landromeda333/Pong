using UnityEngine;

//# Este script se encarga de hacer efectivos los cambios de los ajustes #//
public class SettingsManager : MonoBehaviour
{
    /* Singleton */
    public static SettingsManager Instance;

    // Estado actual de los ajustes en memoria
    public SettingsData data;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }
        DontDestroyOnLoad(gameObject);
        data = SaveSystem.LoadSettings();
    }

    // Seteo de las configuraciones generales
    private void Start()
    {
        ChangeFPSLimit(data.fps);
        ChangeScreenMode(data.screenMode);
        ChangeSFXVolume(data.sfxVolume);
        ChangeMusicVolume(data.musicVolume);
    }

    /* Métodos */
    // Guarda el nuevo valor del Slider y llama a la clase que gestiona el audio
    public void ChangeSFXVolume(float value)
    {
        data.sfxVolume = value;
        AudioManager.Instance.SetSFXVolume(value);
        SaveSystem.SaveSettings(data);
    }

    // Guarda el nuevo valor del Slider y llama a la clase que gestiona el audio
    public void ChangeMusicVolume(float value)
    {
        data.musicVolume = value;
        AudioManager.Instance.SetMusicVolume(value);
        SaveSystem.SaveSettings(data);
    }

    // Guarda el nuevo valor del Dropdown y llama a la clase que gestiona la calidad gráfica
    public void ChangeFPSLimit(int value)
    {
        data.fps = value;
        GraphicsQualityManager.Instance.SetFPSLimit(value);
        SaveSystem.SaveSettings(data);
    }

    // Guarda el nuevo valor del Dropdown y llama a la clase que gestiona la calidad gráfica
    public void ChangeScreenMode(string value)
    {
        data.screenMode = value;
        GraphicsQualityManager.Instance.SetScreenMode(value);
        SaveSystem.SaveSettings(data);
    }
}