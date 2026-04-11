using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UICanvasControllerCPU : MonoBehaviour
{

    [SerializeField] OtherActionsPvsCPU otherActionsPvsCPU;                                    // Script que controla las acciones del juego
    [SerializeField] GoalPlayer1CPU goalPlayer1CPU;                                            // Script de la portería del Jugador1
    [SerializeField] GoalCPU goalCPU;                                                          // Script de la portería deL CPU
    [SerializeField] GameObject victoryText;                                                   // Texto de la victoria
    [SerializeField] GameObject player1Text;                                                   // Texto de preparado del Jugador1
    [SerializeField] GameObject cPUText;                                                       // Texto de preparado del CPU
    [SerializeField] GameObject restartGameButton;                                             // Botón de reinicio
    [SerializeField] GameObject controlsButton;                                                // Botón de controles
    [SerializeField] GameObject backButton;                                                    // Botón de volver
    [SerializeField] GameObject player1Points;                                                 // Marcador del Jugador1
    [SerializeField] GameObject cPUPoints;                                                     // Marcador del CPU
    [SerializeField] GameObject player1ControlsText;                                           // Texto de los controles del Jugador1
    [SerializeField] GameObject player1;                                                       // Jugador1
    [SerializeField] GameObject cPU;                                                           // CPU
    [SerializeField] GameObject backToMenuButton;                                              // Botón de volver al menú
    public float countDown = 3.4f;                                                             // Cuenta atrás

    private void Awake()
    {
        backToMenuButton.transform.localPosition = new Vector2(0, -40);                        // Se posiciona el botón en su lugar de origen para cuando se mueve al terminar la partida
    }

    private void Update()
    {
        player1Points.GetComponent<TextMeshProUGUI>().text = goalCPU.pointsPlayer1.ToString(); // Se actualiza el marcador
        cPUPoints.GetComponent<TextMeshProUGUI>().text = goalPlayer1CPU.pointsCPU.ToString();  // Se actualiza el marcador

        if (!otherActionsPvsCPU.isGameActive && otherActionsPvsCPU.isWaitingPlayer1)           // Mientras se espera aL Jugador1
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                player1Text.GetComponent<TextMeshProUGUI>().text = "¡Preparado!";
            }
            cPUText.GetComponent<TextMeshProUGUI>().text = "¡Preparado!";
        }
        else if (otherActionsPvsCPU.isGameActive && !otherActionsPvsCPU.isWaitingPlayer1 && otherActionsPvsCPU.goalCPU.pointsPlayer1 < otherActionsPvsCPU.goalsLimit && otherActionsPvsCPU.goalPlayer1CPU.pointsCPU < otherActionsPvsCPU.goalsLimit && countDown <= 0)
        {
            player1Text.SetActive(false);
            cPUText.SetActive(false);
            victoryText.GetComponent<TextMeshProUGUI>().text = "";
            backButton.SetActive(false);
        }

        if (countDown > 0 && !otherActionsPvsCPU.isWaitingPlayer1)                             // Cuenta atrás
        {
            controlsButton.SetActive(false);
            backToMenuButton.SetActive(false);
            countDown -= Time.deltaTime;
            victoryText.GetComponent<TextMeshProUGUI>().text = countDown.ToString("0");
        }
    }

    public void RestartGame()                                                                  // Reinicia el juego
    {
        countDown = 3.4f;
        victoryText.GetComponent<TextMeshProUGUI>().text = "";
        player1Text.SetActive(true);
        player1Text.GetComponent<TextMeshProUGUI>().text = "Pulsa A cuando estés listo";
        cPUText.SetActive(true);
        cPUText.GetComponent<TextMeshProUGUI>().text = "Pulsa Flecha derecha cuando estés listo";
        restartGameButton.SetActive(false);
        controlsButton.SetActive(true);
        backToMenuButton.transform.localPosition = new Vector2(0, -40);
    }

    public void ViewControls()                                                                 // Muestra los controles
    {
        player1.SetActive(false);
        cPU.SetActive(false);
        player1Text.SetActive(false);
        cPUText.SetActive(false);
        controlsButton.SetActive(false);
        player1Points.SetActive(false);
        cPUPoints.SetActive(false);
        backButton.SetActive(true);
        player1ControlsText.SetActive(true);
        backToMenuButton.SetActive(false);
    }

    public void BackToGame()                                                                   // Vuelve al juego y quita los controles
    {
        player1.SetActive(true);
        cPU.SetActive(true);
        player1Text.SetActive(true);
        cPUText.SetActive(true);
        player1Points.SetActive(true);
        cPUPoints.SetActive(true);
        backButton.SetActive(false);
        controlsButton.SetActive(true);
        player1ControlsText.SetActive(false);
        backToMenuButton.SetActive(true);
    }

    public void WinnerUI()                                                                     // Interfaz de la victoria
    {
        if (otherActionsPvsCPU.goalCPU.pointsPlayer1 >= otherActionsPvsCPU.goalsLimit)
        {
            victoryText.GetComponent<TextMeshProUGUI>().text = "La victoria es para el Jugador1";
        }
        else if (otherActionsPvsCPU.goalPlayer1CPU.pointsCPU >= otherActionsPvsCPU.goalsLimit)
        {
            victoryText.GetComponent<TextMeshProUGUI>().text = "La victoria es para el Jugador2";
        }
        restartGameButton.SetActive(true);
        backToMenuButton.transform.localPosition = new Vector2(0, -100);
        backToMenuButton.SetActive(true);
    }

    public void BackToMenu()                                                                   // Vuelve al menú principal
    {
        SceneManager.LoadScene("GameModeSelection");
    }
}