using UnityEngine;

public class Goal : MonoBehaviour
{
    AudioSource audSource;

    [SerializeField] int playerGoal;

    private void Awake()
    {
        audSource = GetComponent<AudioSource>();
    }

    /* Cuando la bola entre en la portería */
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            other.gameObject.SetActive(false);
            LvlManager.Instance.UpdateScore(playerGoal);
            audSource.Play();                               // Reproducción explosión
        }
    }
}