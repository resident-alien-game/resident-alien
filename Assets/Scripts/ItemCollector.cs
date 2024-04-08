using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private AudioSource collectPieceSound;
    private StatusManagement status;

    private void Start()
    {
        status = GameObject.Find("Status").GetComponent<StatusManagement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Piece"))
        {
            if (status.CanUseSpell())
            {
                Destroy(other.gameObject);
                status.AddPieces(1);
                status.AddScore(10);
                status.ReduceEnergy(1);
                collectPieceSound.Play();
            }
        }

        if (other.gameObject.CompareTag("Food"))
        {
            if (status.CanEatFood())
            {
                Destroy(other.gameObject);
                status.AddHP(1);
                status.AddScore(20);
                collectPieceSound.Play();
            }
        }

        if (other.gameObject.CompareTag("Drink"))
        {
            if (status.CanDrink())
            {
                Destroy(other.gameObject);
                status.AddEnergy(1);
                status.AddScore(20);
                collectPieceSound.Play();
            }
        }
    }
}
