using UnityEngine;

//# Este script se encarga del power up recogible que reduce el tamaño del oponente #//
public class ReduceOponent : MonoBehaviour, IResettable
{
    /* Int Game Event */
    [SerializeField] IntGameEvent reduceSize;                                                   // Avisa que jugador debe reducir su tamaño

    private void OnEnable()
    {
        GameManager.Instance.RegisterResettable(this);
    }

    private void OnDisable()
    {
        GameManager.Instance.UnregisterResettable(this);
    }

    /* Cuando la bola colisione al Power Up */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<BallMovement>(out BallMovement ball))
        {
            reduceSize.Raise(ball.lastPlayerTouched);  // Averigua quien es el jugador que ha golpeado el PowerUp
        }
        gameObject.SetActive(false);                                                            //Cuando se active el PowerUP se quita
    }

    /* Método para IResettable */
    // Reacción al Game Over
    public void OnGameOver()
    {
        gameObject.SetActive(false);
    }
}