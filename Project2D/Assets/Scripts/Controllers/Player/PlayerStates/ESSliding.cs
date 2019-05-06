using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESSliding : AState
{
    public ESSliding(AMoveController pc)
    {
        EnemyController ec = (EnemyController)pc;
        if (!ec.isGrounded)
        {
            ec.rb.gravityScale = 7;
        }

        ec.anim.SetBool("isSliding", true);
        Debug.Log(this);
    }

    public override void CheckTransition(AMoveController pc)
    {
        EnemyController ec = (EnemyController)pc;
        ec.StartCoroutine(ec.ChangeState(ec));
        
    }

    public override void FixedUpdate(AMoveController pc)
    {
        
    }

    public override void Update(AMoveController pc)
    {
        
    }
}
