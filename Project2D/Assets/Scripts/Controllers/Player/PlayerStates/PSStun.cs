using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSStun : AState
{
    float duration = 0f;

    public PSStun(AMoveController pc)
    {
        pc.rb.gravityScale = 2.7f;
        pc.anim.SetBool("isSliding", false);
    }

    public override void CheckTransition(AMoveController pc)
    {
        if (duration >= pc._playerModel.stunTime)
        {
            pc.isStuned = false;
            pc.ChangeState(new PSGrounded(pc));
        } 
    }

    public override void FixedUpdate(AMoveController pc)
    {
        pc.rb.velocity = new Vector2(pc._playerModel.stunSpeed, pc.rb.velocity.y);
    }

    public override void Update(AMoveController pc)
    {
        duration += Time.deltaTime;
    }
}
