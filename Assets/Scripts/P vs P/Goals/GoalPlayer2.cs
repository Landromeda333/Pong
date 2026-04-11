using UnityEngine;

public class GoalPlayer2 : MonoBehaviour
{
    public int pointsPlayer1;                       // Marcador del Jugador1
    public bool pointForPlayer1;                    // Idicador de si ha marcado el Jugador1
    public GoalPlayer1 goalPlayer1;                 // Se guarda el script para poder modificar la asignación de puntos de la otra portería

    /* Cuando la bola entre en la portería */
    private void OnTriggerEnter2D(Collider2D other)
    {
        GetComponent<AudioSource>().enabled = true; // Activa el audio
        pointsPlayer1++;                            // Cuando marque, se aumenta el marcador
        goalPlayer1.pointForPlayer2 = false;        // Se cambia la asignación de los puntos
        pointForPlayer1 = true;                     // Punto para el Jugador1
        GetComponent<AudioSource>().Play();         // Se reproduce una vez la explosión
    }
}