using UnityEngine;

public class GraphicsQualityManager : MonoBehaviour
{
    GraphicsQualityManager Instance;
    int[] fps = {120, 60, 30};

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
        SetScreenMode(PlayerPrefs.GetInt("ScreenMode"));
    }

    /* Métodos */
    public void SetFPSLimit(int value)
    {
        if (value != PlayerPrefs.GetInt("FPS"))
        {
            PlayerPrefs.SetInt("FPS", value);
        }
        Application.targetFrameRate = fps[value];
    }

    // Aplicación del nuevo modo de la pantalla
    public void SetScreenMode(int value)
    {
        if (value != PlayerPrefs.GetInt("ScreenMode"))
        {
            PlayerPrefs.SetInt("ScreenMode", value);
        }
        switch (value)
        {
            case 0: Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen; break;
            case 1: Screen.fullScreenMode = FullScreenMode.FullScreenWindow; break;
            case 2: Screen.fullScreenMode = FullScreenMode.Windowed; break;
        }
    }
}