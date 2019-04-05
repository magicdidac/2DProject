using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AState
{
    public abstract void Update(PlayerController pc);
    public abstract void FixedUpdate(PlayerController pc);
    public abstract void CheckTransition(PlayerController pc);
}
