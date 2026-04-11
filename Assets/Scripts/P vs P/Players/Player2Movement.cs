using UnityEngine;

public class Player2Movement : MonoBehaviour
{
    float speed = 6f;                                                                              // Velocidad de movimiento del jugador
    string collisioning;                                                                           // Comprobador de colisión
    [SerializeField] OtherActions otherActions;                                                    // Script OtherActionsPvsCPU 

    /* Movimiento del Jugador2 */
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow) && collisioning != "Up" && otherActions.kicked)          // Movimiento hacia arriba hasta que choca con la pared de arriba
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);                              // Se aplica el movimiento
        }
        else if (Input.GetKey(KeyCode.DownArrow) && collisioning != "Down" && otherActions.kicked) // Movimiento hacia abajo hasta que choque con la pared de abajo
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);                            // Se aplica el movimiento
        }

        if (!GameObject.Find("Ball") && !GameObject.Find("Ball(Clone)"))                           // Cuando se marque un gol y desaparezca la bola
        {
            transform.position = new Vector2(8, 0);                                                // Reinicia la posición
        }
    }

    // Detección de colisiones con las paredes
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.name == "DownWall" || collision.gameObject.name == "UpWall") && GetComponent<AudioSource>().enabled == false)
        {
            GetComponent<AudioSource>().enabled = true;
        }
        if (collision.gameObject.name == "UpWall")                                                 // Detección de la pared de arriba
        {
            collisioning = "Up";                                                                   // Se identifica la pared
            GetComponent<AudioSource>().Play();                                                    // Se reproduce el sonido
        }
        else if (collision.gameObject.name == "DownWall")                                          // Detección de la pared de abajo
        {
            collisioning = "Down";                                                                 // Se identifica la pared
            GetComponent<AudioSource>().Play();                                                    // Se reproduce el sonido
        }
    }

    // Cancelación de la detección de colisiones con las paredes
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "UpWall" || collision.gameObject.name == "DownWall")      // Detección de la pared de arriba
        {
            collisioning = null;                                                                   // Se anula la detección
        }
    }
}