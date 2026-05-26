using UnityEngine;
using UnityEngine.Events;

// Este script se encarga de escuchar los SO Events Int #//
public class IntGameEventListener : MonoBehaviour
{
    /* Int Game Event */
    [SerializeField] protected IntGameEvent evt;// Evento a escuchar

    /* Unity Event */
    [SerializeField] UnityEvent<int> response;  // Respuesta al evento

    private void OnEnable()
    {
        evt.Suscribe(this);
    }

    private void OnDisable()
    {
        evt.Unuscribe(this);
    }

    /* Métodos */
    // Respuesta al evento
    public void OnEventRaised(int value) => response.Invoke(value);
}