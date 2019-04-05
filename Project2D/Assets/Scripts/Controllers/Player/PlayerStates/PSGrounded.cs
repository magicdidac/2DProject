using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSGrounded : AState
{
    public PSGrounded(PlayerController pc)
    {
        pc.rb.gravityScale = 2.7f;
    }

    public override void CheckTransition(PlayerController pc)
    {
        if (!pc.isGrounded) pc.ChangeState(new PSOnAir(pc));
        if (pc.isStuned) pc.ChangeState(new PSStun(pc));
        if (Input.GetKeyDown(KeyCode.S)) pc.ChangeState(new PSSliding(pc));
    }

    public override void FixedUpdate(PlayerController pc)
    {
        pc.rb.velocity = new Vector2(pc._playerModel.speed, pc.rb.velocity.y);
    }

    public override void Update(PlayerController pc)
    {
        Jump(pc);
    }

    private void Jump(PlayerController pc)
    {
        pc._playerModel.jumpForce = 14f;
        if (pc.isGrounded && Input.GetButtonDown("Jump"))
        {
            pc.rb.velocity = Vector2.up * pc._playerModel.jumpForce;
        }
    }
}
