using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiCanvasController : MonoBehaviour
{
    [SerializeField] OtherActions otherActions;                                                             // Script que controla las acciones del juego
    [SerializeField] GoalPlayer1 goalPlayer1;                                                               // Script de la portería del Jugador1
    [SerializeField] GoalPlayer2 goalPlayer2;                                                               // Script de la portería del Jugador2
    [SerializeField] GameObject victoryText;                                                                // Texto de la victoria
    [SerializeField] GameObject player1Text;                                                                // Texto de preparado del Jugador1
    [SerializeField] GameObject player2Text;                                                                // Texto de preparado del Jugador2
    [SerializeField] GameObject restartGameButton;                                                          // Botón de reinicio
    [SerializeField] GameObject controlsButton;                                                             // Botón de controles
    [SerializeField] GameObject backButton;                                                                 // Botón de volver
    [SerializeField] GameObject player1Points;                                                              // Marcador del Jugador1
    [SerializeField] GameObject player2Points;                                                              // Marcador del Jugador2
    [SerializeField] GameObject player1ControlsText;                                                        // Texto de los controles del Jugador1
    [SerializeField] GameObject player2ControlsText;                                                        // Texto de los controles del Jugador2
    [SerializeField] GameObject player1;                                                                    // Jugador1
    [SerializeField] GameObject player2;                                                                    // Jugador2
    [SerializeField] GameObject backToMenuButton;                                                           // Botón de volver al menú
    public float countDown = 3.4f;                                                                          // Cuenta atrás

    private void Awake()
    {
        backToMenuButton.transform.localPosition = new Vector2(0, -40);                                     // Se posiciona el botón en su lugar de origen para cuando se mueve al terminar la partida
    }

    private void Update()
    {
        player1Points.GetComponent<TextMeshProUGUI>().text = goalPlayer2.pointsPlayer1.ToString();          // Se actualiza el marcador
        player2Points.GetComponent<TextMeshProUGUI>().text = goalPlayer1.pointsPlayer2.ToString();          // Se actualiza el marcador

        if (!otherActions.isGameActive && (otherActions.isWaitingPlayer1 || otherActions.isWaitingPlayer2)) // Mientras se espera a los jugadores
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                player1Text.GetComponent<TextMeshProUGUI>().text = "¡Preparado!";
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                player2Text.GetComponent<TextMeshProUGUI>().text = "¡Preparado!";
            }
        }
        else if (otherActions.isGameActive && !otherActions.isWaitingPlayer1 && !otherActions.isWaitingPlayer2 && otherActions.goalPlayer2.pointsPlayer1 < otherActions.goalsLimit && otherActions.goalPlayer1.pointsPlayer2 < otherActions.goalsLimit && countDown <= 0) // Cuando el juego está activo
        {
            player1Text.SetActive(false);
            player2Text.SetActive(false);
            victoryText.GetComponent<TextMeshProUGUI>().text = "";
            backButton.SetActive(false);
        }
        if (countDown > 0 && !otherActions.isWaitingPlayer1 && !otherActions.isWaitingPlayer2)              // Cuenta atrás
        {
            controlsButton.SetActive(false);
            backToMenuButton.SetActive(false);
            countDown -= Time.deltaTime;
            victoryText.GetComponent<TextMeshProUGUI>().text = countDown.ToString("0");
        }
    }

    public void RestartGame()                                                                               // Reinicia el juego
    {
        countDown = 3.4f;
        victoryText.GetComponent<TextMeshProUGUI>().text = "";
        player1Text.SetActive(true);
        player1Text.GetComponent<TextMeshProUGUI>().text = "Pulsa A cuando estés listo";
        player2Text.SetActive(true);
        player2Text.GetComponent<TextMeshProUGUI>().text = "Pulsa Flecha derecha cuando estés listo";
        restartGameButton.SetActive(false);
        controlsButton.SetActive(true);
        backToMenuButton.transform.localPosition = new Vector2(0, -40);
    }

    public void ViewControls()                                                                              // Muestra los controles
    {
        player1.SetActive(false);
        player2.SetActive(false);
        player1Text.SetActive(false);
        player2Text.SetActive(false);
        controlsButton.SetActive(false);
        player1Points.SetActive(false);
        player2Points.SetActive(false);
        backButton.SetActive(true);
        player1ControlsText.SetActive(true);
        player2ControlsText.SetActive(true);
        backToMenuButton.SetActive(false);
    }

    public void BackToGame()                                                                                // Vuelve al juego y quita los controles
    {
        player1.SetActive(true);
        player2.SetActive(true);
        player1Text.SetActive(true);
        player2Text.SetActive(true);
        player1Points.SetActive(true);
        player2Points.SetActive(true);
        backButton.SetActive(false);
        controlsButton.SetActive(true);
        player1ControlsText.SetActive(false);
        player2ControlsText.SetActive(false);
        backToMenuButton.SetActive(true);
    }

    public void WinnerUI()                                                                                  // Interfaz de la victoria
    {
        if (otherActions.goalPlayer2.pointsPlayer1 >= otherActions.goalsLimit)
        {
            victoryText.GetComponent<TextMeshProUGUI>().text = "La victoria es para el Jugador1";
        }
        else if (otherActions.goalPlayer1.pointsPlayer2 >= otherActions.goalsLimit)
        {
            victoryText.GetComponent<TextMeshProUGUI>().text = "La victoria es para el Jugador2";
        }
        restartGameButton.SetActive(true);
        backToMenuButton.transform.localPosition = new Vector2(0, -100);
        backToMenuButton.SetActive(true);
    }

    public void BackToMenu()                                                                                // Vuelve al menú principal
    {
        SceneManager.LoadScene("GameModeSelection");
    }
}