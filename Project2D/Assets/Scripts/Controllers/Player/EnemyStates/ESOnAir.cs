using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESOnAir : AState
{
    public ESOnAir(AMoveController pc)
    {
        EnemyController ec = ((EnemyController)pc);
        ec.canShoot = false;
        ec.canCharge = false;
    }

    public override void CheckTransition(AMoveController pc)
    {

        EnemyController ec = (EnemyController)pc;
        if (pc.isGrounded)
            pc.ChangeState(new ESGrounded(pc));

        if (pc.transform.position.y < -1)
            pc.ChangeState(new ESFloatingUp(pc));

        if (ec.DetectGroundToLand("Down"))
            pc.ChangeState(new ESFloatingUp(pc));

    }

    public override void FixedUpdate(AMoveController pc)
    {
        return;
    }

    public override void Update(AMoveController pc)
    {
        return;
    }
}
