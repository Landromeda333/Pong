using System.Collections;
using UnityEngine;

public class BallMovementCPU : MonoBehaviour
{
    [SerializeField] AudioClip collisionSound;                                                  // Audio para cada vez que choque
    OtherActionsPvsCPU otherActionsPvsCPU;                                                      // Script OtherActions
    public float speed = 6;                                                                     // Velocidad de la bola
    public Vector2 moveDirectionBall;                                                           // Variable en la que se va a guardar el valor del movimiento de la bola recogido del script otherActionsPvsCPU
    public string lastPlayerTouched;                                                            // Se guarda el último toque para aplicarlo a los Power Ups
    int goalKick;                                                                               // Variable en la que se va a guardar el valor de quien saca recogido del script otherActionsPvsCPU

    void Start()
    {
        otherActionsPvsCPU = GameObject.Find("Main Camera").GetComponent<OtherActionsPvsCPU>();
        moveDirectionBall = otherActionsPvsCPU.moveDirection;
        goalKick = otherActionsPvsCPU.goalKick;                                                 // Se recoge para saber quién debe sacar

        if (!otherActionsPvsCPU.kicked || otherActionsPvsCPU.multipleBallsActivation == true)   // Si no se le ha asignado ninguna dirección, como en el caso de las nuevas bolas del Power Up Multiple Balls
        {
            moveDirectionBall = otherActionsPvsCPU.moveDirection;                               // Coge la dirección de la bola del script OtherActionsPvsCPU
        }
        else
        {
            moveDirectionBall = new Vector2(-4f, Random.Range(-5f, 5f));                        // Movimiento inicial de la bola
        }

        if (goalKick == 2)                                                                      // Si le toca sacar a CPU
        {
            StartCoroutine(GoalKickAI());                                                       // Ejecuta la corrutina de saque de puerta de CPU
        }
    }

    /* Movimiento de la bola */
    void Update()
    {
        if (goalKick == 1 && Input.GetKey(KeyCode.W) && Input.GetKeyDown(KeyCode.D))            // Si le toca sacar al Jugador1 y presiona la tecla de saque
        {
            lastPlayerTouched = "Player1";                                                      // Se guarda el nombre por si ubiese un Power Up en escena
            moveDirectionBall = new Vector2(4, Random.Range(1, 5));                             // Se le da la dirección correspondiente al saque
            goalKick = 0;                                                                       // Se pone a 0 para que no pueda sacar otra vez
            otherActionsPvsCPU.kicked = true;                                                   // Se ha hecho un saque de puerta
        }
        else if (goalKick == 1 && Input.GetKey(KeyCode.S) && Input.GetKeyDown(KeyCode.D))
        {
            lastPlayerTouched = "Player1";
            moveDirectionBall = new Vector2(4, Random.Range(-1, -5));
            goalKick = 0;
            otherActionsPvsCPU.kicked = true;
        }
        else if (goalKick == 1 && Input.GetKeyDown(KeyCode.D))
        {
            lastPlayerTouched = "Player1";
            moveDirectionBall = new Vector2(4, 0);
            goalKick = 0;
            otherActionsPvsCPU.kicked = true;
        }

        transform.Translate(moveDirectionBall.normalized * speed * Time.deltaTime);             // Se aplica el movimiento
    }

    /* Función para los rebotes y sonidos de la pelota */
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")                                                 // Si choca con una pared
        {
            moveDirectionBall.y *= -1;                                                          // Al estar solo en el eje Y, simplemente hay que cambiar la dirección
        }
        if (collision.gameObject.tag == "Player")                                               // Si choca contra un jugador
        {
            lastPlayerTouched = collision.gameObject.name;                                      // Se guarda su nombre para usarlo en los Power Ups
            if (collision.gameObject.name == "Player1") {
                if (Input.GetKey(KeyCode.W))
                {
                    moveDirectionBall.y += 1.33f;                                               // Se le da una dirección al chocar con el jugador para darle el efecto deseado

                }
                else if (Input.GetKey(KeyCode.S))
                {
                    moveDirectionBall.y -= 1.33f;                                               // Se le da una dirección al chocar con el jugador para darle el efecto deseado

                }
            }
            if (collision.gameObject.name == "CPU")
            {
                moveDirectionBall.y += Random.Range(-1.33f, 1.33f);                             // Se le da una dirección al chocar
            }
            moveDirectionBall.x *= -1;                                                          // Al estar solo en el eje X, simplemente hay que cambiar la dirección
            if (speed < 30)                                                                     // Mientras no llegue al límite de velocidad
            {
                speed += 0.5f;                                                                  // Se aumenta la velocidad cada vez que interactúa con un jugador
            }
        }
        GetComponent<AudioSource>().PlayOneShot(collisionSound);                                // Emite un sonido al chocar
    }

    /* Funcion para identificar goles */
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Finish")                                                   // Si detecta la portería
        {
            if (GameObject.FindGameObjectsWithTag("Ball").Length < 2)                           // Teniendo en cuenta la situación de múltiples bolas
            {
                otherActionsPvsCPU.kicked = false;                                              // Si marca gol, tienen que sacar
            }

            Destroy(gameObject);                                                                // Destruye la bola una vez hecho todo lo necesario
        }
    }

    /* Saque de puerta de CPU*/
    public IEnumerator GoalKickAI()
    {
        lastPlayerTouched = "CPU";                                                              // Se guarda el nombre por si ubiese un Power Up en escena
        yield return new WaitForSeconds(3);                                                     // Se espera 5 seg para que parezca que está pensando
        moveDirectionBall = new Vector2(-4, Random.Range(-5f, 5f));                             // Se le da la dirección correspondiente al saque
        goalKick = 0;                                                                           // Se pone a 0 para que no pueda sacar otra vez
        otherActionsPvsCPU.kicked = true;                                                       // Se ha sacado
    } 
}