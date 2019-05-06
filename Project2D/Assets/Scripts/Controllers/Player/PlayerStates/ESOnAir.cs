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
        ec.rb.gravityScale = 2.7f;
        ec.rb.bodyType = RigidbodyType2D.Dynamic;
        ec.GetComponent<BoxCollider2D>().isTrigger = false;
        Debug.Log(this + "\n" + ec.isGrounded);
    }

    public override void CheckTransition(AMoveController pc)
    {
        EnemyController ec = ((EnemyController)pc);
        if (ec.isGrounded)
        {
            ec.ChangeState(new ESGrounded(ec));
        }
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
