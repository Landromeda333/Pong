using UnityEngine;

public class BallAcelerationBehaviour : MonoBehaviour
{
    BallMovement movement;

    float originalSpeed;
    [SerializeField] float effecTime;
    float countdown;

    [HideInInspector] public int playerDisadvantaged;

    private void Awake()
    {
        movement = GetComponent<BallMovement>();
    }

    private void OnEnable()
    {
        originalSpeed = movement.speed;
        countdown = Time.time;
    }

    private void OnDisable()
    {
        movement.speed = originalSpeed;                 // Se vuelve a la velocidad normal
    }

    private void Update()
    {
        if (Time.time - countdown >= effecTime)
        {
            enabled = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (enabled)
        {
            if (playerDisadvantaged == 1)               // Si se aplica al Jugador1
            {
                if (movement.rb.linearVelocityX > 1)    // Si la ha tocado el Jugador1
                {
                    movement.rb.linearVelocityX /= 2;   // Se aumenta la velocidad de la bola
                }
                else                                    // Si la ha tocado al Jugador 2
                {
                    movement.rb.linearVelocityX *= 2;   // Se reduce la velocidad de la bola
                }
            }
            else                                        // Si se aplica al Jugador2
            {
                if (movement.rb.linearVelocityX > 1)    // Si la ha tocado el Jugador1
                {
                    movement.rb.linearVelocityX *= 2;   // Se aumenta la velocidad de la bola
                }
                else                                    // Si la ha tocado al Jugador 2
                {
                    movement.rb.linearVelocityX /= 2;   // Se reduce la velocidad de la bola
                }
            }
            Debug.Log(movement.rb.linearVelocityX);
        }
    }
}