using UnityEngine;

//# Este script se encarga de la piscina de objetos de los power ups #//
public class PowerUpPool : ObjectPool
{
    /* Power Up Pool Game Event */
    [SerializeField] PowerUpPoolGameEvent savePool; // Solicitud de guardado de la referencia a la piscina

    public static PowerUpPool Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        savePool.Raise(this);
    }
}