﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour //This class follows the Singleton Pattern
{

    [HideInInspector] public static GameController instance = null; //Allows to acces to the game controller from any other script

    [HideInInspector] public MapGenerator mapGenerator; //Map controller reference
    [HideInInspector] public PlayerController player; //Player reference

    private void Awake()
    {

        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController> ();
        mapGenerator = GameObject.FindGameObjectWithTag("MapController").GetComponent<MapGenerator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

}
