using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerModel", menuName = "Models/Player", order = 0)]
public class PlayerModel : CharacterModel
{
    public float movingSpeed;
    public float slideSpeed;
    public float jumpImpulse;
    public float jumpSpeedFactor;
    public float jumpCoolDown;
    public float groundRangeX;
    public float groundRangeY;
    public float slideTime;
}
