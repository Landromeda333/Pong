using System.Collections;
using UnityEngine;

public class OtherActions : MonoBehaviour
{
    [SerializeField] GameObject ball;                                                                                                                              // La bola que se va a instanciar una vez que se destruya
    public GoalPlayer1 goalPlayer1;                                                                                                                                // Acceso al script de la portería del Jugador1
    public GoalPlayer2 goalPlayer2;                                                                                                                                // Acceso al script de la portería del Jugador2
    public Vector2 moveDirection;                                                                                                                                  // Dirección del movimiento
    public int goalKick = 0;                                                                                                                                       // Indica quien saca de puerta
    public bool isGameActive;                                                                                                                                      // Estado de la partida
    BallMovement ballMovement;                                                                                                                                     // Script de la bola
    public bool kicked = true;                                                                                                                                     // Estado del saque de portería
    public bool isWaitingPlayer1 = true;                                                                                                                           // Espera a que el Jugador1 esté listo
    public bool isWaitingPlayer2 = true;                                                                                                                           // Espera a que el Jugador2 esté listo
    public int goalsLimit = 10;                                                                                                                                    // Límite de goles
    [SerializeField] GameObject player1;                                                                                                                           // GameObject del Jugador1
    [SerializeField] GameObject player2;                                                                                                                           // GameObject del Jugador2

    //Variables Audio
    [SerializeField] AudioController audioController;                                                                                                              // Script del controlador de audio

    //Variables UI
    [SerializeField] UiCanvasController uiCanvasController;                                                                                                        // Script del controlador de la interfaz

    //Variables PowerUps
    float chronometer;                                                                                                                                             // Cronómetro para la aparición de PowerUps
    float powerUpsTimer;                                                                                                                                           // Tiempo aleatorio de aparición de los PowerUps
    [SerializeField] GameObject[] powerUps;                                                                                                                        // Lista de PowerUps
    public float timeLimit = 20f;                                                                                                                                  // Duración del cronómetro en segundos
    GameObject[] powerUpsCounter;                                                                                                                                  // Array para guardar los Power Ups encontrados y poder contarlos

    //Variables BallAceleration
    public string activationForPlayerBallAceleration;                                                                                                              // Qué jugador ha activado el PowerUp
    float speedSaved;                                                                                                                                              // Guarda la velocidad a la que iva la bola para restaurarla cuando se acabe el Power Up
    float actualTime;                                                                                                                                              // Cronómetro para el Power Up de cambio de velocidad de la bola
    public GameObject ballToAcelerate;                                                                                                                             // Bola a la que se le aplicará la aceleración

    //Variables IncreaseSelf
    public string activationForPlayerIncreaseSelf;                                                                                                                 // A qué jugador se le aplica este Power Up
    float timeLimitIncrease;                                                                                                                                       // Tiempo límite de duración del Power Up

    //Variables ReduceOponent
    public string activationForPlayerReduceOponent;                                                                                                                // A qué jugador se le aplica este Power Up
    float timeLimitReduce;                                                                                                                                         // Tiempo límite de duración del Power Up

    //Variables MultipleBalls
    [SerializeField] int maxBalls = 3;                                                                                                                             // Cuantas bolas se instanciarán
    public Vector3 multipleBallTransform;                                                                                                                          // Lugar donde instanciar las bolas
    public bool multipleBallsActivation;                                                                                                                           // Estado de activación del Power Up

    private void Awake()
    {
        powerUpsTimer = Random.Range(5f,20f);                                                                                                                      // Tiempo aleatorio para la aparición de PowerUps
    }
    void Update()
    {
        if (!isGameActive && (isWaitingPlayer1 || isWaitingPlayer2))
        {
            if (Input.GetKeyDown(KeyCode.A))                                                                                                                       // Cuando pulse la tecla, pasa a estar preparado
            {
                isWaitingPlayer1 = false;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))                                                                                                              // Cuando pulse la tecla, pasa a estar preparado
            {
                isWaitingPlayer2 = false;
            }
        } else if (!isGameActive && !isWaitingPlayer1 && !isWaitingPlayer2 && goalPlayer2.pointsPlayer1 < goalsLimit && goalPlayer1.pointsPlayer2 < goalsLimit && uiCanvasController.countDown <= 0) // Cuando se acabe la cuenta atrás empieza el juego
        {
            isGameActive = true;
            Instantiate(ball, new Vector2(0, 0), Quaternion.identity);
            moveDirection = new Vector2(-4f, Random.Range(-5f, 5f));
            kicked = true;
        }
        //Identifica el ganador de la partida
        if ((goalPlayer2.pointsPlayer1 == goalsLimit || goalPlayer1.pointsPlayer2 == goalsLimit) && isGameActive)                                                  // Si  han obtenido los puntos necesarios
        {
            StartCoroutine(Winner());
        }
    }
    void LateUpdate() {
        if (isGameActive)
        {
            chronometer += Time.deltaTime;                                                                                                                         // Cronómetro para la aparición de los PowerUps
        
            // Saque de puerta
            if (!GameObject.FindGameObjectWithTag("Ball"))                                                                                                         // Si no hay ninguna bola en la escena
            {
                StartCoroutine(GoalKick());
            }

            // Aparición de PowerUps
            if (chronometer >= powerUpsTimer)                                                                                                                      // Si ha llegado al tiempo definido aleatoriamente
            {
                powerUpsCounter = GameObject.FindGameObjectsWithTag("PowerUp");                                                                                    // Se cuentan los Power Ups en pantalla
                chronometer = 0f;                                                                                                                                  // Se reinicia el tiempo
                powerUpsTimer = Random.Range(5, 20);                                                                                                               // Se define otro tiempo aleatorio
                if (powerUpsCounter.Length < 5)                                                                                                                    // Si puede haber más Power Ups
                {
                    Instantiate(powerUps[Random.Range(0, powerUps.Length - 1)], new Vector2(Random.Range(-5.5f, 5.5f), Random.Range(-4, 4)), Quaternion.identity); // Y se instancia el PowerUp
                }
            }
        }
    }

    /* Funciones */

    // Ball Aceleration Power Up
    public void StartBallAceleration()
    {
        StartCoroutine(BallAceleration());
    }

    // Player Increase Power Up
    public void StartIncreaseSelf()
    {
        StartCoroutine(IncreaseSelfCorutine());
    }

    // Reduce Oponent Power Up
    public void StartReduceOponent()
    {
        StartCoroutine(ReduceOponent());
    }

    // Multiple Balls Power Up
    public void StartMultipleBalls()
    {
        StartCoroutine(MultipleBalls());
    }

    public void RestartGame()
    {
        goalPlayer1.pointsPlayer2 = 0;
        goalPlayer2.pointsPlayer1 = 0;
        chronometer = 0;
        isWaitingPlayer1 = true;
        isWaitingPlayer2 = true;
        player1.transform.localScale = new Vector3(0.15f, 1.48f, 1);
        player2.transform.localScale = new Vector3(0.15f, 1.48f, 1);
    }

    /* Coroutinas */
    IEnumerator GoalKick()
    {
        yield return new WaitForSeconds(4);
        if (goalPlayer1.pointForPlayer2 == true)                                                                                                                   // Si han marcado al Jugador1
        {
            Instantiate(ball, new Vector2(-7, 0), Quaternion.identity);                                                                                            // Se crea delante del Jugador1
            goalPlayer1.pointForPlayer2 = false;                                                                                                                   // Se reinicia el estado del gol
            goalKick = 1;                                                                                                                                          // 1 para indicar que saca el Jugador1
        }
        else if (goalPlayer2.pointForPlayer1 == true)                                                                                                              // Si han marcado al Jugador2
        {
            Instantiate(ball, new Vector2(7, 0), Quaternion.identity);                                                                                             // Se crea delante del Jugador2
            goalPlayer2.pointForPlayer1 = false;                                                                                                                   // Se reinicia el estado del gol
            goalKick = 2;                                                                                                                                          // 2 para indicar que saca el Jugador2
        }
        moveDirection = new Vector2(0, 0);                                                                                                                         // Se pone a 0 para que la bola se quede quieta
    }

    IEnumerator MultipleBalls()
    {
        multipleBallsActivation = true;                                                                                                                            // Se indica que se ha iniciado
        for (int i = 0; i < maxBalls; i++)                                                                                                                         // Hasta que no haya el número de bolas estimado
        {
            moveDirection = new Vector2(Random.Range(-5, 5), Random.Range(-5, 5));                                                                                 // Se asigna una dirección
            while (moveDirection.x == 0)                                                                                                                           // Para que la bola no se quede atrapada en bucle, se le obliga a tener una dirección en X
            {
                moveDirection.x = Random.Range(-5, 5);
            }
            Instantiate(ball, multipleBallTransform, Quaternion.identity);                                                                                         // Se instancia una bola
            yield return null;                                                                                                                                     // Se le da un tiempo para que procese bien la información y no se buguee tomando para todas las bolas el último valor de moveDirection
        }
        multipleBallsActivation = false;                                                                                                                           // Se indica que se ha desactivado
    }

    IEnumerator ReduceOponent()
    {
        timeLimitReduce = timeLimit;
        switch (activationForPlayerReduceOponent)
        {
            case "Player1":
                timeLimitReduce = timeLimit;                                                                                                                       // Se guarda el tiempo límite para que no se pierda en las modificaciones
                if (player1.transform.localScale == new Vector3(0.15f, 0.74f, 1))                                                                                  // Si ya se ha reducido se añade tiempo al Power Up
                {
                    timeLimitReduce += timeLimit;
                }
                else
                {
                    player1.transform.localScale = new Vector3(0.15f, 0.74f, 1);                                                                                   // Si no, se reduce
                }
                yield return new WaitForSeconds(timeLimitReduce);                                                                                                  // Una vez se acaba el tiempo se restaura el tamaño
                if (player1.transform.localScale == new Vector3(0.15f, 0.74f, 1))                                                                                  // Si aún está reducido se aplica. Por que si no entra en conflicto con el Power Up contrario
                {
                    player1.transform.localScale = new Vector3(0.15f, 1.48f, 1);
                }
                break;
            case "Player2":
                timeLimitReduce = timeLimit;
                if (player2.transform.localScale == new Vector3(0.15f, 0.74f, 1))
                {
                    timeLimitReduce += timeLimit;
                }
                else
                {
                    player2.transform.localScale = new Vector3(0.15f, 0.74f, 1);
                }
                yield return new WaitForSeconds(timeLimitReduce);
                if (player2.transform.localScale == new Vector3(0.15f, 0.74f, 1))
                {
                    player2.transform.localScale = new Vector3(0.15f, 1.48f, 1);
                }
                break;
        }
    }

    IEnumerator IncreaseSelfCorutine()
    {
        switch (activationForPlayerIncreaseSelf){
            case "Player1":
                timeLimitIncrease = timeLimit;
                if (player1.transform.localScale == new Vector3(0.15f, 3, 1))
                {
                    timeLimitIncrease += timeLimit;
                }
                else
                {
                    player1.transform.localScale = new Vector3(0.15f, 3, 1);
                }
                yield return new WaitForSeconds(timeLimitIncrease);
                if (player1.transform.localScale == new Vector3(0.15f, 3, 1))
                {
                    player1.transform.localScale = new Vector3(0.15f, 1.48f, 1);
                }
                break;
            case "Player2":
                timeLimitIncrease = timeLimit;
                if (player2.transform.localScale == new Vector3(0.15f, 3, 1))
                {
                    timeLimitIncrease += timeLimit;
                }
                else
                {
                    player2.transform.localScale = new Vector3(0.15f, 3, 1);
                }
                yield return new WaitForSeconds(timeLimitIncrease);
                if (player2.transform.localScale == new Vector3(0.15f, 3, 1))
                {
                    player2.transform.localScale = new Vector3(0.15f, 1.48f, 1);
                }
                break;
        }
    }

    IEnumerator BallAceleration()
    {
        actualTime = 0f;                                                                                                                                           // Reinicia el cronómetro
        ballMovement = ballToAcelerate.GetComponent<BallMovement>();                                                                                               // Se obtiene el script de la bola
        speedSaved = ballMovement.speed;                                                                                                                           // Se guarda la velocidad que lleva

        // Mientras no se alcance el límite de tiempo
        while (actualTime < timeLimit)
        {
            if (activationForPlayerBallAceleration == "Player1")                                                                                                   // Si se aplica al Jugador1
            {
                if (ballMovement.lastPlayerTouched == "Player1")                                                                                                   // Si la ha tocado el Jugador1
                {
                    ballMovement.speed = speedSaved * 2;                                                                                                           // Se aumenta la velocidad de la bola
                }
                else if (ballMovement.lastPlayerTouched == "Player2")                                                                                              // Si la ha tocado al Jugador 2
                {
                    ballMovement.speed = speedSaved / 1.2f;                                                                                                           // Se reduce la velocidad de la bola
                }
            }
            else if (activationForPlayerBallAceleration == "Player2")                                                                                              // Si se aplica al Jugador2
            {
                if (ballMovement.lastPlayerTouched == "Player2")                                                                                                   // Si la ha tocado al Jugador2
                {
                    ballMovement.speed = speedSaved * 2;                                                                                                           // Se aumenta la velocidad de la bola
                }
                else if (ballMovement.lastPlayerTouched == "Player1")                                                                                              // Si la ha tocado el Jugador1
                {
                    ballMovement.speed = speedSaved / 1.2f;                                                                                                           // Se reduce la velocidad de la bola
                }
            }
            actualTime += Time.deltaTime;                                                                                                                          // Incrementa con el tiempo entre frames
            yield return null;                                                                                                                                     // Espera al siguiente frame
        }
        ballMovement.speed = speedSaved;                                                                                                                           // Se vuelve a la velocidad normal
    }

    IEnumerator Winner()
    {
        isGameActive = false;                                                                                                                                      // Se termina el Juego
        powerUpsCounter = GameObject.FindGameObjectsWithTag("PowerUp");                                                                                            // Una vez se acaba la partida se buscan todos los power ups para eliminarlos
        GameObject[] ballsCounter = GameObject.FindGameObjectsWithTag("Ball");                                                                                     // Se buscan todas las bolas para eliminarlas y que no se pueda seguir jugando
        StartCoroutine(audioController.WinnerAudio());                                                                                                             // Se activa el audio de victoria
        foreach (GameObject powerUp in powerUpsCounter)                                                                                                            // Se eliminan los Power Ups
        {
            Destroy(powerUp);
        }
        foreach (GameObject ball in ballsCounter)                                                                                                                  // Se eliminan las bolas
        {
            Destroy(ball);
        }
        yield return new WaitForSeconds(4f);                                                                                                                       // Se espera 4 segundos para que se reproduzca el audio
        uiCanvasController.WinnerUI();                                                                                                                             // Se modifica la interfaz para mostrar el ganador
    }
}