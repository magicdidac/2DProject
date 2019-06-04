using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSSliding : AState
{
    float time;
    bool goingDown;

    private PlayerController pc;

    public PSSliding(PlayerController _pc) : base()
    {
        pc = _pc;
        if (!pc.isGrounded)
        {
            pc.rigidbody2d.gravityScale = 7;
            goingDown = true;
        }
        else goingDown = false;

        pc.animator.SetBool("B-Rope", false);
        pc.animator.SetBool("B-ZipLine", false);
        pc.animator.SetBool("B-Slide", true);
        pc.animator.SetTrigger("T-Slide");
        gc.uiController.SetOnTurboText();

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
        pc.animator.SetTrigger("T-SlideUp");
        pc.animator.SetBool("B-Slide", false);
        gc.uiController.SetOffTurboText();
        pc.ChangeState(state);
    }

    public override void FixedUpdate()
    {
        pc.rigidbody2d.velocity = new Vector2(gc.GetVelocity(pc.model.slideSpeed), pc.rigidbody2d.velocity.y);
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
            pc.rigidbody2d.velocity = Vector2.up * pc.model.jumpForce;
        }
    }
}
