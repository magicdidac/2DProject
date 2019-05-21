using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    private GameController gc;

    [SerializeField] private Image panel = null;
    [SerializeField] private TextMeshProUGUI title = null;
    [SerializeField] public TextMeshProUGUI score = null;
    [SerializeField] public TextMeshProUGUI coins = null;


    private int maxScore;
    

    private void Start()
    {
        gc = GameController.instance;
    }

    private void Update()
    {
    }

    public void WinSetUp()
    {
        title.text = "WIN";

    }

    public void LoseSetUp()
    {
        title.text = "LOSE";
        panel.color = new Color(r: 255, g: 0, b: 0, a: .100f);
    }

    private void InvokeIncreaseScore()
    {
        //StartCoroutine(DicreaseCoins(coinsText, coins));
        //StartCoroutine(IncreaseScore(scoreText, highScore));
    }

    /*IEnumerator IncreaseScore(TextMeshProUGUI t_score, float f_score)
    {
        while(score < f_score)
        {
            score+=5;
            if (score > f_score)
                score = f_score;
            t_score.text = score.ToString();
            yield return new WaitForSeconds(.02f);
        }
    }

    IEnumerator DicreaseCoins(TextMeshProUGUI t_score, float f_score)
    {
        while (f_score > 0)
        {
            f_score-=5;
            if (f_score < 0)
                f_score = 0;
            t_score.text = f_score.ToString();
            yield return new WaitForSeconds(.02f);
        }
    }*/

    public void Retry()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);//.completed += gc.restartVariables();
        gc.restartVariables();
    }
}
