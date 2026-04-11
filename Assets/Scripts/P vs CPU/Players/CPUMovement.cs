using UnityEngine;

public class CPUMovement : MonoBehaviour
{
    float speed = 1;                                                                                    // Velocidad de movimiento del jugador
    [SerializeField] string collisioning;                                                               // Comprobador de colisión
    Vector2 translatePositionY;                                                                         // Se coge una variable aparte para moverlo verticalmente
    BallMovementCPU ballMovementCPU;                                                                    // Sript de la bola

    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Ball"))                                                   // Se busca una bola
        {
            ballMovementCPU = GameObject.FindGameObjectWithTag("Ball").GetComponent<BallMovementCPU>(); // Elige una bola a la que seguir
            if (collisioning == "Up")
            {
                if (ballMovementCPU.moveDirectionBall.y < transform.localPosition.y)
                {
                    translatePositionY = new Vector2(0, ballMovementCPU.moveDirectionBall.y);           // Se coge la dirección de la bola para que vaya hacia donde va la bola
                }
            }
            else if (collisioning == "Down")
            {
                if (ballMovementCPU.moveDirectionBall.y > transform.localPosition.y)
                {
                    translatePositionY = new Vector2(0, ballMovementCPU.moveDirectionBall.y);           // Se coge la dirección de la bola para que vaya hacia donde va la bola
                }
            }
            else
            {
                translatePositionY = new Vector2(0, ballMovementCPU.moveDirectionBall.y);               // Se coge la dirección de la bola para que vaya hacia donde va la bola
            }
            transform.Translate(translatePositionY * speed * Time.deltaTime);                           // Se muve la barra
        }
        else                                                                                            // Si no hay una bola
        {
            transform.position = new Vector2(8, 0);                                                     // Se centra el jugador
        }
    }

    // Detección de colisiones con las paredes
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.name == "DownWall" || collision.gameObject.name == "UpWall") && GetComponent<AudioSource>().enabled == false)
        {
            GetComponent<AudioSource>().enabled = true;
        }
        if (collision.gameObject.name == "UpWall")                                                      // Detección de la pared de arriba
        {
            collisioning = "Up";                                                                        // Se identifica la pared
            GetComponent<AudioSource>().Play();                                                         // Se reproduce el sonido
        }
        else if (collision.gameObject.name == "DownWall")                                               // Detección de la pared de abajo
        {
            collisioning = "Down";                                                                      // Se identifica la pared
            GetComponent<AudioSource>().Play();                                                         // Se reproduce el sonido
        }
    }

    // Cancelación de la detección de colisiones con las paredes
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "UpWall" || collision.gameObject.name == "DownWall")           // Detección de la pared de arriba
        {
            collisioning = null;                                                                        // Se anula la detección
        }
    }
}