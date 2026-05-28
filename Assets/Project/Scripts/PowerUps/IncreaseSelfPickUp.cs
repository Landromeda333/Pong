using UnityEngine;

//# Este script se encarga del recogible power up que incrementa el tamaño #//
public class IncreaseSelfPickUp : MonoBehaviour, IResettable
{
    /* Int Game Event */
    [SerializeField] IntGameEvent increaseSelf; // Avisa qué jugador debe agrandarse

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
        increaseSelf.Raise(collision.gameObject.GetComponent<BallMovement>().lastPlayerTouched);
        gameObject.SetActive(false);
    }

    /* Método para IResettable */
    // Reacción al Game Over
    public void OnGameOver()
    {
        gameObject.SetActive(false);
    }
}