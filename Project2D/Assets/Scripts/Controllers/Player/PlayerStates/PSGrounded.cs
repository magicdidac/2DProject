﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSGrounded : AState
{
    public PSGrounded(AMoveController pc)
    {
        if(pc.rb != null)
            pc.rb.gravityScale = 2.7f;

        pc.model.speed = pc.model.normalSpeed;
    }

    public override void CheckTransition(AMoveController pc)
    {
        if (!pc.isGrounded) pc.ChangeState(new PSOnAir(pc));
        if (pc.isStuned) pc.ChangeState(new PSStun(pc));
        if (pc.isTrampoline) pc.ChangeState(new PSTrampoline());
        if (Input.GetKey(KeyCode.S) && pc.combustible > 0) pc.ChangeState(new PSSliding(pc));
    }

    public override void FixedUpdate(AMoveController pc)
    {
        pc.rb.velocity = new Vector2(pc.model.speed, pc.rb.velocity.y);
    }

    public override void Update(AMoveController pc)
    {
        Jump(pc);
    }

    private void Jump(AMoveController pc)
    {
        if (pc.isGrounded && Input.GetButtonDown("Jump"))
        {
            pc.anim.SetTrigger("T-Jump");
            pc.rb.velocity = Vector2.up * pc.model.jumpForce;
        }
    }
}
