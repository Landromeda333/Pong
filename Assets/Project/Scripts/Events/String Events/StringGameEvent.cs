using System.Collections.Generic;
using UnityEngine;

//# Este script se encarga de lanzar SO Events genéricos #//
[CreateAssetMenu(menuName = "Events/String Game Event")]
public class StringGameEvent : ScriptableObject
{
    /* String Game Event Listener */
    readonly List<StringGameEventListener> listeners = new();   // Clases suscritas al evento

    /* Métodos */
    // Suscripción al evento
    public void Suscribe(StringGameEventListener listener) => listeners.Add(listener);

    // Desuscripción del evento
    public void Unuscribe(StringGameEventListener listener) => listeners.Remove(listener);

    // Lanzamiento del evento
    public void Raise(string name)
    {
        var copy = new List<StringGameEventListener>(listeners);// Copia para evitar conflictos durante la modificación de la lista
        foreach (StringGameEventListener listener in copy)
        {
            listener.OnEventRaised(name);                       // Lanzamiento del evento
        }
    }
}