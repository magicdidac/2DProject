using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugController : AController
{

    [SerializeField] private TextMeshProUGUI textComponent = null;
    [HideInInspector] private const string staticText = "Player position: {0} (X,Y)\n"+
                                                        "Player state: {1}\n"+
                                                        "Player skill: {2}\n\n"+
                                                        "Enemy position: {3} (X,Y)\n"+
                                                        "Enemy state: {4}\n\n"+
                                                        "Game velocity multiplier: x {5}\n"+
                                                        "Player velocity multiplier: x {6}\n"+
                                                        "Game velocity: {7}\n"+
                                                        "Game difficulty: {8}\n\n"+
                                                        "Floor: {9}\n"+
                                                        "Distance between: {10} units";


    private void Update()
    {

        if (gc.player != null)
        {
            try
            {
                textComponent.text = string.Format(staticText,
                    new Vector2(gc.player.transform.position.x, gc.player.transform.position.y),
                    gc.player.currentState.ToString(),
                    PlayerPrefs.GetInt("PlayerSkill", 30),
                    new Vector2(gc.enemy.transform.position.x, gc.enemy.transform.position.y),
                    gc.enemy.currentState.ToString(),
                    gc.GetVelocityMultiplier(),
                    gc.GetSkillMultiplier(),
                    gc.GetVelocity(7.5f),
                    GetDificulty(),
                    gc.GetFloor(),
                    gc.GetEnemyDistance());

                return;
            }
            catch { }
        }

        textComponent.text = string.Format(staticText,
            "null",
            "null",
            PlayerPrefs.GetInt("PlayerSkill", 30),
            "null",
            "null",
            gc.GetVelocityMultiplier(),
            gc.GetSkillMultiplier(),
            gc.GetVelocity(7.5f),
            GetDificulty(),
            gc.GetFloor(),
            "null");
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
