using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSStun : PlayerState
{
    float duration = 0f;

    public PSStun(PlayerController pc)
    {
        pc.rb.gravityScale = 2.7f;
        pc.anim.SetBool("isSliding", false);
    }

    public override void CheckTransition(PlayerController pc)
    {
        if (duration >= pc._playerModel.stunTime)
        {
            pc.isStuned = false;
            pc.ChangeState(new PSGrounded(pc));
        } 
    }

    public override void FixedUpdate(PlayerController pc)
    {
        pc.rb.velocity = new Vector2(pc._playerModel.stunSpeed, pc.rb.velocity.y);
    }

    public override void Update(PlayerController pc)
    {
        Debug.Log(duration);
        duration += Time.deltaTime;
    }
}
