using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour //This class follows the Singleton Pattern
{
    //Instance
    [HideInInspector] public static GameController instance = null;
    

    //Controllers
    [HideInInspector] public MapController mapController;
    [HideInInspector] public AudioController audioController;
    [HideInInspector] public ScoreController scoreController;
    [HideInInspector] public UIController uiController;

    [HideInInspector] public PlayerController player;
    [HideInInspector] public EnemyController enemy;

    //Control Vars
    [Header("Control Vars")]
    [HideInInspector] private int floor = 0;
    [HideInInspector] private float velocityMultiplier = .9f;
    [SerializeField] public readonly float minEnemyDistance;
    [SerializeField] public readonly float maxEnemydistance;
    

    public void restartVariables()
    {
        floor = 0;
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        //AudioController._audioManager.PlayMusic("playGame"); no cal si ja es un PlayOnAwake
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (Time.timeScale == 0) Time.timeScale = 1;
    }

    private void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex != 0)
        {
            if (getEnemyDistance() < minEnemyDistance && floor == 0 && !player.isDead)
                GameWin(true);

            if (getEnemyDistance() > maxEnemydistance && player.isDead)
                GameWin(false);
        }
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

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

    public int GetFloor()
    {
        return floor;
    }

    public float GetVelocityMultiplier()
    {
        return velocityMultiplier;
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
        int r = (int) Mathf.Floor(Mathf.Log((mapController.chunksCounter*.5f),1.5f));
        if (r < 0) r = 0;
        r += PlayerPrefs.GetInt("PlayerSkill", 30);
        if (r <= 100)
            PlayerPrefs.SetInt("PlayerSkill", r);
        else
            PlayerPrefs.SetInt("PlayerSkill", 100);
    }

    public void DecreasePlayerSkill()
    {
        int r = 30 - mapController.chunksCounter;
        r += PlayerPrefs.GetInt("PlayerSkill", 30);

        if (r > 0)
            PlayerPrefs.SetInt("PlayerSkill", r);
        else
            PlayerPrefs.SetInt("PlayerSkill", 0);

    }

    public float CalculateVelocity(float vo)
    {
        vo *= GetSkillMultiplier();

        return vo * velocityMultiplier;
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

    //pospuesto
    public float getEnemyDistance()
    {
        return (Mathf.Round((enemy.transform.position.x - player.transform.position.x)*100)/100);
    }

    public void Exit()
    {
        Application.Quit();
    }
    

    private void DeletePlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    /*private void InitializePlayerPrefs()
    {
        PlayerPrefs.SetFloat("MasterVolume", 0.6f);
        PlayerPrefs.SetInt("MusicActive", 1); //0 = OFF, 1 = ON
        PlayerPrefs.SetInt("ShowFPS", 0); //0 = OFF, 1 = ON
        PlayerPrefs.SetFloat("Brightness", 0.0f);
        PlayerPrefs.SetInt("HighScore", 0);
    }*/


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
