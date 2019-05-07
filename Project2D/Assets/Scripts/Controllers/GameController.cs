using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour //This class follows the Singleton Pattern
{
    [SerializeField] private Vector3 stateInfoPosition = Vector3.zero;
    [SerializeField] private int fontSize = 20;

    [HideInInspector] public GameObject HUD;
    [HideInInspector] public GameObject pauseMenu;
    [HideInInspector] public GameObject optionsMenu;    

    [HideInInspector] public static GameController instance = null; //Allows to acces to the game controller from any other script

    [HideInInspector] public MapController mapController; //Map controller reference
    [HideInInspector] public PlayerController player; //Player reference
    [HideInInspector] public EnemyController enemy; //Enemy reference

    [HideInInspector] private int floor = 0;
    [SerializeField] public float minEnemyDistance;
    [SerializeField] public float maxEnemydistance;

    [HideInInspector] public bool pauseActive;
    [HideInInspector] public bool fpsShowed;

    [HideInInspector] public Scene activeScene;

    [HideInInspector] public ScoreManager scoreManager; // Score Manager reference
    [HideInInspector] public EndGame endGame; // End Game menu reference

    [HideInInspector] public float highScore;

    [HideInInspector] public EnemyIndicator enemyIndicator;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);


        DontDestroyOnLoad(gameObject);

        activeScene = SceneManager.GetActiveScene();

        if(activeScene.buildIndex != 0)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyController>();
            mapController = GameObject.FindGameObjectWithTag("MapController").GetComponent<MapController>();
            scoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
            HUD = GameObject.FindGameObjectWithTag("HUD");
            enemyIndicator = Camera.main.transform.GetChild(0).GetComponent<EnemyIndicator>();

            pauseMenu = HUD.transform.GetChild(2).gameObject;
            optionsMenu = HUD.transform.GetChild(3).gameObject;

            endGame = GameObject.FindGameObjectWithTag("Finish").GetComponent<EndGame>();            
        }
    }

    private void Start()
    {
        pauseActive = false;
        fpsShowed = (PlayerPrefs.GetInt("ShowFPS") == 0) ? false : true;

        if(activeScene.buildIndex != 0)
        {
            pauseMenu.SetActive(false);
            optionsMenu.SetActive(false);
        }

        if (Time.timeScale == 0) Time.timeScale = 1;
    }

    private void Update()
    {
        if(activeScene.buildIndex != 0)
        {
            if (getEnemyDistance() < minEnemyDistance && floor == 0 && !player.isDead)
                GameWin(true);
            if (getEnemyDistance() > maxEnemydistance && !player.isDead)
                GameWin(false);
            
            if (!player.isDead) scoreManager.AddScore(player.transform.position.x);

            if (Input.GetKeyDown(KeyCode.Escape) && !optionsMenu.activeSelf)
                Pause();

            if (Input.GetKeyDown(KeyCode.Escape) && optionsMenu.activeSelf)
            {
                pauseMenu.SetActive(true);
                optionsMenu.SetActive(false);
            }
        }
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void GameWin(bool win)
    {
        player.isDead = true;
        enemy.isDead = true;
        player.ChangeState(new PSDead(player));
        endGame.gameObject.SetActive(true);
        endGame.GameWin(win);
        /*highScore = (scoreManager.HighScore > highScore) ? scoreManager.HighScore : highScore;
        if (win) highScore *= 2;*/
    }

    public void setFloor(int p_floor)
    {
        if(p_floor == 1 || p_floor == 0 || p_floor == -1)
            floor = p_floor;
    }

    public int getFloor()
    {
        return floor;
    }

    public void AddCoins(int newScoreValue)
    {
        scoreManager.AddCoins(newScoreValue);
    }

    //lo he pasado a public para usarlo desde EndGame.cs que controla las escenas
    public void GameController_completed(AsyncOperation obj)
    {
        instance.highScore = (instance.scoreManager.HighScore > instance.highScore) ? instance.scoreManager.HighScore : instance.highScore;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyController>();
        mapController = GameObject.FindGameObjectWithTag("MapController").GetComponent<MapController>();
        scoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
        endGame = GameObject.FindGameObjectWithTag("Finish").GetComponent<EndGame>();
    }

    public void ConsumeCombustible()
    {
        if (player.combustible > 0)
        {
            player.combustible -= Time.deltaTime;
            GameObject.FindWithTag("HUD").GetComponent<HUD>().ChangeFuelBar(player.combustible);
        }
    }

    public void GetCombustible(float value)
    { 
        if (player.combustible + value < player.model.maxCombustible) player.combustible += value;
        else player.combustible = player.model.maxCombustible;
        GameObject.FindWithTag("HUD").GetComponent<HUD>().ChangeFuelBar(player.combustible);
    }
    
    public float getEnemyDistance()
    {
        return (Mathf.Round((enemy.transform.position.x - player.transform.position.x)*100)/100);
    }

    public void Pause()
    {
        pauseActive = !pauseActive;
        pauseMenu.SetActive(pauseActive);
        //GameManager._gameManager.isPaused = active;
        Time.timeScale = (pauseActive) ? 0 : 1;
    }

    public void ShowFPS(bool show)
    {
        fpsShowed = show;
        if (GameObject.FindWithTag("HUD") != null)
        {
            GameObject.FindWithTag("HUD").GetComponent<HUD>().fpsText.SetActive(show);
        }
    }

    public void Exit()
    {
        Application.Quit();
    }

    private void OnDrawGizmos()
    {
        try
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyController>();
            //enemyDistance = Mathf.Abs(player.transform.position.x) + Mathf.Abs(enemy.transform.position.x);
            drawState();
        }
        catch { }
    }

    private void drawState()
    {
        if (player.currentState != null)
        {
            GUIStyle style = new GUIStyle();
            style.normal.textColor = Color.red;
            style.fontSize = fontSize;
            style.alignment = TextAnchor.MiddleLeft;
            Handles.Label(stateInfoPosition + Camera.main.transform.position, "Player state: " + player.currentState + "\nEnemy state: " + enemy.currentState + "\nFloor: " + floor+ "\nEnemy: "+getEnemyDistance(), style);
        }
    }
    
    private void DeletePlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    private void InitializePlayerPrefs()
    {
        PlayerPrefs.SetFloat("MasterVolume", 0.6f);
        PlayerPrefs.SetInt("MusicActive", 1); //0 = OFF, 1 = ON
        PlayerPrefs.SetInt("ShowFPS", 0); //0 = OFF, 1 = ON
        PlayerPrefs.SetFloat("Brightness", 0.0f);
        PlayerPrefs.SetFloat("HighScore", 0.0f);
    }


    //DATABASE
    /*
    public void UploadScore()
    {
        StartCoroutine(Upload());
    }

    IEnumerator Upload()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", username);
        form.AddField("distance_score", );
        form.AddField("coins_score", );
        WWW www = new WWW("http://www.magicdvstudio.com/RobotRunner/uploadScore.php", form);
        yield return www;
        Debug.Log(www.text);
    }*/
}
