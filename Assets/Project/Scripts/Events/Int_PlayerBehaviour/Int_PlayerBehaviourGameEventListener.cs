using UnityEngine;
using UnityEngine.Events;

// Este script se encarga de escuchar los SO Events Int - PlayerBehaviour #//
public class Int_PlayerBehaviourGameEventListener : MonoBehaviour
{
    /* Int_PlayerBehaviour Game Event */
    [SerializeField] protected Int_PlayerBehaviourGameEvent evt;// Evento que escuchar

    /* Unity Event */
    [SerializeField] UnityEvent<int,PlayerBehaviour> response;  // Respuesta al evento

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
    public void OnEventRaised(int value, PlayerBehaviour player) => response.Invoke(value, player);
}