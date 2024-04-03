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
            Destroy(other.gameObject);
            status.AddPieces(1);
            status.AddScore(10);
            collectPieceSound.Play();
        }
    }
}
