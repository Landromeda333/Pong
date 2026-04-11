using UnityEngine;
public class IncreaseSelfCPU : MonoBehaviour
{
    /* Cuando la bola colisione al Power Up */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        OtherActionsPvsCPU otherActionsPvsCPU = GameObject.Find("Main Camera").GetComponent<OtherActionsPvsCPU>(); // Encuentra el escript OtherActions
        BallMovementCPU ballMovementCPU = collision.gameObject.GetComponent<BallMovementCPU>();                    // Obtiene el script BallMovent de la bola
        otherActionsPvsCPU.activationForPlayerIncreaseSelf = ballMovementCPU.lastPlayerTouched;                    // Averigua quien es el jugador que ha golpeado el PowerUp
        otherActionsPvsCPU.StartIncreaseSelf();                                                                    // Como al destruirse no terminará de ejecutar la Corrutina, se ejecuta en su lugar una función 

        Destroy(gameObject);                                                                                       //Cuando se active el PowerUP se quita
    }
}