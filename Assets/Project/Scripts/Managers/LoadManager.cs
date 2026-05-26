using UnityEngine;
using UnityEngine.SceneManagement;

/* Este script se encarga de gestionar la carga de niveles */
public class LoadManager : MonoBehaviour
{
    /* Métodos */
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}