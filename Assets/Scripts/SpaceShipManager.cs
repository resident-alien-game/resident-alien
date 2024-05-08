using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipManager : MonoBehaviour
{
    private Transform startingPosition;
    public float approachingSpeed;
    public float flyingOverSpeed;
    public SoundManagement soundManagement;

    private float currentSpeed;
    private Vector3 directionOfMovement;
    private bool slowDown;
    void Start()
    {
        startingPosition = transform;
        currentSpeed = approachingSpeed;
        directionOfMovement = Vector3.forward;
        slowDown = true;
    }

    void Update()
    {
        transform.Translate(directionOfMovement * currentSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // slow down or speed up
        if (other.gameObject.CompareTag("ChangeSpeed"))
        {
            if (slowDown)
            {
                currentSpeed = flyingOverSpeed;
                soundManagement.spaceShip(true);
            }
            else
            {
                currentSpeed = approachingSpeed;
                soundManagement.spaceShip(false);
                soundManagement.spaceShipAway();
            }
            slowDown = !slowDown;
        } else if (other.gameObject.CompareTag("ChangeDirection"))
        {
            directionOfMovement *= -1;
        }
        // turn back
    }

}
