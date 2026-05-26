using UnityEngine;

/* Este script se encarga del comportamiento de las porterías*/
public class Goal : MonoBehaviour
{
    /* SO Events */
    [SerializeField] Int_IntGameEvent playerScored; // Avisa que el jugador contrario ha marcado

    /* AudioSource */
    AudioSource source;

    [SerializeField] int playerGoal;                // Guarda de qué jugador es la portería
    int score;                                      // Puntuación del jugador

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    /* Cuando la bola entra en la portería */
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            other.gameObject.SetActive(false);      // Desactiva la bola
            UpdateScore();                          // Actualiza los puntos
            source.Play();
        }
    }

    // Actualización de puntuación
    public void UpdateScore()
    {
        score++;
        playerScored.Raise(playerGoal, score);
    }
}