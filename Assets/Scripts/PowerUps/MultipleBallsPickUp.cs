using UnityEngine;

public class MultipleBalls : MonoBehaviour
{
    /* Cuando la bola colisione al Power Up */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        LvlManager.Instance.StartMultipleBalls(collision.gameObject.transform.position);// Como al destruirse no terminará de ejecutar la Corrutina, se ejecuta en su lugar una función
        gameObject.SetActive(false);                                                    //Cuando se active el PowerUP se quita
    }
}