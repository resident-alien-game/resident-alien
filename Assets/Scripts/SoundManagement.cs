using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagement : MonoBehaviour
{
    public AudioSource policeSound;
    public AudioSource backgroundSound;
    public AudioSource hurtSound;
    public AudioSource shootSound;
    public AudioSource collectSound;
    public AudioSource eatSound;
    public AudioSource drinkSound;
    public AudioSource manDieSound;
    public AudioSource alienDieSound;
    public AudioSource alienGrowlSound;
    public AudioSource spaceshipSound;
    public AudioSource spaceshipAwaySound;

    public float timeInterval = 10.0f;
    public bool isAlien;

    private float timePassed = 0f;


    private void Start()
    {
        background();
    }

    private void Update()
    {
        if (isAlien)
        {
            if (timePassed >= timeInterval)
            {
                alienGrowl();
                timePassed = 0f;
            }
            timePassed += Time.deltaTime;
        }
    }

    public void police(bool play)
    {
        if (play)
        {
            policeSound.Play();
        } else
        {
            policeSound.Stop();
        }
    }

    public void background()    
    {
        backgroundSound.Play();
    }

    public void hurt()
    {
        hurtSound.Play();
    }

    public void shoot()
    {
        shootSound.Play();
    }

    public void collect()
    {
        collectSound.Play();
    }

    public void eat()
    {
        eatSound.Play();
    }
    public void drink()
    {
        drinkSound.Play();
    }
    public void manDie()
    {
        manDieSound.Play();
    }

    public void alienDie()
    {
        alienDieSound.Play();
    }

    public void alienGrowl()
    {
        alienGrowlSound.Play();
    }

    public void spaceShip(bool play)
    {
        if (play) spaceshipSound.Play();
        else spaceshipSound.Stop();
    }

    public void spaceShipAway()
    {
        spaceshipAwaySound.Play();
    }
}
