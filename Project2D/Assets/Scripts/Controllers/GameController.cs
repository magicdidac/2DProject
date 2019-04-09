using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour //This class follows the Singleton Pattern
{

    [HideInInspector] public static GameController instance = null; //Allows to acces to the game controller from any other script

    [HideInInspector] public MapController mapGenerator; //Map controller reference
    [HideInInspector] public PlayerController player; //Player reference

    private void Awake()
    {

        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController> ();
        mapGenerator = GameObject.FindGameObjectWithTag("MapController").GetComponent<MapController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    public void ConsumeCombustible()
    {
        player.combustible -= Time.deltaTime;
        GameObject.FindWithTag("HUD").GetComponent<HUD>().ChangeFuelBar(player.combustible);
    }

    public void GetCombustible(float value)
    {
        if (player.combustible + value < player._playerModel.maxCombustible) player.combustible += value;
        else player.combustible = player._playerModel.maxCombustible;
        GameObject.FindWithTag("HUD").GetComponent<HUD>().ChangeFuelBar(player.combustible);
    }

}
