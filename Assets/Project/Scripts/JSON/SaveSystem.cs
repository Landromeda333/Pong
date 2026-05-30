using System.IO;
using UnityEngine;

public static class SaveSystem
{
    // Ruta donde se guarda el archivo
    static string path = Path.Combine(Application.persistentDataPath, "settings.json");

    // Guarda los ajustes en un archivo JSON
    public static void SaveSettings(SettingsData data)
    {
        File.WriteAllText(path, JsonUtility.ToJson(data, true));
    }

    // Carga los ajustes desde el archivo JSON
    public static SettingsData LoadSettings()
    {
        if (File.Exists(path))
        {
            return JsonUtility.FromJson<SettingsData>(File.ReadAllText(path));
        }

        // Si no existe el archivo devuelve valores por defecto
        return new SettingsData
        {
            fps = 60,
            screenMode = "Pantalla completa",
            musicVolume = 80,
            sfxVolume = 80
        };
    }
}