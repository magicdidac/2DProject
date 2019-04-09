using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    public Text scoreText;
    public Text highScoreText;

    private float score;
    private float highScore;

    private float pointsPerSecond = 5;
    private bool scoreIncreasing; //false when the player is dead
    
    void Start()
    {
        scoreIncreasing = true;
    }
    
    void Update()
    {
        if (scoreIncreasing) score += pointsPerSecond * Time.deltaTime;
        UpdateScore();
        //if (score > highScore) highScore = score;
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
    }

    private void UpdateScore()
    {
        scoreText.text = "Score: " + Mathf.Round(score);
    }

    private void UpdateHighScore()
    {
        scoreText.text = "HighScore: " + Mathf.Round(score);
    }
}
