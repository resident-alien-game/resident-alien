using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusManagement : MonoBehaviour
{
    public static StatusManagement instance;
    public TMP_Text ScoreText;
    public GameObject gameOverPanel;

    int HP = 5;
    int Energy = 5;
    int Score = 0;
    int Pieces = 0;
    int TotalPieces;
    int currentHP;
    int currentEnergy;

    public BarControl HPBar;
    public BarControl EnergyBar;
    public PieceManage pieceManage;


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
        ScoreText.text = "Score: " + Score;
        Time.timeScale = 1f;
        currentEnergy = Energy;
        currentHP = HP;
        HPBar.SetMaxValue(HP);
        EnergyBar.SetMaxValue(Energy);
        pieceManage.setMaxPiece(TotalPieces);
    }

    public void AddHP(int value)
    {
        currentHP += value;

        HPBar.SetValue(currentHP);
    }

    public void ReduceHP(int value)
    {
        currentHP -= value;

        HPBar.SetValue(currentHP);
        if (IsGameOver())
        {
            GameOver();
        }
    }

    public void AddEnergy(int value)
    {
        currentEnergy += value;

        EnergyBar.SetValue(currentEnergy);
    }

    public void ReduceEnergy(int value)
    {

        currentEnergy -= value;

        EnergyBar.SetValue(currentEnergy);
    }

    public void AddScore(int value)
    {
        Score += value;
        ScoreText.text = "Score: " + Score;
    }

    public void AddPieces(int value)
    {
        Pieces += value;
        pieceManage.setCheckMark(Pieces);
    }

    public void ReduceTotalPieces(int value)
    {
        TotalPieces -= value;
        pieceManage.setMissingPiece(TotalPieces);
        if (IsGameOver())
        {
            GameOver();
        }
    }

    public bool CanUseSpell()
    {
        return currentEnergy > 0;
    }

    public bool CanEatFood()
    {
        return currentHP < 5;
    }

    public bool CanDrink()
    {
        return currentEnergy < 5;
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