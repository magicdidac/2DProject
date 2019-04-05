using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSOnAir : AState
{
    public PSOnAir(PlayerController pc)
    {
        pc.rb.gravityScale = 2.7f;
    }

    public override void CheckTransition(PlayerController pc)
    {
        if (pc.isGrounded) pc.ChangeState(new PSGrounded(pc));

        if (pc.isTrampoline) pc.ChangeState(new PSTrampoline(pc));

        if (pc.isRope) pc.ChangeState(new PSRope());

        if (Input.GetKeyDown(KeyCode.S)) pc.ChangeState(new PSSliding(pc));

    }

    public override void FixedUpdate(PlayerController pc)
    {
        pc.rb.velocity = new Vector2(pc._playerModel.speed, pc.rb.velocity.y);
    }

    public override void Update(PlayerController pc)
    {
        return;
    }
}
