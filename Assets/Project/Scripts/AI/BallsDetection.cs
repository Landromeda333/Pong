using System.Collections.Generic;
using UnityEngine;

public class BallsDetection : MonoBehaviour
{
    [HideInInspector] public List<GameObject> balls = new();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            balls.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            balls.Remove(other.gameObject);
        }
    }
}