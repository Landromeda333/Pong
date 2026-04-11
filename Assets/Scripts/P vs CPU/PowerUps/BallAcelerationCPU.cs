using UnityEngine;

public class BallAcelerationCPU : MonoBehaviour
{
    /* Cuando la bola colisione al Power Up */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        OtherActionsPvsCPU otherActionsPvsCPU = GameObject.Find("Main Camera").GetComponent<OtherActionsPvsCPU>(); // Encuentra el escript OtherActions
        BallMovementCPU ballMovementCPU = collision.gameObject.GetComponent<BallMovementCPU>();                    // Obtiene el script BallMovent de la bola
        otherActionsPvsCPU.activationForPlayerBallAceleration = ballMovementCPU.lastPlayerTouched;                 // Averigua quien es el jugador que ha golpeado el PowerUp
        otherActionsPvsCPU.ballToAcelerate = collision.gameObject;                                                 // Para saber a qué bola aplicarle el modificador de velocidad
        otherActionsPvsCPU.StartBallAceleration();                                                                 // Como al destruirse no terminará de ejecutar la Corrutina, se ejecuta en su lugar una función
        Destroy(gameObject);                                                                                       //Cuando se active el PowerUP se quita
    }
}