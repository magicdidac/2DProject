using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    private GameController gc;

    [HideInInspector]
    public int score { get; set; } = 0;
    [HideInInspector]
    public int coins { get; set; } = 0;
    [HideInInspector]
    public int highScore { get; set; }
    
    private void Awake()
    {
        gc = GameController.instance;
        gc.scoreController = this;

        highScore = PlayerPrefs.GetInt("HighScore",0);
    }

    private void Update()
    {
        if (!gc.player.isDead)
            UpdateScore();
    }

    public void UpdateScore()
    {
        int newScoreValue = Mathf.RoundToInt(gc.player.transform.position.x);
        if (newScoreValue > 0)
        {
            score = newScoreValue;
            gc.uiController.UpdateScore();
        }
    }

    public void AddCoins(int coinsToAdd)
    {
        coins += coinsToAdd;
        gc.uiController.UpdateCoins();
    }

    public void MultiplyScore()
    {
        score += score;
    }

    public void UpdateHighScore()
    {
        if (score+coins > highScore)
        {
            highScore = score+coins;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
    }

    public void RestartScore()
    {
        score = 0;
        coins = 0;
    }
    
}
