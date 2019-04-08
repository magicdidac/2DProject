using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSGrounded : AState
{
    public PSGrounded(AMoveController pc)
    {
        pc.rb.gravityScale = 2.7f;
        pc._playerModel.speed = pc._playerModel.normalSpeed;
    }

    public override void CheckTransition(AMoveController pc)
    {
        if (!pc.isGrounded) pc.ChangeState(new PSOnAir(pc));
        if (pc.isStuned) pc.ChangeState(new PSStun(pc));
        if (Input.GetKey(KeyCode.S)) pc.ChangeState(new PSSliding(pc));
    }

    public override void FixedUpdate(AMoveController pc)
    {
        pc.rb.velocity = new Vector2(pc._playerModel.speed, pc.rb.velocity.y);
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
