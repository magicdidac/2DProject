using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSSliding : AState
{
    float time;
    bool goingDown;

    private PlayerController pc;

    public PSSliding(PlayerController _pc)
    {
        pc = _pc;
        if (!pc.isGrounded)
        {
            pc.rb.gravityScale = 7;
            goingDown = true;
        }
        else goingDown = false;

        pc.anim.SetBool("B-Rope", false);
        pc.anim.SetBool("B-ZipLine", false);
        pc.anim.SetBool("B-Slide", true);
        pc.anim.SetTrigger("T-Slide");
        
    }

    public override void CheckTransition()
    {

        if (Input.GetKeyUp(KeyCode.S))
            ChangeToState(new PSGrounded(pc));

        if (pc.isRope)
            ChangeToState(new PSRope(pc));

        if (pc.isTrampoline)
            ChangeToState(new PSTrampoline(pc));

        if (!pc.HaveFuel() && pc.isGrounded)
            ChangeToState(new PSGrounded(pc));

        if (!pc.isGrounded && !goingDown)
            ChangeToState(new PSOnAir(pc));
    }

    private void ChangeToState(AState state)
    {
        pc.anim.SetTrigger("T-SlideUp");
        pc.anim.SetBool("B-Slide", false);
        pc.ChangeState(state);
    }

    public override void FixedUpdate()
    {
        pc.rb.velocity = new Vector2(pc.model.slideSpeed, pc.rb.velocity.y);
    }

    public override void Update()
    {
        if (pc.isGrounded) goingDown = false;
        Jump();
        pc.ConsumeFuel();
    }

    private void Jump()
    {
        if (pc.isGrounded && Input.GetButtonDown("Jump"))
        {
            pc.model.speed = pc.model.plusJumpSpeedX;
            pc.rb.velocity = Vector2.up * pc.model.jumpForce;
        }
    }
}
