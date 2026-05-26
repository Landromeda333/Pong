using UnityEngine;
using UnityEngine.Events;

// Este script se encarga de escuchar los SO Events Int - Int  #//
public class Int_IntGameEventListener : MonoBehaviour
{
    /* Int_Int Game Event */
    [SerializeField] protected Int_IntGameEvent evt;// Evento a escuchar

    /* Unity Event */
    [SerializeField] UnityEvent<int,int> response;  // Respuesta al evento

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
    public void OnEventRaised(int value1, int value2) => response.Invoke(value1, value2);
}