using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESOnAir : AState
{
    [HideInInspector] private EnemyController ec;

    public ESOnAir(EnemyController _ec)
    {
        ec = _ec;
    }

    public override void CheckTransition()
    {
        
        if (ec.isGrounded)
        {
            ec.anim.SetBool("B-Ground", true);
            ec.ChangeState(new ESGrounded(ec));
        }

        if (ec.transform.position.y < -1)
            ec.ChangeState(new ESFloatingUp(ec));

        if (ec.DetectGroundToLand("Down"))
            ec.ChangeState(new ESFloatingUp(ec));

    }

    public override void FixedUpdate()
    {
    }

    public override void Update()
    {
    }
}
