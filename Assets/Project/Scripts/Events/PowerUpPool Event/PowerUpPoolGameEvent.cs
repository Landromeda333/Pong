using System.Collections.Generic;
using UnityEngine;

//# Este script se encarga de lanzar SO Events PowerUpPool #//
[CreateAssetMenu(menuName = "Events/PowerUp Pool GameEvent")]
public class PowerUpPoolGameEvent : ScriptableObject
{
    /* Power Up Pool Game Event Listener*/
    readonly List<PowerUpPoolGameEventListener> listeners = new();   // Clases suscritas al evento

    /* Métodos */
    // Suscripción al evento
    public void Suscribe(PowerUpPoolGameEventListener listener) => listeners.Add(listener);

    // Desuscripción del evento
    public void Unuscribe(PowerUpPoolGameEventListener listener) => listeners.Remove(listener);

    // Lanzamiento del evento
    public void Raise(PowerUpPool pool)
    {
        var copy = new List<PowerUpPoolGameEventListener>(listeners);// Copia para evitar conflictos durante la modificación de la lista
        foreach (PowerUpPoolGameEventListener listener in copy)
        {
            listener.OnEventRaised(pool);                            // Lanzamiento del evento
        }
    }
}