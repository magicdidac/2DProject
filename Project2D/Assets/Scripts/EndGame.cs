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
    [SerializeField] private Image winLosePanel = null;
    [SerializeField] private Sprite winSprite = null;
    [SerializeField] private Sprite loseSprite = null;
    [SerializeField] private Image basePanel = null;
    [SerializeField] private Sprite winBase = null;
    [SerializeField] private Sprite loseBase = null;


    private int maxScore;
    

    private void OnEnable()
    {
        gc = GameController.instance;

        coinsText.text = "" + gc.scoreController.GetCoinsScore();
        scoreText.text = "" + gc.scoreController.GetScore();

        gc.audioController.StopAllMusic(); //hacer mejor un FadeOut
        gc.audioController.StopAllSounds();
        AudioListener.pause = true;  //es para parar el sonido de los instantKill
        gc.isAutomateStart = false;

        Destroy(gc.enemy.gameObject);

        Invoke("InvokeIncreaseScore", 2.5f);
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
            RetrySpace();

    }

    public void WinSetUp()
    {
        basePanel.sprite = winBase;
        winLosePanel.sprite = winSprite;
    }

    public void LoseSetUp()
    {
        basePanel.sprite = loseBase;
        winLosePanel.sprite = loseSprite;
    }

    private void InvokeIncreaseScore()
    {
        

        StartCoroutine(DicreaseCoins(coinsText, gc.scoreController.GetCoinsScore()));
        StartCoroutine(IncreaseScore(scoreText, gc.scoreController.GetScore()+gc.scoreController.GetCoinsScore()));
    }

    IEnumerator IncreaseScore(Text t_score, int f_score)
    {
        int initialScore = gc.scoreController.GetScore();
        gc.audioController.PlaySound("scoreCounting");

        while (initialScore < f_score)
        {  
            initialScore += 5;
            if (initialScore > f_score)
                initialScore = f_score;

            t_score.text = initialScore+"";
            yield return new WaitForSeconds(.02f);
        }
        gc.audioController.StopSound("scoreCounting");
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
        gc.SetFloor(0);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);//.completed += gc.restartVariables();
        AudioListener.pause = false;
        //gc.restartVariables();
    }

    private void RetrySpace()
    {
        gc.isAutomateStart = true;
        Retry();
    }
}
