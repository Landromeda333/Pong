using UnityEngine;
using UnityEngine.Events;

// Este script se encarga de escuchar los SO Events PowerUpPool #//
public class PowerUpPoolGameEventListener : MonoBehaviour
{
    /* Power Up Pool Game Event */
    [SerializeField] protected PowerUpPoolGameEvent evt;// Evento que escuchar

    /* Unity Event */
    [SerializeField] UnityEvent<PowerUpPool> response;  // Respuesta al evento

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
    public void OnEventRaised(PowerUpPool pool) => response.Invoke(pool);
}