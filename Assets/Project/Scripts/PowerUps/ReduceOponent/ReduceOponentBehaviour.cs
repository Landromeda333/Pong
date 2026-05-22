using System.Collections;
using UnityEngine;

public class ReduceOponentBehaviour : MonoBehaviour
{
    [SerializeField] float effectTime;
    float countDown;

    void OnEnable()
    {
        countDown = Time.time;
        transform.localScale = new Vector3(0.15f, 0.74f, 1);
    }

    private void OnDisable()
    {
        transform.localScale = new Vector3(0.15f, 1.48f, 1);
    }

    private void Update()
    {
        if (Time.time - countDown >= effectTime)
        {
            enabled = false;
        }
    }
}