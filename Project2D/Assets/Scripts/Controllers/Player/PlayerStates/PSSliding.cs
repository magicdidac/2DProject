using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSSliding : AState
{

    public PSSliding(PlayerController pc)
    {
        pc.rb.gravityScale = 4;
        pc._playerModel.jumpForce = 0;
        pc.anim.SetBool("isSliding", true);
    }

    public override void CheckTransition(PlayerController pc)
    {
        if (Input.GetKeyUp(KeyCode.S))
        {
            pc.anim.SetBool("isSliding", false);
            pc.ChangeState(new PSGrounded(pc));
        }
        if (pc.isRope)
        {
            pc.anim.SetBool("isSliding", false);
            pc.ChangeState(new PSRope());
        }
    }

    public override void FixedUpdate(PlayerController pc)
    {
        pc.rb.velocity = new Vector2(pc._playerModel.slideSpeed, pc.rb.velocity.y);
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
