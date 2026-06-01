using UnityEngine;

//# Este script se encarga del comportamiento del power up que acelera la bola #//
public class BallAcelerationPickUp : MonoBehaviour, IResettable
{
    private void OnEnable()
    {
        GameManager.Instance.RegisterResettable(this);
    }

    private void OnDisable()
    {
        GameManager.Instance.UnregisterResettable(this);
    }

    /* Cuando la bola colisione al Power Up */
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<BallAcelerationBehaviour>(out BallAcelerationBehaviour pwrUp))
        {
            pwrUp.enabled = true;// Averigua quien es el jugador que ha golpeado el PowerUp
        }
        gameObject.SetActive(false);                                  //Cuando se active el PowerUP se quita
    }

    /* Métodos para IResettable*/
    public void OnGameOver()
    {
        gameObject.SetActive(false);
    }
}