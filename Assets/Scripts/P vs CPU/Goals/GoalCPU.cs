using UnityEngine;

public class GoalCPU : MonoBehaviour
{
    public int pointsPlayer1;                   // Marcador del Jugador1
    public bool pointForPlayer1;                    // Idicador de si ha marcado el Jugador1
    [SerializeField] GoalPlayer1CPU goalPlayer1CPU; // Se guarda el script para poder modificar la asignación de puntos de la otra portería

    /* Cuando la bola entre en la portería */
    private void OnTriggerEnter2D(Collider2D other)
    {
        GetComponent<AudioSource>().enabled = true; // Activar el sonido
        pointsPlayer1++;                            // Cuando marque, se aumenta el marcador
        goalPlayer1CPU.pointForCPU = false;         // Se cambia la asignación de los puntos
        pointForPlayer1 = true;                     // Punto para el Jugador1
        GetComponent<AudioSource>().Play();         // Se reproduce una vez la explosión
    }
}