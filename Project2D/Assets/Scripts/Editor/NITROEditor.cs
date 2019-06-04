using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class NITROEditor : EditorWindow
{
    [MenuItem("N.I.T.R.O./Easy")]
    public static void SetEasyMode()
    {
        PlayerPrefs.SetInt("PlayerSkill", 30);
        Debug.Log("Established difficulty easy");
    }

    [MenuItem("N.I.T.R.O./Normal")]
    public static void SetNormalMode()
    {
        PlayerPrefs.SetInt("PlayerSkill", 50);
        Debug.Log("Established difficulty normal");
    }

    [MenuItem("N.I.T.R.O./Hard")]
    public static void SetHardMode()
    {
        PlayerPrefs.SetInt("PlayerSkill", 90);
        Debug.Log("Established difficulty hard");
    }

    [MenuItem("N.I.T.R.O./GetOneHundredCoins")]
    public static void GetOneHundredCoins()
    {

        if(GameController.instance == null)
        {
            Debug.LogWarning("Game needs to be running to do this action");
            return;
        }

        GameController.instance.scoreController.AddCoins(100);
        Debug.Log("Added 100 Coins to player");
    }

}
