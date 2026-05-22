using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/IntGameEvent")]
public class IntGameEvent : ScriptableObject
{
    readonly List<IntGameEventListener> listeners = new();

    public void Suscribe(IntGameEventListener listener) => listeners.Add(listener);
    public void Unuscribe(IntGameEventListener listener) => listeners.Remove(listener);

    public void Raise(int value)
    {
        var copy = new List<IntGameEventListener>(listeners);
        foreach (IntGameEventListener listener in copy)
        {
            listener.OnEventRaised(value);
        }
    }
}