using UnityEngine;

//# Este script se encarga de gestionar la calidad gráfica #//
public class GraphicsQualityManager : MonoBehaviour
{
    /* Singleton */
    public static GraphicsQualityManager Instance;

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

    private void Start()
    {
        SetFPSLimit(PlayerPrefs.GetInt("FPS"));
        SetScreenMode(PlayerPrefs.GetString("ScreenMode"));
    }

    /* Métodos */
    // Aplicación del límite de FPS
    public void SetFPSLimit(int value)
    {
        if (value != PlayerPrefs.GetInt("FPS"))
        {
            PlayerPrefs.SetInt("FPS", value);
        }
        Application.targetFrameRate = value;
    }

    // Aplicación del nuevo modo de la pantalla
    public void SetScreenMode(string value)
    {
        if (value != PlayerPrefs.GetString("ScreenMode"))
        {
            PlayerPrefs.SetString("ScreenMode", value);
        }

        switch (value)
        {
            case "Pantalla completa": Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen; break;
            case "Sin bordes": Screen.fullScreenMode = FullScreenMode.FullScreenWindow; break;
            case "Ventana": Screen.fullScreenMode = FullScreenMode.Windowed; break;
        }
    }
}