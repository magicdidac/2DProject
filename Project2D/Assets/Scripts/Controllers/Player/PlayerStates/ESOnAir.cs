using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESOnAir : AState
{
    public ESOnAir(AMoveController pc)
    {
        EnemyController ec = ((EnemyController)pc);
        ec.isGrounded = false;
        ec.canShoot = false;
        ec.rb.gravityScale = 2.7f;
        ec.rb.bodyType = RigidbodyType2D.Dynamic;
        ec.GetComponent<BoxCollider2D>().isTrigger = false;
        Debug.Log(this);
    }

    public override void CheckTransition(AMoveController pc)
    {
        EnemyController ec = ((EnemyController)pc);
        if (ec.isGrounded)
        {
            pc.ChangeState(new ESWaiting(pc));
        }
    }

    public override void FixedUpdate(AMoveController pc)
    {

    }

    public override void Update(AMoveController pc)
    {
        return;
    }
}
