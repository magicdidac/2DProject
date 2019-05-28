using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugController : MonoBehaviour
{
    [HideInInspector] private GameController gc;

    [SerializeField] private TextMeshProUGUI textComponent = null;
    [HideInInspector] private const string staticText = "Player position: {0} (X,Y)\n"+
                                                        "Player state: {1}\n"+
                                                        "Player skill: {2}\n\n"+
                                                        "Enemy position: {3} (X,Y)\n"+
                                                        "Enemy state: {4}\n\n"+
                                                        "Game velocity multiplier: x{5}\n"+
                                                        "Game velocity: {6}\n"+
                                                        "Game difficulty: {7}\n\n"+
                                                        "Floor: {8}\n"+
                                                        "Distance between: {9} units";


    private void Update()
    {
        if (gc == null)
            gc = GameController.instance;

        textComponent.text = string.Format(staticText,
            new Vector2(gc.player.transform.position.x, gc.player.transform.position.y),
            gc.player.currentState.ToString(),
            PlayerPrefs.GetInt("PlayerSkill", 30),
            new Vector2(gc.enemy.transform.position.x, gc.enemy.transform.position.y),
            gc.enemy.currentState.ToString(),
            "WIP",
            "WIP",
            GetDificulty(),
            gc.GetFloor(),
            gc.getEnemyDistance());
    }

    private string GetDificulty()
    {
        if (PlayerPrefs.GetInt("PlayerSkill", 30) <= 40)
            return "easy";
        else if (PlayerPrefs.GetInt("PlayerSkill", 30) <= 80)
            return "normal";
        else
            return "hard";
    }

}
