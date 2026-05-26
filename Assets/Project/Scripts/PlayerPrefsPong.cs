using UnityEngine;

/* Este script se encarga de gestionar los PlayerPrefs */
public class PlayerPrefsPong : MonoBehaviour
{
    private void Awake()
    {
        if (PlayerPrefs.GetInt("Initialized", 0) == 0)
        {
            PlayerPrefs.SetInt("Initialized", 1);

            /* Graphics Quality */
            PlayerPrefs.SetInt("FPS", 0);
            PlayerPrefs.SetString("ScreenMode", "Pantalla completa");

            /* Audio */
            PlayerPrefs.SetFloat("MusicVolume", 80);
            PlayerPrefs.SetFloat("SFXVolume", 80);
        }
    }
}