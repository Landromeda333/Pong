using UnityEngine;

//# Este script se encarga de gestionar el recogible del power up que instancia varias bolas #//
public class MultipleBalls : MonoBehaviour, IResettable
{
    [SerializeField] int ballsToSpawn;                                                            // Cantidad de bolas a mostrar

    /* Cuando la bola colisione al Power Up */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartMultipleBalls(collision.gameObject.transform.position);
        gameObject.SetActive(false);
    }

    /* Métodos */

    // Instanciación de las bolas
    public void StartMultipleBalls(Vector2 location)
    {
        for (int i = 0; i < ballsToSpawn; i++)
        {
            GameObject ball = BallsPool.Instance.GetPooledObject();                               // Aparición de una bola de la piscina de objetos
            if (ball)
            {
                ball.transform.position = location;                                               // Posicionamiento de la nueva bola en la posición de la bola original
                Vector2 moveDirection = new Vector2(Random.Range(-5, 5), Random.Range(-5, 5));    // Se asigna una dirección
                // Para que la bola no se quede inmóvil, se le obliga a tener una dirección en X
                while (moveDirection.x == 0)
                {
                    moveDirection.x = Random.Range(-5, 5);
                }
                ball.GetComponent<BallMovement>().rb.AddForce(moveDirection, ForceMode2D.Impulse);// Implementación del impulso
            }
        }
    }

    /* Método para IResetteable */
    public void OnGameOver()
    {
        gameObject.SetActive(false);
    }
}