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
        SetFPSLimit(SettingsManager.Instance.data.fps);
        SetScreenMode(SettingsManager.Instance.data.screenMode);
    }

    /* Métodos */
    // Aplicación del límite de FPS
    public void SetFPSLimit(int value)
    {
        Application.targetFrameRate = value;
    }

    // Aplicación del nuevo modo de la pantalla
    public void SetScreenMode(string value)
    {
        switch (value)
        {
            case "Pantalla completa": Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen; break;
            case "Sin bordes": Screen.fullScreenMode = FullScreenMode.FullScreenWindow; break;
            case "Ventana": Screen.fullScreenMode = FullScreenMode.Windowed; break;
        }
    }
}