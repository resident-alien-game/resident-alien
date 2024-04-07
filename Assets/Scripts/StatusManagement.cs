using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusManagement : MonoBehaviour
{
    public static StatusManagement instance;
    public TMP_Text HPText;
    public TMP_Text EnergyText;
    public TMP_Text ScoreText;
    public TMP_Text PiecesText;

    int HP = 5;
    int Energy = 5;
    int Score = 0;
    int Pieces = 0;
    int TotalPieces;

    private void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        TotalPieces = GameObject.FindGameObjectsWithTag("Piece").Length;
        HPText.text = "HP: " + HP;
        EnergyText.text = "Energy: " + Energy;
        ScoreText.text = "Score: " + Score;
        PiecesText.text = "Pieces: " + Pieces + "/" + TotalPieces;
    }

    public void AddHP(int value)
    {
        HP += value;
        HPText.text = "HP: " + HP;
    }

    public void ReduceHP(int value)
    {
        HP -= value;
        HPText.text = "HP: " + HP;
    }

    public void AddEnergy(int value)
    {
        Energy += value;
        EnergyText.text = "Energy: " + Energy;
    }

    public void ReduceEnergy(int value)
    {
        if (Energy > 0){
            Energy -= value;
        } else {
            ReduceHP(value);
        }
        EnergyText.text = "Energy: " + Energy;
    }

    public void AddScore(int value)
    {
        Score += value;
        ScoreText.text = "Score: " + Score;
    }

    public void AddPieces(int value)
    {
        Pieces += value;
        PiecesText.text = "Pieces: " + Pieces + "/" + TotalPieces;
    }
}
