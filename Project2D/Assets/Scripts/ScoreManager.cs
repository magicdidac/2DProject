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

    private GameController gc;

    [HideInInspector]
    public float Score { get; set; } = 0;
    [HideInInspector]
    public float Coins { get; set; } = 0;
    [HideInInspector]
    public float HighScore { get; set; }
    
    void Start()
    {
        gc = GameController.instance;
        scoreText.text = string.Format("000{0}", Score);
        highScoreText.text = Format(PlayerPrefs.GetFloat("HighScore"));
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

    private string Format(float pScore)
    {
        if (pScore > 999) return string.Format("{0}", pScore);
        if (pScore > 99) return string.Format("0{0}", pScore);
        if (pScore > 9) return string.Format("00{0}", pScore);
        return string.Format("000{0}", pScore);
    }
}
