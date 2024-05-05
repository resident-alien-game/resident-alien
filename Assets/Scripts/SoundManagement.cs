using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagement : MonoBehaviour
{
    public AudioSource sirenSound;
    public AudioSource backgroundSound;
    public AudioSource hurtSound;
    public AudioSource shootSound;
    public AudioSource collectSound;
    public AudioSource eatSound;
    public AudioSource drinkSound;
    public AudioSource manDieSound;
    public AudioSource alienDieSound;

    public void siren()
    {
        backgroundSound.Stop();
        sirenSound.Play();
        sirenSound.loop = true;
    }

    public void background()    
    {
        sirenSound.Stop();
        backgroundSound.Play();
        backgroundSound.loop = true;
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
