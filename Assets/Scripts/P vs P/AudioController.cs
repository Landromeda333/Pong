using System.Collections;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] UiCanvasController uiCanvasController;                                                                                     // Script que controla la interfaz de usuario
    [SerializeField] OtherActions otherActions;                                                                                                 // Script que controla las acciones del juego
    [SerializeField] AudioClip countDownClip;                                                                                                   // Sonido de la cuenta atrás
    [SerializeField] AudioClip backgroundMusic;                                                                                                 // Música de fondo
    [SerializeField] AudioClip victoryClip;                                                                                                     // Sonido de victoria
    [SerializeField] GameObject player1;                                                                                                        // Jugador 1
    [SerializeField] GameObject player2;                                                                                                        // Jugador 2
    AudioSource audioSource;                                                                                                                    // Componente AudioSource del menú principal

    private void Start()
    {
        audioSource = GameObject.Find("AudioSource").GetComponent<AudioSource>();                                                               // Se busca el objeto que contiene el componente AudioSource del menú principal
    }

    void Update()
    {
        if (!otherActions.isGameActive && !otherActions.isWaitingPlayer1 && !otherActions.isWaitingPlayer2 && uiCanvasController.countDown > 0) // Cuando empieza la cuenta atrás se reduce gradualmente el volumen
        {
            GetComponent<AudioSource>().enabled = true;
            GetComponent<AudioSource>().volume += Time.deltaTime;
            audioSource.GetComponent<AudioSource>().volume -= Time.deltaTime;
        }
    }

    private void LateUpdate()
    {
        if (otherActions.isGameActive)                                                                                                          // Cuando empieza la partida
        {
            audioSource.enabled = false;
            audioSource.volume = 0;
            GetComponent<AudioSource>().resource = backgroundMusic;
            if (GetComponent<AudioSource>().loop == false)
            {
                GetComponent<AudioSource>().volume = 0;
                GetComponent<AudioSource>().loop = true;
                GetComponent<AudioSource>().Play();
            }
            if (GetComponent<AudioSource>().volume < 0.5f)
            {
                GetComponent<AudioSource>().volume += Time.deltaTime;
            }
        }
    }

    public void RestartGame()                                                                                                                   // Reinicio del juego
    {
        GetComponent<AudioSource>().enabled = false;
        GetComponent<AudioSource>().loop = false;
        GetComponent<AudioSource>().resource = countDownClip;
        player1.GetComponent<AudioSource>().enabled = false;
        player2.GetComponent<AudioSource>().enabled = false;
    }

    public IEnumerator WinnerAudio()                                                                                                            // Sonido de victoria
    {
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().loop = false;
        yield return new WaitForSeconds(4f);
        GetComponent<AudioSource>().PlayOneShot(victoryClip);
        yield return new WaitForSeconds(7f);
        audioSource.enabled = true;
        for (float i = 0; audioSource.volume < 1; i =+ Time.deltaTime)
        {
            audioSource.volume += i;
            yield return new WaitForEndOfFrame();
        }
    }
}