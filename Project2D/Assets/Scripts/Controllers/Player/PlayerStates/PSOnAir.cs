using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSOnAir : PlayerState
{
    public PSOnAir(PlayerController pc)
    {
        pc.rb.gravityScale = 2.7f;
    }

    public override void CheckTransition(PlayerController pc)
    {
        if (pc.isGrounded) pc.ChangeState(new PSGrounded(pc));
        if (pc.isRope) pc.ChangeState(new PSRope());
        if (Input.GetKeyDown(KeyCode.S)) pc.ChangeState(new PSSliding(pc));
    }

    public override void FixedUpdate(PlayerController pc)
    {

    }

    public override void Update(PlayerController pc)
    {
        //pc.groundCollision();
    }
}
