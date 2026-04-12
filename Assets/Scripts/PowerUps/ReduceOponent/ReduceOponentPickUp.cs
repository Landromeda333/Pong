using UnityEngine;

public class ReduceOponent : MonoBehaviour
{
    /* Cuando la bola colisione al Power Up */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        LvlManager.Instance.players[collision.gameObject.GetComponent<BallMovement>().lastPlayerTouched-1].GetComponent<ReduceOponentBehaviour>().enabled = true; // Averigua quien es el jugador que ha golpeado el PowerUp
        gameObject.SetActive(false);                                                                    //Cuando se active el PowerUP se quita
    }
}