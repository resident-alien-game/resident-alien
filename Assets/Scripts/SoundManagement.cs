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

    private void Start()
    {
        background();
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
}
