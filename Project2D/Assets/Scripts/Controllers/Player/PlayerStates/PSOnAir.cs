using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSOnAir : AState
{
    public PSOnAir(AMoveController pc)
    {
        pc.rb.gravityScale = 2.7f;
    }

    public override void CheckTransition(AMoveController pc)
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            pc.ChangeState(new PSSliding(pc));
        }

        if (pc.isGrounded) pc.ChangeState(new PSGrounded(pc));

        if (pc.isTrampoline) pc.ChangeState(new PSTrampoline());

        if (pc.isRope) pc.ChangeState(new PSRope());

        if (pc.isTirolina) pc.ChangeState(new PSTirolina(pc));
    }

    public override void FixedUpdate(AMoveController pc)
    {
        if(pc.rb.velocity.x < pc._playerModel.speed)
            pc.rb.velocity = new Vector2(pc._playerModel.speed, pc.rb.velocity.y);
        else
            pc.rb.velocity = new Vector2(pc.rb.velocity.x, pc.rb.velocity.y);
    }

    public override void Update(AMoveController pc)
    {
        return;
    }
}
