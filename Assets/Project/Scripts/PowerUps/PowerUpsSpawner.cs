using System.Collections.Generic;
using UnityEngine;

//# Este script se encarga de gestionar la aparición de los power ups en partida #//
public class PowerUpsSpawner : MonoBehaviour
{
    /* Power Up Pools */
    List<PowerUpPool> powerUppools = new List<PowerUpPool>();// Piscinas que guardan los power ups

    [SerializeField] float minTime = 5, maxTime = 20;        // Tiempo mínimo y máximo para spawnear power ups

    float chronometer, powerUpsTimer;                        // Guarda el tiempo del juego para hacer la comprobación, Guarda el tiempo en el que se debe spawnear un power up

    private void Start()
    {
        chronometer = Time.time;
        powerUpsTimer = Random.Range(minTime, maxTime);      // Tiempo aleatorio para la aparición de PowerUps
    }

    void Update()
    {
        // Aparición de PowerUps
        if (Time.time - chronometer >= powerUpsTimer)        // Si ha llegado al tiempo definido aleatoriamente
        {
            chronometer = Time.time;                         // Se reinicia el tiempo
            powerUpsTimer = Random.Range(minTime, maxTime);  // Se define otro tiempo aleatorio
            // Se pide un power up aleatorio
            GameObject pwrUp = powerUppools[Random.Range(0, powerUppools.Count)].GetPooledObject();
            if (pwrUp)
            {
                pwrUp.transform.position = new Vector2(Random.Range(-7, 7), Random.Range(-4, 4));
            }
        }
    }

    /* Métodos para SO Event SavePool */
    // Guarda las piscinas de power ups
    public void SavePool(PowerUpPool pool)
    {
        powerUppools.Add(pool);
    }
}