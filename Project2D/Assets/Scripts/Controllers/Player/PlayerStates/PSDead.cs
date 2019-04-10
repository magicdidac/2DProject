using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSDead : AState
{
    public PSDead(AMoveController pc)
    {
        pc.rb.velocity = Vector2.zero;
        pc.isDead = true;
    }

    public override void CheckTransition(AMoveController pc)
    {
        
    }

    public override void FixedUpdate(AMoveController pc)
    {
        
    }

    public override void Update(AMoveController pc)
    {
        
    }
}
