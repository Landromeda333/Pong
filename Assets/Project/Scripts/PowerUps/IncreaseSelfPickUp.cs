using UnityEngine;

//# Este script se encarga del recogible power up que incrementa el tamaño #//
public class IncreaseSelfPickUp : MonoBehaviour
{
    /* Int Game Event */
    [SerializeField] IntGameEvent increaseSelf; // Avisa qué jugador debe agrandarse

    /* Cuando la bola colisione al Power Up */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        increaseSelf.Raise(collision.gameObject.GetComponent<BallMovement>().lastPlayerTouched);
        gameObject.SetActive(false);
    }

    /* Métodos */
    // Reacción al SO Event OnGameOver
    public void OnGameOver()
    {
        gameObject.SetActive(false);
    }
}