using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidControl : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float changeDirectionInterval = 2f;
    
    private float timer;
    private Vector3 randomDirection;
    public float groundSize = 100f; // Size of the ground

    private FieldOfView fieldOfView;
    private FormChange formChange;
    public GameObject alien;


    void Start()
    {
        timer = changeDirectionInterval;
        GetNewRandomDirection();
        fieldOfView = GetComponent<FieldOfView>();
        formChange = alien.GetComponent<FormChange>();
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

        // Check to see if alien is being seen by a kid. If so, set alien as discovered.
        if (fieldOfView.canSeePlayer)
            formChange.Discovered();
    }

    void Move()
    {
        Vector3 movement = randomDirection.normalized * moveSpeed * Time.deltaTime;

        // Face the direction
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(movement.x, 0, movement.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

        transform.Translate(movement);

        // Clamp object's position to stay within the ground area
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -groundSize / 2f, groundSize / 2f);
        clampedPosition.z = Mathf.Clamp(clampedPosition.z, -groundSize / 2f, groundSize / 2f);
        transform.position = clampedPosition;
    }

    void GetNewRandomDirection()
    {
        randomDirection = new Vector3(UnityEngine.Random.Range(-1f, 1f), 0f, UnityEngine.Random.Range(-1f, 1f)).normalized;
    }
}
