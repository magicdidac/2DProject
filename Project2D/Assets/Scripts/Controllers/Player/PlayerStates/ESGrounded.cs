using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESGrounded : AState
{
    public ESGrounded(AMoveController pc)
    {
        EnemyController ec = (EnemyController)pc;
        ec.calculateTimeToNextShoot();
        ec.hasJump = false;

        if (ec.rb != null)
        {
            ec.rb.bodyType = RigidbodyType2D.Dynamic;
            ec.GetComponent<BoxCollider2D>().isTrigger = false;
        }
        ec.shield.gameObject.SetActive(false);
        Debug.Log(this + "\n" + ec.isGrounded);
    }

    public override void CheckTransition(AMoveController pc)
    {
        EnemyController ec = (EnemyController)pc;
        if (ec.isSliding) return;
        if (!ec.isGrounded && ec.hasJump) ec.ChangeState(new ESOnAir(ec));
        else if (!ec.isGrounded) ec.ChangeState(new ESFloatingUp(ec));
    }

    public override void FixedUpdate(AMoveController pc)
    {
        
    }

    public override void Update(AMoveController pc)
    {
        EnemyController ec = (EnemyController)pc;
        ec.JumpSlideDetect(pc);
    }
}
