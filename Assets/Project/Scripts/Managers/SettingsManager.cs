using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    /* Singleton */
    public static SettingsManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Seteo de las configuraciones generales
    private void Start()
    {
        // Gráficos
        ChangeFPSLimit(PlayerPrefs.GetInt("FPS", 60));
        ChangeScreenMode(PlayerPrefs.GetString("ScreenMode", "Pantalla completa"));

        // Sonido
        ChangeSFXVolume(PlayerPrefs.GetFloat("SFXVolume", 10));
        ChangeMusicVolume(PlayerPrefs.GetFloat("MusicVolume", 10));
    }

    /* Métodos */
    // Guarda el nuevo valor del Slider y llama a la clase que gestiona el audio
    public void ChangeSFXVolume(float value)
    {
        if (PlayerPrefs.GetFloat("SFXVolume") != value)
        {
            PlayerPrefs.SetFloat("SFXVolume", value);
        }
        AudioManager.Instance.SetSFXVolume(value);
    }

    // Guarda el nuevo valor del Slider y llama a la clase que gestiona el audio
    public void ChangeMusicVolume(float value)
    {
        if (PlayerPrefs.GetFloat("MusicVolume") != value)
        {
            PlayerPrefs.SetFloat("MusicVolume", value);
        }
        AudioManager.Instance.SetMusicVolume(value);
    }

    // Guarda el nuevo valor del Dropdown y llama a la clase que gestiona la calidad gráfica
    public void ChangeFPSLimit(int value)
    {
        if (PlayerPrefs.GetInt("FPS") != value)
        {
            PlayerPrefs.SetInt("FPS", value);
        }
        GraphicsQualityManager.Instance.SetFPSLimit(value);
    }

    // Guarda el nuevo valor del Dropdown y llama a la clase que gestiona la calidad gráfica
    public void ChangeScreenMode(string value)
    {
        if (PlayerPrefs.GetString("ScreenMode") != value)
        {
            PlayerPrefs.SetString("ScreenMode", value);
        }
        GraphicsQualityManager.Instance.SetScreenMode(value);
    }
}