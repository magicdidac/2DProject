using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    public GameObject panel;

    public Text scoreText;
    public Text coinsText;

    public Text totalScoreMeters;
    public Text totalScoreCoins;

    public Text highScoreText;
    public Text totalScoreText;

    private float score = 0;
    private float coins = 0;

    [HideInInspector]
    public float highScore = 0;

    private bool scoreIncreasing = true; //false when the player is dead

    void Start()
    {
        scoreText.text = string.Format("000{0}", score);
        highScoreText.text = string.Format("000{0}", score);
        panel.SetActive(false);
    }

    public void AddScore(float newScoreValue)
    {
        if (newScoreValue > 0)
        {
            score = Mathf.Round(newScoreValue);
            scoreText.text = Format(score);
        }
    }

    public void AddCoins(int newScoreValue)
    {
        coins += newScoreValue;
        coinsText.text = Format(coins);
    }

    public void CalculateFinalScore(bool win)
    {

        highScore = score;
        totalScoreCoins.text += coins.ToString();
        totalScoreMeters.text += score.ToString();

        if (win)
        {
            panel.GetComponent<Image>().color = new Color(0, 255, 34, 196);
            panel.transform.GetChild(0).GetComponent<Text>().text = "WINNER";
            highScore = (coins + score) * 2;
            totalScoreText.text = highScore.ToString();
        }
        else
        {
            panel.GetComponent<Image>().color = new Color(255, 0, 0, 196);
            panel.transform.GetChild(0).GetComponent<Text>().text = "LOSER";
            highScore = coins + score;
            totalScoreText.text = highScore.ToString();
        }
    }

    private string Format(float pScore)
    {
        if (pScore > 999) return string.Format("{0}", pScore);
        if (pScore > 99) return string.Format("0{0}", pScore);
        if (pScore > 9) return string.Format("00{0}", pScore);
        return string.Format("000{0}", pScore);
    }
}
