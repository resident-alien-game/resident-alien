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
    public GameObject gameOverPanel;

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
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
        TotalPieces = GameObject.FindGameObjectsWithTag("Piece").Length;
        HPText.text = "HP: " + HP;
        EnergyText.text = "Energy: " + Energy;
        ScoreText.text = "Score: " + Score;
        PiecesText.text = "Pieces: " + Pieces + "/" + TotalPieces;
        Time.timeScale = 1f;
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
        if (IsGameOver())
        {
            GameOver();
        }
    }

    public void AddEnergy(int value)
    {
        Energy += value;
        EnergyText.text = "Energy: " + Energy;
    }

    public void ReduceEnergy(int value)
    {

        Energy -= value;
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

    public void ReduceTotalPieces(int value)
    {
        TotalPieces -= value;
        PiecesText.text = "Pieces: " + Pieces + "/" + TotalPieces;
        if (IsGameOver())
        {
            GameOver();
        }
    }

    public bool CanUseSpell()
    {
        return Energy > 0;
    }

    public bool CanEatFood()
    {
        return HP < 5;
    }

    public bool CanDrink()
    {
        return Energy < 5;
    }

    public bool IsGameOver()
    {
        return HP == 0 || TotalPieces == 3;
    }

    public void GameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}