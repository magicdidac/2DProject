using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESSliding : AState
{

    [HideInInspector] private EnemyController ec;

    private bool enterSlideZone = false;

    public ESSliding(EnemyController _ec)
    {
        ec = _ec;
        ec.anim.SetTrigger("T-Slide");
    }

    public override void CheckTransition()
    {

        if (!enterSlideZone && ec.DetectObstacleUp())
            enterSlideZone = true;
        if (enterSlideZone && !ec.DetectObstacleUp())
        {
            ec.anim.SetTrigger("T-SlideOut");
            ec.ChangeState(new ESGrounded(ec));
        }
                
    }

    public override void FixedUpdate()
    {
        
    }

    public override void Update()
    {
        
    }
}
