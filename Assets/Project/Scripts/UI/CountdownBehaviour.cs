using UnityEngine;
using UnityEngine.UIElements;

/* Este script se encarga de gestionar la cuenta atrás del inicio de la partida */
public class CountdownBehaviour : MonoBehaviour
{
    /* SO Events */
    [SerializeField] GameEvent startToPlay;                         // Avisa del comienzo de la partida

    [SerializeField] float countdownTime = 3.4f;                    // Cuenta atrás
    float operando;                                                 // Guarda el tiempo al que debe llegar Time.time para empezar la partida

    Label _countdownText;                                           // Texto de la cuenta atrás

    private void OnEnable()
    {
        operando = Time.time + countdownTime;
        _countdownText = GetComponent<UIDocument>().rootVisualElement.Q<Label>("countdown-text");
        _countdownText.text = countdownTime.ToString("0");
        _countdownText.style.display = DisplayStyle.Flex;
    }

    // Cuando se lance el evento StartToPlay este script se desactiva
    private void OnDisable()
    {
        _countdownText.style.display = DisplayStyle.None;
    }

    private void Update()
    {
        _countdownText.text = (operando - Time.time).ToString("0"); // Actualización del contador

        // Si ha terminado la cuenta atrás
        if (operando - Time.time <= 0)
        {
            startToPlay.Raise();
            enabled = false;
        }
    }
}