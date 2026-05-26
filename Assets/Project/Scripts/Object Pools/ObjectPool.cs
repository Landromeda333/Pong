using System.Collections.Generic;
using UnityEngine;

//# Este script se encarga de ser la base para las piscinas de objetos #//
public class ObjectPool : MonoBehaviour
{
    /* Game Objects */
    protected List<GameObject> pooledObjects = new List<GameObject>();// Objetos en la piscina
    [SerializeField] GameObject objectToPool;                         // Tipo de objeto

    [SerializeField] int amountToPool;                                // Cantidad a guardar de ese objeto

    private void OnEnable()
    {
        GameObject tmp;
        // Instanciación de los objetos
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }

    private void OnDisable()
    {
        foreach (GameObject obj in pooledObjects)
        {
            Destroy(obj);
        }
        pooledObjects.Clear();
    }

    /* Métodos */
    // Exposición de un objeto de la piscina
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                pooledObjects[i].SetActive(true);
                return pooledObjects[i];
            }
        }
        return null;
    }
}