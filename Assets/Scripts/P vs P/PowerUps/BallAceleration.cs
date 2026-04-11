using UnityEngine;

public class BallAceleration : MonoBehaviour
{
    /* Cuando la bola colisione al Power Up */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        OtherActions otherActions = GameObject.Find("Main Camera").GetComponent<OtherActions>(); // Encuentra el escript OtherActions
        BallMovement ballMovement = collision.gameObject.GetComponent<BallMovement>();           // Obtiene el script BallMovent de la bola
        otherActions.activationForPlayerBallAceleration = ballMovement.lastPlayerTouched;        // Averigua quien es el jugador que ha golpeado el PowerUp
        otherActions.ballToAcelerate = collision.gameObject;                                     // Para saber a qué bola aplicarle el modificador de velocidad
        otherActions.StartBallAceleration();                                                     // Como al destruirse no terminará de ejecutar la Corrutina, se ejecuta en su lugar una función
        Destroy(gameObject);                                                                     //Cuando se active el PowerUP se quita
    }
}