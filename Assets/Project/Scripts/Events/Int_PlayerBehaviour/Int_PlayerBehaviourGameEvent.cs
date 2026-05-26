using System.Collections.Generic;
using UnityEngine;

//# Este script se encarga de lanzar SO Events Int - PlayerBehaviour #//
[CreateAssetMenu(menuName = "Events/Int-PlayerBehaviour GameEvent")]
public class Int_PlayerBehaviourGameEvent : ScriptableObject
{
    /* Int - PlayerBehaviour Game Event Listener */ 
    readonly List<Int_PlayerBehaviourGameEventListener> listeners = new();   // Clases suscritas al evento

    /* Métodos */
    // Suscripción al evento
    public void Suscribe(Int_PlayerBehaviourGameEventListener listener) => listeners.Add(listener);

    // Desuscripción del evento
    public void Unuscribe(Int_PlayerBehaviourGameEventListener listener) => listeners.Remove(listener);

    // Lanzamiento del evento
    public void Raise(int value, PlayerBehaviour player)
    {
        var copy = new List<Int_PlayerBehaviourGameEventListener>(listeners);// Copia para evitar conflictos durante la modificación de la lista
        foreach (Int_PlayerBehaviourGameEventListener listener in copy)
        {
            listener.OnEventRaised(value, player);                           // Lanzamiento del evento
        }
    }
}