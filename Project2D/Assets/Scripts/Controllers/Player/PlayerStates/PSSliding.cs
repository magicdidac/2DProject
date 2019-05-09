using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSSliding : AState
{
    float time;
    bool goingDown;

    private AMoveController moveController;

    public PSSliding(AMoveController pc)
    {
        moveController = pc;
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

    public override void CheckTransition(AMoveController pc)
    {
        if (Input.GetKeyUp(KeyCode.S))
            ChangeToState(new PSGrounded(pc));

        if (pc.isRope)
            ChangeToState(new PSRope(pc));

        if (pc.isTrampoline)
            ChangeToState(new PSTrampoline());

        if (pc.combustible <= 0 && pc.isGrounded)
            ChangeToState(new PSGrounded(pc));

        if (!pc.isGrounded && !goingDown)
            ChangeToState(new PSOnAir(pc));
    }

    private void ChangeToState(AState state)
    {
        moveController.anim.SetTrigger("T-SlideUp");
        moveController.anim.SetBool("B-Slide", false);
        moveController.ChangeState(state);
    }

    public override void FixedUpdate(AMoveController pc)
    {
        pc.rb.velocity = new Vector2(pc.model.slideSpeed, pc.rb.velocity.y);
    }

    public override void Update(AMoveController pc)
    {
        if (pc.isGrounded) goingDown = false;
        Jump(pc);
        pc.gc.ConsumeCombustible();
    }

    private void Jump(AMoveController pc)
    {
        if (pc.isGrounded && Input.GetButtonDown("Jump"))
        {
            pc.model.speed = pc.model.plusJumpSpeedX;
            pc.rb.velocity = Vector2.up * pc.model.jumpForce;
        }
    }
}
