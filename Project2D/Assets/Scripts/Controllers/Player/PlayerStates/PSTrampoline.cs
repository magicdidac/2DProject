using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSTrampoline : AState
{
    public PSTrampoline(PlayerController pc)
    {

    }

    public override void CheckTransition(PlayerController pc)
    {
        if (pc.transform.position.y > 8 * pc.floor)
        {
            pc.isTrampoline = false;
            pc.ChangeState(new PSOnAir(pc));
        }
    }

    public override void FixedUpdate(PlayerController pc)
    {

    }

    public override void Update(PlayerController pc)
    {
        pc.rb.velocity = Vector2.up * pc._playerModel.jumpForce;
    }
}
