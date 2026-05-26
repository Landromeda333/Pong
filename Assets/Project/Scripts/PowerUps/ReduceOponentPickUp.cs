using UnityEngine;

//# Este script se encarga del power up recogible que reduce el tamaño del oponente #//
public class ReduceOponent : MonoBehaviour
{
    /* Int Game Event */
    [SerializeField] IntGameEvent reduceSize;// Avisa que jugador debe reducir su tamaño

    /* Cuando la bola colisione al Power Up */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        reduceSize.Raise(collision.gameObject.GetComponent<BallMovement>().lastPlayerTouched);  // Averigua quien es el jugador que ha golpeado el PowerUp
        gameObject.SetActive(false);                                                            //Cuando se active el PowerUP se quita
    }

    /* Métodos */
    // Reacción al SO Event OnGameOver
    public void OnGameOver()
    {
        gameObject.SetActive(false);
    }
}