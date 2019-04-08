﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour //This class follows the Singleton Pattern
{
    [SerializeField] private Vector3 stateInfoPosition = Vector3.zero;
    [SerializeField] private int fontSize = 20;

    [HideInInspector] public static GameController instance = null; //Allows to acces to the game controller from any other script

    [HideInInspector] public MapController mapController; //Map controller reference
    [HideInInspector] public PlayerController player; //Player reference
    [HideInInspector] public EnemyController enemy; //Enemy reference

    [HideInInspector] public int floor = 0;
    [HideInInspector] public float enemyDistance = 3;
    [HideInInspector] public float maxDistance;

    private void Awake()
    {

        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController> ();
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyController> ();
        mapController = GameObject.FindGameObjectWithTag("MapController").GetComponent<MapController>();

        enemyDistance = Mathf.Abs(player.transform.position.x) + Mathf.Abs(enemy.transform.position.x);
        maxDistance = enemyDistance + 4.5f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex).completed += GameController_completed;  
    }

    private void GameController_completed(AsyncOperation obj)
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyController>();
        mapController = GameObject.FindGameObjectWithTag("MapController").GetComponent<MapController>();
    }

    public float getEnemyDistance()
    {
        return (Mathf.Round((enemy.transform.position.x - player.transform.position.x)*100)/100);
    }

    private void OnDrawGizmos()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyController>();
        //enemyDistance = Mathf.Abs(player.transform.position.x) + Mathf.Abs(enemy.transform.position.x);
        drawState();
    }

    private void drawState()
    {
        //if (player.currentState != null)
        //{
            GUIStyle style = new GUIStyle();
            style.normal.textColor = Color.red;
            style.fontSize = fontSize;
            style.alignment = TextAnchor.MiddleLeft;
            Handles.Label(stateInfoPosition + Camera.main.transform.position, "Player state: " + player.currentState + "\nEnemy state: " + enemy.currentState + "\nFloor: " + floor+ "\nEnemy: "+getEnemyDistance()+" ("+enemyDistance+")", style);
        //}
    }
}