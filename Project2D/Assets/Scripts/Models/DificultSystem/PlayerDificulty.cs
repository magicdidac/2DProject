using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerDificulty
{
    [HideInInspector] public enum Dificulty { Easy, Normal, Hard};

    [SerializeField] private Dificulty name = Dificulty.Easy;
    [SerializeField] private int maxSkillPoints = 10;
    [SerializeField] private int minSkillPoints = 0;
    [SerializeField] private float speedMultiplier = 0;


    public Dificulty GetName() { return name; }

    public int GetMaxSkillPoints() { return maxSkillPoints; }

    public int GetMinSkillPoints() { return minSkillPoints; }

    public float GetSpeedMultiplier() { return speedMultiplier; }

    public bool isDificulty(int skillPoints)
    {
        return (skillPoints >= minSkillPoints && skillPoints < maxSkillPoints);
    }


}
