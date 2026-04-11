using UnityEngine;

public class MultipleBalls : MonoBehaviour
{
    /* Cuando la bola colisione al Power Up */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        OtherActions otherActions = GameObject.Find("Main Camera").GetComponent<OtherActions>(); // Encuentra el escript OtherActions
        BallMovement ballMovement = collision.gameObject.GetComponent<BallMovement>();           // Obtiene el script BallMovent de la bola
        otherActions.multipleBallTransform = collision.gameObject.transform.position;            // Se guarda la posición de la colisión entre la bola y el Power Up para que las bolas se instancien en ese lugar
        otherActions.StartMultipleBalls();                                                       // Como al destruirse no terminará de ejecutar la Corrutina, se ejecuta en su lugar una función

        Destroy(gameObject);                                                                     //Cuando se active el PowerUP se quita
    }
}