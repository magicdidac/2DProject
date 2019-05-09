using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESSliding : AState
{

    private bool enterSlideZone = false;

    public ESSliding(AMoveController pc)
    {
        pc.anim.SetTrigger("T-Slide");
    }

    public override void CheckTransition(AMoveController pc)
    {
        EnemyController ec = (EnemyController)pc;

        if (!enterSlideZone && ec.DetectObstacleUp())
            enterSlideZone = true;
        if (enterSlideZone && !ec.DetectObstacleUp())
        {
            pc.anim.SetTrigger("T-SlideOut");
            ec.ChangeState(new ESGrounded(ec));
        }
                
    }

    public override void FixedUpdate(AMoveController pc)
    {
        
    }

    public override void Update(AMoveController pc)
    {
        
    }
}
