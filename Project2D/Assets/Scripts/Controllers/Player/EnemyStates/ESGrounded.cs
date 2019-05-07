using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESGrounded : AState
{
    public ESGrounded(AMoveController pc)
    {
        EnemyController ec = (EnemyController)pc;        
        ec.shield.gameObject.SetActive(false);
    }

    public override void CheckTransition(AMoveController pc)
    {
        EnemyController ec = (EnemyController)pc;
        if (ec.DetectObstacleToJump())
        {
            Jump(pc);
            ec.ChangeState(new ESOnAir(ec));
        }

        if (!ec.DetectGroundToLand()) ec.ChangeState(new ESFloatingUp(ec));
        if (ec.DetectObstacleToSlide()) ec.ChangeState(new ESSliding(ec));
        if (ec.gc.getFloor() != 0)
            ec.ChangeState(new ESOtherFloor(ec));
    }

    public override void FixedUpdate(AMoveController pc) {}

    public override void Update(AMoveController pc) {}

    private void Jump(AMoveController pc)
    {
        //Anim Jump
        pc.rb.velocity = Vector2.up * pc.model.jumpForce;
    }

}
