﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{

    private Image panel;

    private float score;
    private float coins;
    private float highScore;

    private GameController gc;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI coinsText;

    private void Start()
    {
        panel = GetComponent<Image>();
        gc = GameController.instance;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
           Retry();
        }
    }

    public void GameWin(bool win)
    {
        score = gc.scoreManager.Score;
        coins = gc.scoreManager.Coins;
        gc.scoreManager.coinsText.gameObject.SetActive(false);
        gc.scoreManager.scoreText.gameObject.SetActive(false);
        gc.scoreManager.gameObject.SetActive(false);

        scoreText.text = score.ToString();
        coinsText.text = coins.ToString();

        if (win)
        {
            panel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "WINNER";
            highScore = (coins + score) * 2;
        }
        else
        {
            panel.GetComponent<Image>().color = new Color(r:255f, g:0f, b:0f, a:.100f);
            panel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "LOSER";
            highScore = coins + score;
        }

        Invoke("InvokeIncreaseScore", 1f);
    }

    private void InvokeIncreaseScore()
    {
        StartCoroutine(DicreaseCoins(coinsText, coins));
        StartCoroutine(IncreaseScore(scoreText, highScore));
    }

    IEnumerator IncreaseScore(TextMeshProUGUI t_score, float f_score)
    {
        while(score < f_score)
        {
            score++;
            t_score.text = score.ToString();
            yield return new WaitForSeconds(.005f);
        }
    }

    IEnumerator DicreaseCoins(TextMeshProUGUI t_score, float f_score)
    {
        while (f_score > 0)
        {
            f_score--;
            t_score.text = f_score.ToString();
            yield return new WaitForSeconds(.005f);
        }
    }

    public void Retry()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex).completed += gc.GameController_completed;
        gc.setFloor(0);
    }

    public void Menu(string name)
    {
        SceneManager.LoadScene(name);
    }
}
