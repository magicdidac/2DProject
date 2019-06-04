using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSGrounded : AState
{
    [HideInInspector] private PlayerController pc;

    public PSGrounded(PlayerController _pc)
    {
        pc = _pc;

        if(pc.rb != null)
            pc.rb.gravityScale = 2.7f;

        pc.model.speed = pc.model.normalSpeed;
    }

    public override void CheckTransition()
    {

        if (!pc.isGrounded) pc.ChangeState(new PSOnAir(pc));

        if (pc.isStuned) pc.ChangeState(new PSStun(pc));

        if (pc.isTrampoline) pc.ChangeState(new PSTrampoline(pc));

        if (Input.GetKey(KeyCode.S) && pc.HaveFuel()) pc.ChangeState(new PSSliding(pc));
    }

    public override void FixedUpdate()
    {
        pc.rb.velocity = new Vector2(pc.gc.GetVelocity(pc.model.speed), pc.rb.velocity.y);
    }

    public override void Update()
    {
        Jump();
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            pc.anim.SetTrigger("T-Jump");
            pc.rb.velocity = Vector2.up * pc.model.jumpForce;
        }
    }
}
