using UnityEngine;
public class IncreaseSelf : MonoBehaviour
{
    /* Cuando la bola colisione al Power Up */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        LvlManager.Instance.players[collision.gameObject.GetComponent<BallMovement>().lastPlayerTouched-1].GetComponent<IncreaseSelfBehaviour>().enabled = true; 
        gameObject.SetActive(false);
    }
}