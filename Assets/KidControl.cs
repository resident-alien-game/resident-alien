using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidControl : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float changeDirectionInterval = 2f;
    
    private float timer;
    private Vector3 randomDirection;

    void Start()
    {
        timer = changeDirectionInterval;
        GetNewRandomDirection();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            GetNewRandomDirection();
            timer = changeDirectionInterval;
        }

        Move();
    }

    void Move()
    {
        Vector3 movement = randomDirection.normalized * moveSpeed * Time.deltaTime;
        transform.Translate(movement);
    }

    void GetNewRandomDirection()
    {
        randomDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;
    }
}
