using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESSliding : AState
{

    private bool enterSlideZone = false;

    public ESSliding(AMoveController pc)
    {
    }

    public override void CheckTransition(AMoveController pc)
    {
        EnemyController ec = (EnemyController)pc;

        if (!enterSlideZone && ec.DetectObstacleUp())
            enterSlideZone = true;
        if (enterSlideZone && !ec.DetectObstacleUp())
            ec.ChangeState(new ESGrounded(ec));
                
    }

    public override void FixedUpdate(AMoveController pc)
    {
        
    }

    public override void Update(AMoveController pc)
    {
        
    }
}
