using UnityEngine;

/* Este script se encarga de gestionar los efectos de los power ups que cambian la escala de los jugadores */
public class ScaleBehaviour : MonoBehaviour
{
    PlayerData data;             // Clase que se encarga del comportamiento del jugador

    public Vector2 targetScale;                  // Escala a la que cambia el jugador
    Vector2 originalScale;                       // Escala original del jugador

    [SerializeField] float effectTime;           // Tiempo del efecto del power up
    float countDown;                             // Guarda el tiempo del juego para hacer la comparación

    private void Awake()
    {
        data = GetComponent<PlayerData>();
        originalScale = transform.localScale;
    }

    void OnEnable()
    {
        ScaleBehaviour[] scalers = GetComponents<ScaleBehaviour>();
        foreach (ScaleBehaviour scaler in scalers)
        {
            if (scaler != this && scaler.enabled)
            {
                scaler.enabled = false;
            }
        }
        countDown = Time.time;
        transform.localScale = targetScale;      // Tamaño reducido
    }

    private void Update()
    {
        // Si ha terminado el contador
        if (Time.time - countDown >= effectTime)
        {
            enabled = false;
            transform.localScale = originalScale;// Tamaño normal
        }
    }

    /* Métodos para SO Event IncreaseSelf y ReduceOponent */
    public void CheckPlayerNum(int num)
    {
        // Si el jugador que debe reducirse es este
        if (num == data.playerNum)
        {
            if (!enabled)
            {
                enabled = true;
            }
            else
            {
                countDown = Time.time;
            }
        }
    }
}