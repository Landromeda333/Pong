using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Events/GameEvent")]
public class GameEvent : ScriptableObject
{
    readonly List<GameEventListener> listeners = new();

    public void Suscribe(GameEventListener listener) => listeners.Add(listener);
    public void Unuscribe(GameEventListener listener) => listeners.Remove(listener);

    public void Raise()
    {
        var copy = new List<GameEventListener>(listeners);
        foreach (GameEventListener listener in copy)
        {
            listener.OnEventRaised();
        }
    }
}