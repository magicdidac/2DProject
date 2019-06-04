using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : AController
{
    /*
     LAWS:
        - All variables will be private if you need access to it, plese do it by Getter and/or Setter
        - All the functions should be used somewhere, except controllers (only on GameController class)
        - Just put the regions that the class will use
        - All variables, regardless of whether they are public or private, you should put [HideInInspector] or [SerializeField]
     */

    #region Variables

    //Menus
    [Header("Menus")]
    [SerializeField] private GameObject pauseMenu = null;
    [SerializeField] private EndGame endGameMenu = null;
    [SerializeField] private GameObject debugMenu = null;

    //Score Objects
    [Header("Score Objects")]
    [SerializeField] private Text scoreText = null;
    [SerializeField] private Text coinsText = null;
    [SerializeField] private Text highScoreText = null;
    [SerializeField] private GameObject scorePanel = null;

    //Object references
    [Header("Objects References")]
    [SerializeField] private EnemyIndicator enemyIndicator = null;
    [SerializeField] private Text fpsText = null;
    [SerializeField] private GameObject fuelPanel = null;
    [SerializeField] private Image fuelArrow = null;
    [SerializeField] private GameObject startMessage = null;

    //Controll Vars
    [HideInInspector] private bool pauseIsActive = false;
    [HideInInspector] private bool fpsAreShowing = false;
    [HideInInspector] private float deltaTime = 0;

    #endregion


    #region Initializers

    //Start
    private void Start()
    {
        highScoreText.text = Format(gc.scoreController.GetHighScore());
    }

    //Other functions that helps to initialize
    public void StartGame()
    {
        fuelPanel.SetActive(true);
        scorePanel.SetActive(true);
        startMessage.SetActive(false);
    }

    #endregion
    

    //Update
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F3))
            debugMenu.SetActive(!debugMenu.activeSelf);

        if (Input.GetKeyDown(KeyCode.Escape))
            SwitchPause();

        UpdateFPS();
        if (gc.GetFloor() != 0)
            enemyIndicator.gameObject.SetActive(true);
        else
            enemyIndicator.gameObject.SetActive(false);
    }


    #region Other

    private void UpdateFPS()
    {
        if (!fpsAreShowing)
            return;

        deltaTime += (Time.deltaTime - deltaTime) * .1f;
        float fps = 1 / deltaTime;
        fpsText.text = "FPS: " + Mathf.Ceil(fps);
    }

    public void UpdateFuel()
    {

        float rotationZ = (gc.player.fuel / gc.player.model.maxFuel * 120) - 60;

        fuelArrow.rectTransform.rotation = Quaternion.Euler(0, 0, -rotationZ);
    }

    public void UpdateScore()
    {
        scoreText.text = Format(gc.scoreController.GetScore())+"m";
    }

    public void UpdateCoins()
    {
        coinsText.text = Format(gc.scoreController.GetCoinsScore());
    }

    public void UpdateHighScore()
    {
        highScoreText.text = "record:"+Format(gc.scoreController.GetHighScore());
    }

    public void SwitchFPS(bool _fpsAreShowing)
    {
        fpsAreShowing = _fpsAreShowing;
        fpsText.gameObject.SetActive(fpsAreShowing);
    }

    public void SwitchPause()
    {
        pauseIsActive = !pauseIsActive;
        pauseMenu.SetActive(pauseIsActive);
        Time.timeScale = (pauseIsActive) ? 0 : 1;
    }

    private string Format(int ammount)
    {
        if (ammount > 999) return string.Format("{0}", ammount);
        if (ammount > 99) return string.Format("0{0}", ammount);
        if (ammount > 9) return string.Format("00{0}", ammount);
        return string.Format("000{0}", ammount);
    }

    private void StopGame()
    {

        scorePanel.SetActive(false);
        fuelPanel.SetActive(false);
        fpsText.gameObject.SetActive(false);
        enemyIndicator.gameObject.SetActive(false);
    }

    public void ActiveEndMenu(bool win)
    {
        StopGame();
        if (win)
        {
            gc.scoreController.MultiplyScore();
            endGameMenu.WinSetUp();
        }
        else
            endGameMenu.LoseSetUp();


        gc.scoreController.UpdateHighScore();
        endGameMenu.score.text = Format(gc.scoreController.GetScore());
        endGameMenu.coins.text = Format(gc.scoreController.GetCoinsScore());
        endGameMenu.gameObject.SetActive(true);
    }

    

    #endregion


}
