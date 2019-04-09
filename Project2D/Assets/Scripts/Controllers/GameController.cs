using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour //This class follows the Singleton Pattern
{

    [HideInInspector] public static GameController instance = null; //Allows to acces to the game controller from any other script

    [HideInInspector] public MapController mapGenerator; //Map controller reference
    [HideInInspector] public PlayerController player; //Player reference

    [HideInInspector] public ScoreManager scoreManager; // Score Manager reference

    public Text scoreText;
    private int score = 0;

    private void Awake()
    {

        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController> ();
        mapGenerator = GameObject.FindGameObjectWithTag("MapController").GetComponent<MapController>();
        //scoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
        scoreManager = FindObjectOfType<ScoreManager>(); 
    }

    private void Start()
    {
        //StartCoroutine(AddScoreByTime());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    public void AddScore(int newScoreValue)
    {
        //score += newScoreValue;
        //UpdateScore();
        scoreManager.AddScore(newScoreValue);
    }

    /*private void UpdateScore()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    IEnumerator AddScoreByTime()
    {
        yield return new WaitForSeconds(1);
        AddScore(100); 
    }*/

}
