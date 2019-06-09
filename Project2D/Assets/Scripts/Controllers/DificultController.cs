using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DificultController : AController
{
    [SerializeField] private PlayerDificulty[] dificulties = null;

    public PlayerDificulty GetDificulty()
    {

        int skillPoints = PlayerPrefs.GetInt("PlayerSkill", 30);

        foreach (PlayerDificulty pd in dificulties)
        {
            if (pd.isDificulty(skillPoints))
                return pd;
        }

        return null;
    }

}
