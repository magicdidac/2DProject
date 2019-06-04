using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AState
{
    [HideInInspector] protected GameController gc;
    
    public AState()
    {
        gc = GameController.instance;
    }

    public abstract void Update();
    public abstract void FixedUpdate();
    public abstract void CheckTransition();
}
