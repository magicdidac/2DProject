using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    public GameObject totalScoreObject;
    public GameObject totalCoinsObject;
    public GameObject highScoreObject;

    private Image panel;

    private float score;
    private float coins;
    private float highScore;

    private GameController gc;

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

        StartCoroutine(AnimateScore(totalScoreObject.GetComponent<Text>(), score));
        StartCoroutine(AnimateScore(totalCoinsObject.GetComponent<Text>(), coins));

        if (win)
        {
            panel.transform.GetChild(0).GetComponent<Text>().text = "WINNER";
            highScore = (coins + score) * 2;
        }
        else
        {
            panel.GetComponent<Image>().color = new Color(r:255f, g:0f, b:0f, a:.100f);
            panel.transform.GetChild(0).GetComponent<Text>().text = "LOSER";
            highScore = coins + score;
        }
        StartCoroutine(AnimateScore(highScoreObject.GetComponent<Text>(), highScore));
    }

    IEnumerator AnimateScore(Text t_score, float f_score)
    {
        t_score.text = "0";
        int score = 0;
        while(score < f_score)
        {
            score++;
            t_score.text = score.ToString();
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
