using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    public Text scoreText;
    public Text highScoreText;

    private float score = 0;
    private float highScore = 0;

    private bool scoreIncreasing = true; //false when the player is dead
    
    void Start()
    {
        scoreText.text = string.Format("000{0}", score);
        highScoreText.text = string.Format("000{0}", score);
    }

    void Update()
    {
        //if (scoreIncreasing) score += pointsPerSecond * Time.deltaTime;
        //UpdateScore();
        //if (score > highScore) highScore = score;
    }

    public void AddScore(float newScoreValue)
    {
        if (newScoreValue > 0) scoreText.text = Format(Mathf.Round(newScoreValue));
    }

    private void UpdateScore()
    {
        scoreText.text = Format(Mathf.Round(score));
    }

    private void UpdateHighScore()
    {
        //scoreText.text = Mathf.Round(score).ToString();
    }

    private string Format(float pScore)
    {
        if (pScore > 99) return string.Format("0{0}", pScore);
        if (pScore > 9) return string.Format("00{0}", pScore);
        return string.Format("000{0}", pScore);
    }
}
