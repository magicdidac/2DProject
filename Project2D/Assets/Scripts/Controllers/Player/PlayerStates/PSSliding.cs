using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSSliding : AState
{
    float time;
    bool goingDown;

    public PSSliding(AMoveController pc)
    {
        if (!pc.isGrounded)
        {
            pc.rb.gravityScale = 7;
            goingDown = true;
        }
        else goingDown = false;
        pc.anim.SetBool("B-Slide", true);
    }

    public override void CheckTransition(AMoveController pc)
    {
        if (Input.GetKeyUp(KeyCode.S))
        {
            pc.anim.SetBool("B-Slide", false);
            pc.ChangeState(new PSGrounded(pc));
        }
        if (pc.isRope)
        {
            pc.anim.SetBool("B-Slide", false);
            pc.ChangeState(new PSRope(pc));
        }
        if (pc.isTrampoline)
        {
            pc.anim.SetBool("B-Slide", false);
            pc.ChangeState(new PSTrampoline());
        }
        if (pc.combustible <= 0 && pc.isGrounded)
        {
            pc.anim.SetBool("B-Slide", false);
            pc.ChangeState(new PSGrounded(pc));
        }
        if (!pc.isGrounded && !goingDown)
        {
            pc.anim.SetBool("B-Slide", false);
            pc.ChangeState(new PSOnAir(pc));
        }
        if (pc.isTrampoline)
        {
            pc.anim.SetBool("B-Slide", false);
            pc.ChangeState(new PSTrampoline());
        }
    }

    public override void FixedUpdate(AMoveController pc)
    {
        pc.rb.velocity = new Vector2(pc._playerModel.slideSpeed, pc.rb.velocity.y);
    }

    public override void Update(AMoveController pc)
    {
        if (pc.isGrounded) goingDown = false;
        Jump(pc);
        GameController.instance.ConsumeCombustible();
    }

    private void Jump(AMoveController pc)
    {
        if (pc.isGrounded && Input.GetButtonDown("Jump"))
        {
            pc._playerModel.speed = pc._playerModel.plusJumpSpeedX;
            pc.rb.velocity = Vector2.up * pc._playerModel.jumpForce;
        }
    }
}
