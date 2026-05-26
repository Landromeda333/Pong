using System.Collections.Generic;
using UnityEngine;

//# Este script se encarga de lanzar SO Events Int - Int #//
[CreateAssetMenu(menuName = "Events/Int-Int GameEvent")]
public class Int_IntGameEvent : ScriptableObject
{
    /* Int_Int Game Event Listener */
    readonly List<Int_IntGameEventListener> listeners = new();   // Clases suscritas al evento

    /* Métodos */
    // Suscripción al evento
    public void Suscribe(Int_IntGameEventListener listener) => listeners.Add(listener);

    // Desuscripción del evento
    public void Unuscribe(Int_IntGameEventListener listener) => listeners.Remove(listener);

    // Lanzamiento del evento
    public void Raise(int value1, int value2)
    {
        var copy = new List<Int_IntGameEventListener>(listeners);// Copia para evitar conflictos durante la modificación de la lista
        foreach (Int_IntGameEventListener listener in copy)
        {
            listener.OnEventRaised(value1, value2);              // Lanzamiento del evento
        }
    }
}