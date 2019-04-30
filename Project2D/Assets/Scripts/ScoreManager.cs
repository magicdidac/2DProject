using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    //public GameObject panel;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI highScoreText;

    /*public Text totalScoreMeters;
    public Text totalScoreCoins;

    public Text highScoreText;
    public Text totalScoreText;*/

    [HideInInspector]
    public float Score { get; set; } = 0;
    [HideInInspector]
    public float Coins { get; set; } = 0;

    [HideInInspector]
    public float HighScore { get; set; } = 0;
    
    void Start()
    {
        scoreText.text = string.Format("000{0}", Score);
        //highScoreText.text = Format(GameController.instance.highScore);
        //panel.SetActive(false);
    }

    public void AddScore(float newScoreValue)
    {
        if (newScoreValue > 0)
        {
            Score = Mathf.Round(newScoreValue);
            scoreText.text = Format(Score);
        }
    }

    public void AddCoins(int newScoreValue)
    {
        Coins += newScoreValue;
        coinsText.text = Format(Coins);
    }

    public void GameOver(bool win)
    {
        HighScore = Score;
        /*totalScoreCoins.text += coins.ToString();
        totalScoreMeters.text += score.ToString();

        if (win)
        {
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
        }*/
    }

    private string Format(float pScore)
    {
        if (pScore > 999) return string.Format("{0}", pScore);
        if (pScore > 99) return string.Format("0{0}", pScore);
        if (pScore > 9) return string.Format("00{0}", pScore);
        return string.Format("000{0}", pScore);
    }
}
