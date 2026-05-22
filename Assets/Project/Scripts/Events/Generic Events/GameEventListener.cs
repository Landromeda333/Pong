using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    [SerializeField] protected GameEvent evt;
    [SerializeField] UnityEvent response;

    private void OnEnable()
    {
        evt.Suscribe(this);
    }

    private void OnDisable()
    {
        evt.Unuscribe(this);
    }

    public void OnEventRaised() => response.Invoke();
}