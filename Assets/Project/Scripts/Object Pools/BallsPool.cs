using UnityEngine;

//# Piscina de objetos de las pelotas #//
public class BallsPool : ObjectPool
{
    public static BallsPool Instance;

    private void Awake()
    {
        Instance = this;
    }

    /* Métodos */
    // Comprueba y devuelve si hay una pelota activa
    public bool CheckActiveBalls()
    {
        foreach (GameObject ball in pooledObjects)
        {
            if (ball.activeSelf)
            {
                return true;
            }
        }
        return false;
    }
}