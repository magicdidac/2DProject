using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AState
{
    public abstract void Update();
    public abstract void FixedUpdate();
    public abstract void CheckTransition();
}
