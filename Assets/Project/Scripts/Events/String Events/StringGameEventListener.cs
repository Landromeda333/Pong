using UnityEngine;
using UnityEngine.Events;

// Este script se encarga de escuchar los SO Events String #//
public class StringGameEventListener : MonoBehaviour
{
    /* String Game Event */
    [SerializeField] protected StringGameEvent evt; // Evento a escuchar
    [SerializeField] UnityEvent<string> response;   // Respuesta al evento

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
    public void OnEventRaised(string name) => response.Invoke(name);
}