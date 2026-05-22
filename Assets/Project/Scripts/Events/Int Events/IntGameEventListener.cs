using UnityEngine;
using UnityEngine.Events;

public class IntGameEventListener : MonoBehaviour
{
    [SerializeField] protected IntGameEvent evt;
    [SerializeField] UnityEvent<int> response;

    private void OnEnable()
    {
        evt.Suscribe(this);
    }

    private void OnDisable()
    {
        evt.Unuscribe(this);
    }

    public void OnEventRaised(int value) => response.Invoke(value);
}