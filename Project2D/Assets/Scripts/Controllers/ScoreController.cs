using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : AController
{
    /*
     LAWS:
        - All variables will be private if you need access to it, plese do it by Getter and/or Setter
        - All the functions should be used somewhere, except controllers (only on GameController class)
        - Just put the regions that the class will use
        - All variables, regardless of whether they are public or private, you should put [HideInInspector] or [SerializeField]
     */

    #region Variables

    [HideInInspector] private int score = 0;
    [HideInInspector] private int coins = 0;
    [HideInInspector] private int highScore = 0;

    #endregion


    #region Initializers

    //Start
    private void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    #endregion


    #region Getters

    public int GetScore() { return score; }
    public int GetCoinsScore() { return coins; }
    public int GetHighScore() { return highScore; }

    #endregion


    #region Setters or Variable Modifiers

    public void AddCoins(int coinsToAdd)
    {
        coins += coinsToAdd;
        gc.uiController.UpdateCoins();
    }

    public void MultiplyScore()
    {
        score += score;
    }

    #endregion


    //Update
    private void Update()
    {
        if (gc.IsGameRunning() && !gc.player.isDead)
            UpdateScore();
    }


    #region Other

    public void UpdateScore()
    {
        int newScoreValue = Mathf.RoundToInt(gc.player.transform.position.x);
        if (newScoreValue > 0)
        {
            score = newScoreValue;
            gc.uiController.UpdateScore();
        }
    }

    public void UpdateHighScore()
    {
        if (score + coins > highScore)
        {
            highScore = score + coins;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
    }

    #endregion


}
