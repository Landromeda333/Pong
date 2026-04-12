using UnityEngine;

public class PlayerPrefsPong : MonoBehaviour
{
    private void Awake()
    {
        if (PlayerPrefs.GetInt("Initialized", 0) == 0)
        {
            PlayerPrefs.SetInt("Initialized", 1);
            PlayerPrefs.SetInt("FPS", 0);
            PlayerPrefs.SetInt("ScreenMode", 0);
        }
    }
}