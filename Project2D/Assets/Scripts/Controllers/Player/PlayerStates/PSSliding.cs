using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSSliding : AState
{

    public PSSliding(AMoveController pc)
    {
        pc.rb.gravityScale = 4;
        pc.anim.SetBool("isSliding", true);
    }

    public override void CheckTransition(AMoveController pc)
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
        if (pc.isTrampoline)
        {
            pc.anim.SetBool("isSliding", false);
            pc.ChangeState(new PSTrampoline());
        }
        if (!pc.isGrounded)
        {
            pc.anim.SetBool("isSliding", false);
            pc.ChangeState(new PSOnAir(pc));
        }
    }

    public override void FixedUpdate(AMoveController pc)
    {
        pc.rb.velocity = new Vector2(pc._playerModel.slideSpeed, pc.rb.velocity.y);
    }

    public override void Update(AMoveController pc)
    {
        Jump(pc);
    }

    private void Jump(AMoveController pc)
    {
        //pc._playerModel.jumpForce = 12.5f;
        if (pc.isGrounded && Input.GetButtonDown("Jump"))
        {
            pc.rb.velocity = Vector2.up * pc._playerModel.jumpForce;
        }
    }
}
