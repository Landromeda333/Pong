using UnityEngine;

//# Este script se encarga del comportamiento del power up que acelera la bola #//
public class BallAcelerationPickUp : MonoBehaviour
{
    /* Cuando la bola colisione al Power Up */
    private void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<BallAcelerationBehaviour>().enabled = true;// Averigua quien es el jugador que ha golpeado el PowerUp
        gameObject.SetActive(false);                                  //Cuando se active el PowerUP se quita
    }

    /* Métodos */
    // Reacción al SO Event OnGameOver
    public void OnGameOver()
    {
        gameObject.SetActive(false);
    }
}