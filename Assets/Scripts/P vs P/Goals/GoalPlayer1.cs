using UnityEngine;

public class GoalPlayer1 : MonoBehaviour
{
    public int pointsPlayer2;                       // Marcador del Jugador2
    public bool pointForPlayer2;                    // Idicador de si ha marcado el Jugador2
    public GoalPlayer2 goalPlayer2;                 // Se guarda el script para poder modificar la asignación de puntos de la otra portería

    /* Cuando la bola entre en la portería */
    private void OnTriggerEnter2D(Collider2D other)
    {
        GetComponent<AudioSource>().enabled = true; // Activa el audio
        pointsPlayer2++;                            // Cuando marque, se aumenta el marcador
        goalPlayer2.pointForPlayer1 = false;        // Se cambia la asignación de los puntos
        pointForPlayer2 = true;                     // Punto para el Jugador2
        GetComponent<AudioSource>().Play();         // Se reproduce una vez la explosión
    }
}