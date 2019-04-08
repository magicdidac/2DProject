using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESShootingUp : AState
{
    public ESShootingUp(AMoveController pc)
    {
        ((EnemyController)pc).canShoot = false;
    }

    public override void CheckTransition(AMoveController pc)
    {
        EnemyController ec = ((EnemyController)pc);

        if (ec.canShoot)
        {
            ec.attack();
            ec.canShoot = false;
            pc.ChangeState(new ESWaiting(pc));
        }
    }

    public override void FixedUpdate(AMoveController pc)
    {
        
    }

    public override void Update(AMoveController pc)
    {
        EnemyController ec = ((EnemyController)pc);
        if (ec.canCharge && pc.transform.position.x >= ec.shootPosition-3)
        {
            ec.canCharge = false;
            pc.anim.SetTrigger("Charge");
        }
    }
}
