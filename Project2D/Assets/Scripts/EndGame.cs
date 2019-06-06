using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    [HideInInspector] private GameController gc;

    [SerializeField] private Text scoreText = null;
    [SerializeField] private Text coinsText = null;


    private int maxScore;
    

    private void OnEnable()
    {
        gc = GameController.instance;

        coinsText.text = "" + gc.scoreController.GetCoinsScore();
        scoreText.text = "" + gc.scoreController.GetScore();

        Invoke("InvokeIncreaseScore", 1f);
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
            Retry();

    }

    public void WinSetUp()
    {
        Debug.Log("Win");
    }

    public void LoseSetUp()
    {
        Debug.Log("Lose");
    }

    private void InvokeIncreaseScore()
    {
        

        StartCoroutine(DicreaseCoins(coinsText, gc.scoreController.GetCoinsScore()));
        StartCoroutine(IncreaseScore(scoreText, gc.scoreController.GetScore()+gc.scoreController.GetCoinsScore()));
    }

    IEnumerator IncreaseScore(Text t_score, int f_score)
    {
        int initialScore = gc.scoreController.GetScore();

        while(initialScore < f_score)
        {
            initialScore += 5;
            if (initialScore > f_score)
                initialScore = f_score;

            t_score.text = initialScore+"";
            yield return new WaitForSeconds(.02f);
        }
    }

    IEnumerator DicreaseCoins(Text t_score, float f_score)
    {
        while (f_score > 0)
        {
            f_score-=5;
            if (f_score < 0)
                f_score = 0;
            t_score.text = f_score.ToString();
            yield return new WaitForSeconds(.02f);
        }
    }

    public void Retry()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);//.completed += gc.restartVariables();
        gc.restartVariables();
    }
}
