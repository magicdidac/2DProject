using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AState
{
    public abstract void Update(AMoveController pc);
    public abstract void FixedUpdate(AMoveController pc);
    public abstract void CheckTransition(AMoveController pc);
}
