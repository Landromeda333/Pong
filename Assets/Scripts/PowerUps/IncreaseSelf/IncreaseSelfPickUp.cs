using UnityEngine;
public class IncreaseSelf : MonoBehaviour
{
    int playerNum;
    private void OnDisable()
    {
        playerNum = 0;
    }

    /* Cuando la bola colisione al Power Up */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerNum = collision.gameObject.GetComponent<BallMovement>().lastPlayerTouched;
        if (playerNum > 0)
        {
            GameManager.Instance.playersMovement[playerNum-1].GetComponent<IncreaseSelfBehaviour>().enabled = true;
        }
        gameObject.SetActive(false);
    }
}