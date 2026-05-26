using System.Collections.Generic;
using UnityEngine;

//# Este script se encarga de lanzar SO Events Vector 2 #//
[CreateAssetMenu(menuName = "Events/Vector2 Game Event")]
public class Vector2GameEvent : ScriptableObject
{
    /* Vector 2 Game Event Listeners */
    readonly List<Vector2GameEventListener> listeners = new();   // Clases suscritas al evento

    /* Métodos */
    // Suscripción al evento
    public void Suscribe(Vector2GameEventListener listener) => listeners.Add(listener);

    // Desuscripción del evento
    public void Unuscribe(Vector2GameEventListener listener) => listeners.Remove(listener);

    // Lanzamiento del evento
    public void Raise(Vector2 value)
    {
        var copy = new List<Vector2GameEventListener>(listeners);// Copia para evitar conflictos durante la modificación de la lista
        foreach (Vector2GameEventListener listener in copy)
        {
            listener.OnEventRaised(value);                       // Lanzamiento del evento
        }
    }
}