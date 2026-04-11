using System.Collections;
using UnityEngine;

public class OtherActionsPvsCPU : MonoBehaviour
{
    [SerializeField] GameObject ball;                                                                                                                              // La bola que se va a instanciar una vez que se destruya
    public GoalPlayer1CPU goalPlayer1CPU;                                                                                                                          // Acceso al script de la portería del Jugador1
    public GoalCPU goalCPU;                                                                                                                                        // Acceso al script de la portería del Jugador2
    public Vector2 moveDirection;                                                                                                                                  // Dirección del movimiento
    public int goalKick = 0;                                                                                                                                       // Indica quien saca de puerta
    public bool isGameActive;                                                                                                                                      // Estado de la partida
    BallMovementCPU ballMovementCPU;                                                                                                                               // Script de la bola
    public bool kicked = true;                                                                                                                                     // Estado del saque de portería
    public bool isWaitingPlayer1 = true;                                                                                                                           // Espera a que el Jugador1 esté listo
    public int goalsLimit = 10;                                                                                                                                    // Cantidad de goles objetivo
    [SerializeField] GameObject player1;                                                                                                                           // GameObject del Jugador1
    [SerializeField] GameObject cPU;                                                                                                                               // GameObject de la CPU

    //Variables Audio
    [SerializeField] AudioControllerCPU audioControllerCPU;                                                                                                        // Script del controlador de audio

    //Variables UI
    [SerializeField] UICanvasControllerCPU uiCanvasControllerCPU;                                                                                                  // Script del controlador de la interfaz

    // Variables PowerUps
    float chronometer;                                                                                                                                             // Cronómetro para la aparición de PowerUps
    float powerUpsTimer;                                                                                                                                           // Tiempo aleatorio de aparición de los PowerUps
    [SerializeField] GameObject[] powerUps;                                                                                                                        // Lista de PowerUps
    public float timeLimit = 20f;                                                                                                                                  // Duración del cronómetro en segundos
    GameObject[] powerUpsCounter;                                                                                                                                  // Array para guardar los Power Ups encontrados y poder contarlos

    // Variables BallAceleration
    public string activationForPlayerBallAceleration;                                                                                                              // Qué jugador ha activado el PowerUp
    float speedSaved;                                                                                                                                              // Guarda la velocidad a la que iva la bola para restaurarla cuando se acabe el Power Up
    float actualTime;                                                                                                                                              // Cronómetro para el Power Up de cambio de velocidad de la bola
    public GameObject ballToAcelerate;                                                                                                                             // Bola a la que se le aplicará la aceleración
    
    // Variables IncreaseSelf
    public string activationForPlayerIncreaseSelf;                                                                                                                 // A qué jugador se le aplica este Power Up
    float timeLimitIncrease;                                                                                                                                       // Tiempo límite de duración del Power Up

    // Variables ReduceOponent
    public string activationForPlayerReduceOponent;                                                                                                                // A qué jugador se le aplica este Power Up
    float timeLimitReduce;                                                                                                                                         // Tiempo límite de duración del Power Up

    // Variables MultipleBalls
    [SerializeField] int maxBalls = 3;                                                                                                                             // Cuantas bolas se instanciarán
    public Vector3 multipleBallTransform;                                                                                                                          // Lugar donde instanciar las bolas
    public bool multipleBallsActivation;                                                                                                                           // Estado de activación del Power Up

    private void Awake()
    {
        powerUpsTimer = Random.Range(5f, 20f);                                                                                                                     // Tiempo aleatorio para la aparición de PowerUps
    }

    private void Update()
    {
        if (!isGameActive && isWaitingPlayer1)
        {
            if (Input.GetKeyDown(KeyCode.A))                                                                                                                       // Cuando pulse la tecla, pasa a estar preparado
            {
                isWaitingPlayer1 = false;
            }
        }
        else if (!isGameActive && !isWaitingPlayer1 && goalCPU.pointsPlayer1 < goalsLimit && goalPlayer1CPU.pointsCPU < goalsLimit && uiCanvasControllerCPU.countDown <= 0)  // Cuando se acabe la cuenta atrás empieza el juego
        {
            isGameActive = true;
            Instantiate(ball, new Vector2(0, 0), Quaternion.identity);
            moveDirection = new Vector2(-4f, Random.Range(-5f, 5f));
            kicked = true;
        }
        //Identifica el ganador de la partida
        if ((goalCPU.pointsPlayer1 == goalsLimit || goalPlayer1CPU.pointsCPU == goalsLimit) && isGameActive)                                                       // Si  han obtenido los puntos necesarios
        {
            StartCoroutine(Winner());
        }
    }
    void LateUpdate()
    {
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
        goalPlayer1CPU.pointsCPU = 0;
        goalCPU.pointsPlayer1 = 0;
        chronometer = 0;
        isWaitingPlayer1 = true;
        player1.transform.localScale = new Vector3(0.15f, 1.48f, 1);
        cPU.transform.localScale = new Vector3(0.15f, 1.48f, 1);
    }

    /* Coroutinas */
    IEnumerator GoalKick()
    {
        yield return new WaitForSeconds(4);
        if (goalPlayer1CPU.pointForCPU == true)                                                                                                                    // Si han marcado al Jugador1
        {
            Instantiate(ball, new Vector2(-7, 0), Quaternion.identity);                                                                                            // Se crea delante del Jugador1
            goalPlayer1CPU.pointForCPU = false;                                                                                                                    // Se reinicia el estado del gol
            goalKick = 1;                                                                                                                                          // 1 para indicar que saca el Jugador1
        }
        else if (goalCPU.pointForPlayer1 == true)                                                                                                                  // Si han marcado al Jugador2
        {
            Instantiate(ball, new Vector2(7, 0), Quaternion.identity);                                                                                             // Se crea delante del Jugador2
            goalCPU.pointForPlayer1 = false;                                                                                                                       // Se reinicia el estado del gol
            goalKick = 2;                                                                                                                                          // 1 para indicar que saca el Jugador1
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
                moveDirection.x  = Random.Range(-5, 5);
            }
            Instantiate(ball, multipleBallTransform, Quaternion.identity);                                                                                         // Se instancia una bola
            yield return null;                                                                                                                                     // Se le da un tiempo para que procese bien la información y no se buguee tomando para todas las bolas el último valor de moveDirection
        }
        multipleBallsActivation = false;                                                                                                                           // Se indica que se ha desactivado
    }

    IEnumerator ReduceOponent()
    {
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
            case "CPU":
                timeLimitReduce = timeLimit;
                if (cPU.transform.localScale == new Vector3(0.15f, 0.74f, 1))
                {
                    timeLimitReduce += timeLimit;
                }
                else
                {
                    cPU.transform.localScale = new Vector3(0.15f, 0.74f, 1);
                }
                yield return new WaitForSeconds(timeLimitReduce);
                if (cPU.transform.localScale == new Vector3(0.15f, 0.74f, 1))
                {
                    cPU.transform.localScale = new Vector3(0.15f, 1.48f, 1);
                }
                break;
        }
    }

    IEnumerator IncreaseSelfCorutine()
    {
        switch (activationForPlayerIncreaseSelf)
        {
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
            case "CPU":
                timeLimitIncrease = timeLimit;
                if (cPU.transform.localScale == new Vector3(0.15f, 3, 1))
                {
                    timeLimitIncrease += timeLimit;
                }
                else
                {
                    cPU.transform.localScale = new Vector3(0.15f, 3, 1);
                }
                yield return new WaitForSeconds(timeLimitIncrease);
                if (cPU.transform.localScale == new Vector3(0.15f, 3, 1))
                {
                    cPU.transform.localScale = new Vector3(0.15f, 1.48f, 1);
                }
                break;
        }
    }

    IEnumerator BallAceleration()
    {
        actualTime = 0f;                                                                                                                                           // Reinicia el cronómetro
        ballMovementCPU = ballToAcelerate.GetComponent<BallMovementCPU>();                                                                                         // Se obtiene el script de la bola
        speedSaved = ballMovementCPU.speed;                                                                                                                        // Se guarda la velocidad que lleva

        // Mientras no se alcance el límite de tiempo
        while (actualTime < timeLimit)
        {
            if (activationForPlayerBallAceleration == "Player1")                                                                                                   // Si se aplica al Jugador1
            {
                if (ballMovementCPU.lastPlayerTouched == "Player1")                                                                                                // Si la ha tocado el Jugador1
                {
                    ballMovementCPU.speed = speedSaved * 2;                                                                                                        // Se aumenta la velocidad de la bola
                }
                else if (ballMovementCPU.lastPlayerTouched == "CPU")                                                                                               // Si la ha tocado CPU
                {
                    ballMovementCPU.speed = speedSaved / 1.2f;                                                                                                     // Se reduce la velocidad de la bola
                }
            }
            else if (activationForPlayerBallAceleration == "CPU")                                                                                                  // Si se aplica a CPU
            {
                if (ballMovementCPU.lastPlayerTouched == "CPU")                                                                                                    // Si la ha tocado CPU
                {
                    ballMovementCPU.speed = speedSaved * 2;                                                                                                        // Se aumenta la velocidad de la bola
                }
                else if (ballMovementCPU.lastPlayerTouched == "Player1")                                                                                           // Si la ha tocado el Jugador1
                {
                    ballMovementCPU.speed = speedSaved / 1.2f;                                                                                                     // Se reduce la velocidad de la bola
                }
            }
            actualTime += Time.deltaTime;                                                                                                                          // Incrementa con el tiempo entre frames
            yield return null;                                                                                                                                     // Espera al siguiente frame
        }
        ballMovementCPU.speed = speedSaved;                                                                                                                        // Se vuelve a la velocidad normal
    }

    IEnumerator Winner()
    {
        isGameActive = false;                                                                                                                              // Se termina el Juego
        powerUpsCounter = GameObject.FindGameObjectsWithTag("PowerUp");                                                                                    // Una vez se acaba la partida se buscan todos los power ups para eliminarlos
        GameObject[] ballsCounter = GameObject.FindGameObjectsWithTag("Ball");                                                                             // Se buscan todas las bolas para eliminarlas y que no se pueda seguir jugando
        StartCoroutine(audioControllerCPU.WinnerAudio());                                                                                                  // Se activa el audio de victoria
        foreach (GameObject powerUp in powerUpsCounter)                                                                                                    // Se eliminan los Power Ups
        {
            Destroy(powerUp);
        }
        foreach (GameObject ball in ballsCounter)                                                                                                          // Se eliminan las bolas
        {
            Destroy(ball);
        }
        yield return new WaitForSeconds(4f);                                                                                                               // Se espera 4 segundos para que se reproduzca el audio
        uiCanvasControllerCPU.WinnerUI();                                                                                                                  // Se modifica la interfaz para mostrar el ganador
    }
}