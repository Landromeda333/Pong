using UnityEngine;

public class MultipleBallsCPU : MonoBehaviour
{
    /* Cuando la bola colisione al Power Up */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        OtherActionsPvsCPU otherActionsPvsCPU = GameObject.Find("Main Camera").GetComponent<OtherActionsPvsCPU>(); // Encuentra el escript OtherActions
        BallMovementCPU ballMovementCPU = collision.gameObject.GetComponent<BallMovementCPU>();                    // Obtiene el script BallMovent de la bola
        otherActionsPvsCPU.multipleBallTransform = collision.gameObject.transform.position;                        // Se guarda la posición de la colisión entre la bola y el Power Up para que las bolas se instancien en ese lugar
        otherActionsPvsCPU.StartMultipleBalls();                                                                   // Como al destruirse no terminará de ejecutar la Corrutina, se ejecuta en su lugar una función

        Destroy(gameObject);                                                                                       //Cuando se active el PowerUP se quita
    }
}