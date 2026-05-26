using UnityEngine;
using UnityEngine.Events;

// Este script se encarga de escuchar los SO Events Vector2 #//
public class Vector2GameEventListener : MonoBehaviour
{
    /* Vector 2 Game Event */
    [SerializeField] protected Vector2GameEvent evt;// Evento a escuchar

    /* Unity Event */
    [SerializeField] UnityEvent<Vector2> response;  // Respuesta al evento

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
    public void OnEventRaised(Vector2 value) => response.Invoke(value);
}