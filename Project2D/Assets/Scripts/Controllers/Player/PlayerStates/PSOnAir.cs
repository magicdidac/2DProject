using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSOnAir : PlayerState
{
    public PSOnAir(PlayerController pc)
    {

    }

    public override void CheckTransition(PlayerController pc)
    {
        if (pc.isGrounded) pc.ChangeState(new PSGrounded(pc));
    }

    public override void FixedUpdate(PlayerController pc)
    {

    }

    public override void Update(PlayerController pc)
    {
        //pc.groundCollision();
    }
}
