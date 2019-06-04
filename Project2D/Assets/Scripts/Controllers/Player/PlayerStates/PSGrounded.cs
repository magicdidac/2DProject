using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSGrounded : AState
{
    [HideInInspector] private PlayerController pc;

    public PSGrounded(PlayerController _pc) : base()
    {
        pc = _pc;

        if(pc.rigidbody2d != null)
            pc.rigidbody2d.gravityScale = 2.7f;

        pc.model.speed = pc.model.normalSpeed;
    }

    public override void CheckTransition()
    {

        if (!pc.isGrounded) pc.ChangeState(new PSOnAir(pc));

        if (pc.isStuned) pc.ChangeState(new PSStun(pc));

        if (pc.isTrampoline) pc.ChangeState(new PSTrampoline(pc));

        if (Input.GetKey(KeyCode.S) && pc.HaveFuel())
            pc.ChangeState(new PSSliding(pc));
    }

    public override void FixedUpdate()
    {
        pc.rigidbody2d.velocity = new Vector2(gc.GetVelocity(pc.model.speed), pc.rigidbody2d.velocity.y);
    }

    public override void Update()
    {
        Jump();
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            pc.animator.SetTrigger("T-Jump");
            pc.rigidbody2d.velocity = Vector2.up * pc.model.jumpForce;
        }
    }
}
