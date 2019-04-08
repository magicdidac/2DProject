using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESWaiting : AState
{
    AMoveController myPc;

    public ESWaiting(AMoveController pc)
    {
        ((EnemyController)pc).calculateTimeToNextShoot();
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
