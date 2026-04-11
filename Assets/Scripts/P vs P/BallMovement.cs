using UnityEngine;

public class BallMovement : MonoBehaviour
{ 
    [SerializeField] AudioClip collisionSound;                                                            // Audio para cada vez que choque
    OtherActions otherActions;                                                                            // Encuentra el escript OtherActions
    public float speed = 6;                                                                               // Velocidad de la bola
    public Vector2 moveDirectionBall;                                                                     // Variable en la que se va a guardar el valor del movimiento de la bola recogido del script OtherActions
    public string lastPlayerTouched;                                                                      // Se guarda el último toque para aplicarlo a los Power Ups
    int goalKick;                                                                                         // Variable en la que se va a guardar el valor de quien saca recogido del script OtherActions
    
    void Start()
    {
        otherActions = GameObject.Find("Main Camera").GetComponent<OtherActions>();
        moveDirectionBall = otherActions.moveDirection;
        goalKick = otherActions.goalKick;

        if (!otherActions.kicked || otherActions.multipleBallsActivation == true)                         // Si no se le ha asignado ninguna dirección, como en el caso de las nuevas bolas del Power Up Multiple Balls
        {
            moveDirectionBall = otherActions.moveDirection;                                               // Se le asigna la dirección que se le ha dado en el script OtherActions
        }
        else
        {
            moveDirectionBall = new Vector2(-4f, Random.Range(-5f, 5f));                                  // Movimiento inicial de la bola
        }
    }

    /* Movimiento de la bola */
    void Update()
    {
        if (goalKick == 1 &&  Input.GetKey(KeyCode.W) && Input.GetKeyDown(KeyCode.D))                     // Si le toca sacar al Jugador1 y presiona la tecla de saque
        {
            lastPlayerTouched = "Player1";                                                                // Se guarda el último jugador que ha tocado la bola
            moveDirectionBall = new Vector2(4, Random.Range(1, 5));                                       // Se le da la dirección correspondiente al saque
            goalKick = 0;                                                                                 // Se pone a 0 para que no pueda sacar otra vez
            otherActions.kicked = true;                                                                   // Se ha hecho un saque de puerta
        }
        else if (goalKick == 1 && Input.GetKey(KeyCode.S) && Input.GetKeyDown(KeyCode.D))
        {
            lastPlayerTouched = "Player1";
            moveDirectionBall = new Vector2(4, Random.Range(-1, -5));
            goalKick = 0;
            otherActions.kicked = true;
        }
        else if (goalKick == 1 && Input.GetKeyDown(KeyCode.D))
        {
            lastPlayerTouched = "Player1";
            moveDirectionBall = new Vector2(4,0);
            goalKick = 0;
            otherActions.kicked = true;
        }

        if (goalKick == 2 && Input.GetKey(KeyCode.UpArrow) && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            lastPlayerTouched = "Player2";
            moveDirectionBall = new Vector2(-4, Random.Range(1, 5));
            goalKick = 0;
            otherActions.kicked = true;
        }
        else if (goalKick == 2 && Input.GetKey(KeyCode.DownArrow) && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            lastPlayerTouched = "Player2";
            moveDirectionBall = new Vector2(-4, Random.Range(-5, -1));
            goalKick = 0;
            otherActions.kicked = true;
        }
        else if (goalKick == 2 && Input.GetKey(KeyCode.LeftArrow))
        {
            lastPlayerTouched = "Player2";
            moveDirectionBall = new Vector2(-4, 0);
            goalKick = 0;
            otherActions.kicked = true;
        }
        transform.Translate(moveDirectionBall.normalized * speed * Time.deltaTime);                       // Se aplica el movimiento
    }

    // Función para los rebotes y sonidos de la pelota
    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Wall")                                                           // Si choca con una pared
        {
            moveDirectionBall.y *= -1;                                                                    // Al estar solo en el eje Y, simplemente hay que cambiar la dirección
        }
        if (collision.gameObject.tag == "Player")                                                         // Si choca contra un jugador
        {
            lastPlayerTouched = collision.gameObject.name;
            if (collision.gameObject.name == "Player1")
            {
                if (Input.GetKey(KeyCode.W))                                                              // Si el jugador se mueve hacia arriba, incrementa la inclinación para aplicar el efecto deseado
                {
                    moveDirectionBall.y += 1.33f;

                }
                else if (Input.GetKey(KeyCode.S))                                                         // Si el jugador se mueve hacia arriba, incrementa la inclinación para aplicar el efecto deseado
                {
                    moveDirectionBall.y -= 1.33f;

                }
            }
            if (collision.gameObject.name == "Player2")
            {
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    moveDirectionBall.y += 1.33f;

                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    moveDirectionBall.y -= 1.33f;

                }
            }
           moveDirectionBall.x *= -1;                                                                     // Al estar solo en el eje X, simplemente hay que cambiar la dirección
           if (speed < 30)
           {
                speed += 0.5f;                                                                            // Se aumenta la velocidad cada vez que interactúa con un jugador
           }
            GetComponent<AudioSource>().PlayOneShot(collisionSound);                                      // Emite un sonido al chocar
        }
    }

    // Funcion para identificar goles
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Finish")                                                             // Si detecta la portería
        {
            if (GameObject.FindGameObjectsWithTag("Ball").Length < 2)                                     // Teniendo en cuenta la situación de múltiples bolas
            {
                otherActions.kicked = false;                                                              // Si marca gol, tienen que sacar
            }
            Destroy(gameObject);                                                                          // Destruye la bola una vez hecho todo lo necesario
        }
    }
}