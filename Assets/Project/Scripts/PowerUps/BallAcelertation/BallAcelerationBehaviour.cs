using UnityEngine;

//# Este script se encarga de gestionar el comportamiento de la bola al tomar el power up que acelera la bola al jugador contrario #//
public class BallAcelerationBehaviour : MonoBehaviour
{
    /* Scripts */
    BallMovement movement;                           // Script que se encarga del moviento de la bola

    [SerializeField] float effecTime;                // Tiempo de efecto del power up
    float originalSpeed;                             // Velocidad de la bola
    float countdown;                                 // Guarda el tiempo del juego para hacer la comprobación

    [HideInInspector] public int playerAdvantaged;   // Player con la ventaja

    private void Awake()
    {
        movement = GetComponent<BallMovement>();
    }

    private void OnEnable()
    {
        originalSpeed = movement.speed;
        playerAdvantaged = movement.lastPlayerTouched;
        countdown = Time.time;
    }

    private void OnDisable()
    {
        movement.speed = originalSpeed;
    }

    private void Update()
    {
        // Si ya ha pasado el tiempo correspondiente
        if (Time.time - countdown >= effecTime)
        {
            enabled = false;
        }
    }

    private void OnCollisionEnter2D (Collision2D collision)
    {
        if (enabled)
        {
            if (collision.gameObject.CompareTag("Player") && collision.gameObject.TryGetComponent<PlayerData>(out PlayerData player))
            {
                // Se accede al PlayerBehaviour para tener una fuente fiable. Porque el lastPlayerTouched puede que no se actualice a tiempo y de lugar a un mal funcionamiento
                if (playerAdvantaged != player.playerNum)
                {
                    movement.rb.linearVelocityX /= 2;
                }
                // Acción para el jugador perjudicado
                else
                {
                    movement.rb.linearVelocityX = Mathf.Clamp(movement.rb.linearVelocityX*= 2, -movement.maxSpeed, movement.maxSpeed);
                }
            }
        }
    }
}