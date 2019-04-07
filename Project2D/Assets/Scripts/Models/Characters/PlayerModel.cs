using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerModel", menuName = "Models/Player", order = 0)]
public class PlayerModel : CharacterModel
{
    public float jumpForce;
    public int precisionDown;
    public float offset;
    public float trampolineOffset;
    public float slideSpeed;
    public float stunSpeed;
    public float groundRangeX;
    public float groundRangeY;
    public float stunTime;
    public float exitRopeForce;
}
