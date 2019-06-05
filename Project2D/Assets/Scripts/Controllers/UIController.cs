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
    [SerializeField] private Text fpsText = null;
    [SerializeField] private GameObject fuelPanel = null;
    [SerializeField] private Image fuelArrow = null;
    [SerializeField] private GameObject startMessage = null;
    [SerializeField] private Text turboText = null;
    [SerializeField] private Color turboOffColor = Color.black;
    [SerializeField] private Color turboOnColor = Color.black;
    [SerializeField] private Text enemyDistanceText = null;
    [SerializeField] private GameObject enemyDistancePanel = null;

    //Controll Vars
    [HideInInspector] private bool pauseIsActive = false;

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
        enemyDistancePanel.SetActive(true);
        startMessage.SetActive(false);

        UpdateHighScore();

    }

    #endregion
    

    //Update
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F3))
            debugMenu.SetActive(!debugMenu.activeSelf);

        if (Input.GetKeyDown(KeyCode.Escape))
            SwitchPause();

        UpdateEnemyDistance();
        UpdateFuel();
    }


    #region Other


    public void SetOnTurboText()
    {
        turboText.color = turboOnColor;
    }

    public void SetOffTurboText()
    {
        turboText.color = turboOffColor;
    }

    public void UpdateFuel()
    {
        if (gc == null)
            gc = GameController.instance;

        if (gc.player == null || gc.enemy == null)
            return;

        float rotationZ = (gc.player.fuel / gc.player.model.maxFuel * 170) - 85;

        fuelArrow.rectTransform.rotation = Quaternion.Lerp(fuelArrow.rectTransform.rotation, Quaternion.Euler(0, 0, -rotationZ), .1f);


    }

    public void UpdateScore()
    {
        scoreText.text = Format(gc.scoreController.GetScore());
    }

    public void UpdateCoins()
    {
        coinsText.text = Format(gc.scoreController.GetCoinsScore());
    }

    public void UpdateHighScore()
    {
        highScoreText.text = Format(gc.scoreController.GetHighScore());
    }

    public void UpdateEnemyDistance()
    {
        enemyDistanceText.text = Format2(gc.GetEnemyDistance());
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

    private string Format2(float ammount)
    {
        return string.Format("{0}m", Mathf.Round(ammount*10)/10);
    }

    private void StopGame()
    {
        scorePanel.SetActive(false);
        fuelPanel.SetActive(false);
        enemyDistancePanel.SetActive(false);
        fpsText.gameObject.SetActive(false);
    }

    public void ActiveEndMenu(bool win)
    {
        StopGame();
        if (win)
            endGameMenu.WinSetUp();
        else
            endGameMenu.LoseSetUp();

        endGameMenu.gameObject.SetActive(true);
    }

    

    #endregion


}
