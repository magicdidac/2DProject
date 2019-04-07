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
        if (pc.isGrounded && Input.GetKey(KeyCode.S)) pc.ChangeState(new PSGrounded(pc));
        else if (pc.isGrounded) pc.ChangeState(new PSGrounded(pc));

        if (pc.isTrampoline) pc.ChangeState(new PSTrampoline());

        if (pc.isRope) pc.ChangeState(new PSRope());

    }

    public override void FixedUpdate(AMoveController pc)
    {
        pc.rb.velocity = new Vector2(pc._playerModel.speed, pc.rb.velocity.y);
    }

    public override void Update(AMoveController pc)
    {
        return;
    }
}
