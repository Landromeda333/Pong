using UnityEngine;

public class Player1MovementCPU : MonoBehaviour
{
    float speed = 6f;                                                                            // Velocidad de movimiento del Jugador1
    string collisioning;                                                                         // Comprobador de colisión
    [SerializeField] OtherActionsPvsCPU otherActionsPvsCPU;                                      // Script OtherActionsPvsCPU

    /* Movimiento del Jugador1 */
    void Update()
    {
        if (Input.GetKey(KeyCode.W) && collisioning != "Up" && otherActionsPvsCPU.kicked)        // Movimiento hacia arriba hasta que colisione con la pared de arriba
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);                            // Sube
        }
        else if (Input.GetKey(KeyCode.S) && collisioning != "Down" && otherActionsPvsCPU.kicked) // Movimiento hacia abajo hasta que colisione con la pared de abajo
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);                          // Baja
        }

        if (!GameObject.Find("BallPvsCPU") && !GameObject.Find("BallPvsCPU(Clone)"))             // Cuando se marque un gol y desaparezca la bola se reinicia la posición
        {
            transform.position = new Vector2(-8, 0);                                             // Se reinicia la posición del Jugador1 al centro
        }
    }

    // Detección de colisiones con las paredes
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.name == "DownWall" || collision.gameObject.name == "UpWall") && GetComponent<AudioSource>().enabled == false)
        {
            GetComponent<AudioSource>().enabled = true;                                          // Activa el sonido
        }
        if (collision.gameObject.name == "UpWall")                                               // Detección de la pared de arriba
        {
            collisioning = "Up";                                                                 // Se identifica la pared
            GetComponent<AudioSource>().Play();                                                  // Se reproduce el sonido
        }
        else if (collision.gameObject.name == "DownWall")                                        // Detección de la pared de abajo
        {
            collisioning = "Down";                                                               // Se identifica la pared
            GetComponent<AudioSource>().Play();                                                  // Se reproduce el sonido
        }
    }

    // Cancelación de la detección de colisiones con las paredes
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "UpWall" || collision.gameObject.name == "DownWall")    // Detección de la pared de arriba
        {
            collisioning = null;                                                                 // Se anula la detección
        }
    }
}