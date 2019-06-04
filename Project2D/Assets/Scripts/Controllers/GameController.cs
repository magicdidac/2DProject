using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour //This class follows the Singleton Pattern
{

    #region Variables

    //Instance
    [HideInInspector] public static GameController instance = null;


    //Prefabs
    [Header("Prefabs")]
    [HideInInspector] private Transform playerSpawnPoint = null;
    [SerializeField] private GameObject playerObj = null;
    [SerializeField] private GameObject enemyObj = null;
    [SerializeField] private float enemyPositionOfset = 0;

    //Controllers
    [HideInInspector] public MapController mapController;
    [HideInInspector] public AudioController audioController;
    [HideInInspector] public ScoreController scoreController;
    [HideInInspector] public UIController uiController;

    [HideInInspector] public PlayerController player;
    [HideInInspector] public EnemyController enemy;

    //Control Vars
    [Header("Control Vars")]
    [HideInInspector] private bool isGameRunning = false;
    [HideInInspector] private int floor = 0;
    [HideInInspector] private float velocityMultiplier = .9f;
    [SerializeField] private float minEnemyDistance = 1;
    [SerializeField] private float maxEnemydistance = 9;

    #endregion


    #region Initializers

    private void Awake()
    {
        //Singleton Pattern
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject); //Dont destroy when change the scene

        InitializePlayerPrefs();

        //TODO: Play chill Music

    }

    public void StartGame()
    {
        if (isGameRunning)
            return;

        isGameRunning = true;

        uiController.StartGame();

        //Spawn player and enemy
        playerSpawnPoint = GameObject.FindGameObjectWithTag("Respawn").transform;
        player = Instantiate(playerObj, playerSpawnPoint.position, Quaternion.identity).GetComponent<PlayerController>();
        enemy = Instantiate(enemyObj, GetEnemySpawnPosition(), Quaternion.identity).GetComponent<EnemyController>();
    }

    public void restartVariables()
    {
        floor = 0;
        velocityMultiplier = .9f;
        isGameRunning = false;
    }


    #endregion


    #region Getters

    public int GetFloor() { return floor; }

    public float GetEnemyDistance()
    {
        return (Mathf.Round((enemy.transform.position.x - player.transform.position.x) * 100) / 100);
    }

    public float GetSkillMultiplier()
    {
        int playerSkill = PlayerPrefs.GetInt("PlayerSkill", 30);

        if (playerSkill <= 35)
            return .8f;
        else if (playerSkill <= 60)
            return 1;
        else if (playerSkill <= 75)
            return 1.2f;
        else if (playerSkill <= 95)
            return 1.3f;
        else
            return 1.5f;
    }

    public float GetVelocityMultiplier() { return velocityMultiplier; }

    public float GetVelocity(float vo)
    {
        vo *= GetSkillMultiplier();

        return vo * velocityMultiplier;
    }

    private Vector3 GetEnemySpawnPosition()
    {
        return playerSpawnPoint.position + (Vector3.right * enemyPositionOfset);
    }

    public float GetMinimumEnemyDistance() { return minEnemyDistance; }

    public bool IsGameRunning() { return isGameRunning; }

    #endregion


    #region Setters or Variable Modifiers

    public void AddController(AController c)
    {
        if (c is MapController)
            mapController = (MapController)c;
        else if (c is AudioController)
            audioController = (AudioController)c;
        else if (c is ScoreController)
            scoreController = (ScoreController)c;
        else if (c is UIController)
            uiController = (UIController)c;

    }

    public void AddFloor()
    {
        if (floor < 1)
            floor++;
    }

    public void SubtractFloor()
    {
        if (floor > -1)
            floor--;
    }

    public void IncreaseVelocityMultiplier()
    {
        if (velocityMultiplier < 1.75f)
            velocityMultiplier += .1f;
        else
            velocityMultiplier = 1.75f;
    }

    public void IncreasePlayerSkill()
    {
        int r = (int)Mathf.Floor(Mathf.Log((mapController.GetChunksCounter() * .5f), 1.5f));
        if (r < 0) r = 0;
        r += PlayerPrefs.GetInt("PlayerSkill", 30);
        if (r <= 100)
            PlayerPrefs.SetInt("PlayerSkill", r);
        else
            PlayerPrefs.SetInt("PlayerSkill", 100);
    }

    public void DecreasePlayerSkill()
    {
        int r = 30 - mapController.GetChunksCounter();
        r += PlayerPrefs.GetInt("PlayerSkill", 30);

        if (r > 0)
            PlayerPrefs.SetInt("PlayerSkill", r);
        else
            PlayerPrefs.SetInt("PlayerSkill", 0);

    }

    #endregion


    private void Update()
    {
        if (!isGameRunning && Input.GetKeyDown(KeyCode.Space)) //Start Game
            StartGame();

        if (isGameRunning)
        {
            if (GetEnemyDistance() < minEnemyDistance && floor == 0 && !player.isDead) //Check
                GameWin(true);

            if (GetEnemyDistance() > maxEnemydistance && player.isDead)
                GameWin(false);
        }
    }


    #region Other

    public void LoadScene(int index) { SceneManager.LoadScene(index); }

    public void Exit() { Application.Quit(); }

    public void GameWin(bool win)
    {
        if (player.isDead)
            return;

        uiController.ShowEndPanel(win);
        AudioController._audioManager.StopAllMusic();

        player.Kill();
        if (win)
            scoreController.MultiplyScore();


        scoreController.UpdateHighScore();
    }

    #endregion


    #region PlayerPrefs

    private void DeletePlayerPrefs() { PlayerPrefs.DeleteAll(); }

    private void InitializePlayerPrefs()
    {
        PlayerPrefs.GetFloat("MasterVolume", .6f);
        PlayerPrefs.GetInt("MusicActive", 1); //0 = OFF - 1 = ON
        PlayerPrefs.GetInt("ShowFPS", 0); //0 = OFF - 1 = ON
        PlayerPrefs.GetFloat("Brightness", .5f);
        PlayerPrefs.GetInt("HighScore", 0);
        PlayerPrefs.GetInt("PlayerSkill", 30);
    }

    #endregion


    #region Gizmos

    private void OnDrawGizmos()
    {
        DrawEnemySpawnPoint();
        DrawMinimalDistance();
    }

    private void DrawEnemySpawnPoint()
    {
        if (playerSpawnPoint != null)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawSphere(GetEnemySpawnPosition(), .25f);
        }
        else
            playerSpawnPoint = GameObject.FindGameObjectWithTag("Respawn").transform;
    }

    private void DrawMinimalDistance()
    {
        if (player == null) //Exit if player doesn't exists on current scene
            return;

        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(new Vector3(player.transform.position.x + minEnemyDistance, -.5f), new Vector3(player.transform.position.x + minEnemyDistance, 1));
               
    }

    #endregion



    #region Database

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

    #endregion

}
