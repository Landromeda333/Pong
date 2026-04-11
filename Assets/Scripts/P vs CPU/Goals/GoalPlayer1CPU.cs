using UnityEngine;

public class GoalPlayer1CPU : MonoBehaviour
{
    public int pointsCPU;                           // Marcador de CPU
    public bool pointForCPU;                        // Idicador de si ha marcado CPU
    public GoalCPU goalCPU;                         // Se guarda el script para poder modificar la asignación de puntos de la otra portería

    /* Cuando la bola entre en la portería */
    private void OnTriggerEnter2D(Collider2D other)
    {
        GetComponent<AudioSource>().enabled = true; // Se activa el sonido
        pointsCPU++;                                // Cuando marque, se aumenta el marcador
        goalCPU.pointForPlayer1 = false;            // Se cambia la asignación de los puntos
        pointForCPU = true;                         // Punto para CPU
        GetComponent<AudioSource>().Play();         // Se reproduce una vez la explosión
    }
}