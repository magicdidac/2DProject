using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    //GameController
    [HideInInspector] private GameController gc;

    //Menus
    [Header("Menus")]
    [SerializeField] public GameObject pauseMenu;
    [SerializeField] public GameObject optionsMenu;
    [SerializeField] public EndGame endGameMenu;
    [SerializeField] public GameObject debugMenu;

    //Score Objects
    [Header("Score Objects")]
    [SerializeField] private Text scoreText = null;
    [SerializeField] private Text coinsText = null;
    [SerializeField] private Text highScoreText = null;
    [SerializeField] private GameObject scorePanel = null;

    //Object references
    [Header("Objects References")]
    [SerializeField] public EnemyIndicator enemyIndicator;
    [SerializeField] private Text fpsText = null;
    [SerializeField] private GameObject fuelPanel = null;
    [SerializeField] private Image fuelArrow = null;
    [SerializeField] private GameObject startMessage = null;

    //Controll Vars
    [HideInInspector] public bool pauseIsActive = false;
    [HideInInspector] public bool fpsAreShowing = false;
    [HideInInspector] private float deltaTime;

    private void Start()
    {
        gc = GameController.instance;
        gc.uiController = this;
        highScoreText.text = Format(gc.scoreController.highScore);
    }

    public void StartGame()
    {
        fuelPanel.SetActive(true);
        scorePanel.SetActive(true);
        startMessage.SetActive(false);
    }

    public void SwitchPause()
    {
        pauseIsActive = !pauseIsActive;
        pauseMenu.SetActive(pauseIsActive);
        Time.timeScale = (pauseIsActive) ? 0 : 1;
    }

    public void SwitchFPS(bool _fpsAreShowing)
    {
        fpsAreShowing = _fpsAreShowing;
        fpsText.gameObject.SetActive(fpsAreShowing);
    }

    public void StopGame()
    {
        
        scorePanel.SetActive(false);
        fuelPanel.SetActive(false);
        fpsText.gameObject.SetActive(false);
        enemyIndicator.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F3))
            debugMenu.SetActive(!debugMenu.activeSelf);

        UpdateFPS();
        if (gc.GetFloor() != 0)
            enemyIndicator.gameObject.SetActive(true);
        else
            enemyIndicator.gameObject.SetActive(false);
    }

    private void UpdateFPS()
    {
        if (!fpsAreShowing)
            return;

        deltaTime += (Time.deltaTime - deltaTime) * .1f;
        float fps = 1 / deltaTime;
        fpsText.text = "FPS: " + Mathf.Ceil(fps);
    }

    public void UpdateFuelBar()
    {
        //fuelBar.fillAmount = gc.player.fuel / gc.player.model.maxFuel;
        
        float rotationZ = (gc.player.fuel / gc.player.model.maxFuel * 120) -60;

        fuelArrow.rectTransform.rotation = Quaternion.Euler(0,0,-rotationZ);
    }

    public void UpdateScore()
    {
        scoreText.text = Format(gc.scoreController.score);
    }

    public void UpdateCoins()
    {
        coinsText.text = Format(gc.scoreController.coins);
    }

    public void UpdateHighScore()
    {
        highScoreText.text = Format(gc.scoreController.highScore);
    }

    private string Format(int ammount)
    {
        if (ammount > 999) return string.Format("{0}", ammount);
        if (ammount > 99) return string.Format("0{0}", ammount);
        if (ammount > 9) return string.Format("00{0}", ammount);
        return string.Format("000{0}", ammount);
    }


    public void ShowEndPanel(bool win)
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
        endGameMenu.score.text = Format(gc.scoreController.score);
        endGameMenu.coins.text = Format(gc.scoreController.coins);
        endGameMenu.gameObject.SetActive(true);
    }

}
