using UnityEngine;

public class IncreaseSelfBehaviour : MonoBehaviour
{
    [SerializeField] float effectTime;
    float timer;

    private void OnEnable()
    {
        timer = Time.time;
        transform.localScale = new Vector3(0.15f, 3, 1);
    }

    private void OnDisable()
    {
        transform.localScale = new Vector3(0.15f, 1.48f, 1);
    }

    private void Update()
    {
        if (Time.time - timer >= effectTime)
        {
            enabled = false;
        }
    }
}