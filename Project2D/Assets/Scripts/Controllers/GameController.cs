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
    [SerializeField] private float minEnemyDistance = 1;
    [SerializeField] private float maxEnemyDistance = 0f;
    [HideInInspector] private float playerEnemyDistance;

    [HideInInspector] public bool isAutomateStart { get; set; } = false;
    [HideInInspector] private bool allowInstantiate;

    [Header("Speed in Game")]
    [SerializeField] private float speedMultiplier = 1;
    [HideInInspector] private float startSpeedMultiplier;
    [SerializeField] private float increaseSpeedMultiplier = .05f;
    [SerializeField] private float maxSpeedMultiplier = 1.5f;

    [Header("Dificult")]
    [SerializeField] private DificultController dc = null;


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

        startSpeedMultiplier = speedMultiplier;

        InitializePlayerPrefs();
    }

    public void StartGame()
    {
        
        if (!allowInstantiate)
            return;

        uiController.StartGame();
        mapController.StartGame();

        //Spawn player and enemy
        playerSpawnPoint = GameObject.FindGameObjectWithTag("Respawn").transform;
        Invoke("SpawnPlayer", 1);
        allowInstantiate = false;

        AudioListener.pause = false; // new
        audioController.PlaySound("introExplosion");
        audioController.StopAllMusic();
        audioController.PlayMusic("gameSong");
    }

    private void SpawnPlayer()
    {
        isGameRunning = true;
        player = Instantiate(playerObj, playerSpawnPoint.position, Quaternion.identity).GetComponent<PlayerController>();
        enemy = Instantiate(enemyObj, GetEnemySpawnPosition(), Quaternion.identity).GetComponent<EnemyController>();
        playerEnemyDistance = Mathf.Round((enemy.transform.position.x - player.transform.position.x) * 100) / 100; //new
    }

    public void restartVariables()
    {
        floor = 0;
        speedMultiplier = startSpeedMultiplier;
        isGameRunning = false;
        allowInstantiate = true;
        if (isAutomateStart) StartGame();
    }

    


    #endregion


    #region Getters

    public int GetFloor() { return floor; }

    public float GetEnemyDistance()
    {
        if (enemy == null || player == null) return playerEnemyDistance;

        playerEnemyDistance = Mathf.Round((enemy.transform.position.x - player.transform.position.x) * 100) / 100;
        return playerEnemyDistance;
    }

    public float GetSkillMultiplier()
    {
        return dc.GetDificulty().GetSpeedMultiplier();
    }

    public PlayerDificulty.Dificulty GetCurrentDificulty()
    {


        return dc.GetDificulty().GetName();
    }

    public float GetVelocityMultiplier() { return speedMultiplier; }

    public float GetVelocity(float vo)
    {
        vo *= GetSkillMultiplier();

        return vo * speedMultiplier;
    }

    private Vector3 GetEnemySpawnPosition()
    {
        return playerSpawnPoint.position + (Vector3.right * enemyPositionOfset);
    }

    public float GetMinimumEnemyDistance() { return minEnemyDistance; }

    public bool IsGameRunning() { return isGameRunning; }

    #endregion


    #region Setters or Variable Modifiers

    public void SetFloor(int floor) { this.floor = floor; }

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
        if (speedMultiplier < maxSpeedMultiplier)
            speedMultiplier += increaseSpeedMultiplier;
        else
            speedMultiplier = maxSpeedMultiplier;
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
        int r = PlayerPrefs.GetInt("PlayerSkill", 30);
        r -= 30 - mapController.GetChunksCounter();

        if (r > 0)
            PlayerPrefs.SetInt("PlayerSkill", r);
        else
            PlayerPrefs.SetInt("PlayerSkill", 0);

    }

    #endregion


    private void Update()
    {
        if (!isGameRunning && Input.GetKeyDown(KeyCode.Space))
        {
            //Start Game
            StartGame();
        }

        if (isGameRunning)
        {
            if (GetEnemyDistance() < minEnemyDistance && floor == 0 && !player.isDead) //Check
                GameWin(true, PlayerController.DeathType.CatchEnemy);

            if (GetEnemyDistance() > maxEnemyDistance && !player.isDead)
                GameWin(false, PlayerController.DeathType.EnemyRunAway);
        }
    }


    #region Other

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void Exit()
    {
        Debug.Log("Quit Application");
        Application.Quit();
    }

    public void GameWin(bool win, PlayerController.DeathType dt)
    {
        if (player.isDead)
            return;

        if(!win)
            DecreasePlayerSkill();

        player.Kill(dt);
        
        if (win)
            scoreController.MultiplyScore();

        scoreController.UpdateHighScore();

        if (win)
            Invoke("ActiveEndMenuWin", 2f);
        else
            Invoke("ActiveEndMenuLose", 2f);

    }

    private void ActiveEndMenuWin()
    {
        uiController.ActiveEndMenu(true);
    }
    private void ActiveEndMenuLose()
    {
        uiController.ActiveEndMenu(false);
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
