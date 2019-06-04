using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSStun : AState
{

    [HideInInspector] private PlayerController pc;
    float duration = 0f;

    public PSStun(PlayerController _pc)
    {
        pc = _pc;
        pc.rb.gravityScale = 2.7f;
        pc.anim.SetTrigger("T-Impact");
        pc.anim.SetBool("B-Slide", false);
    }

    public override void CheckTransition()
    {
        if (duration >= pc.model.stunTime)
        {
            pc.isStuned = false;
            pc.ChangeState(new PSGrounded(pc));
        } 
    }

    public override void FixedUpdate()
    {
        pc.rb.velocity = new Vector2(pc.gc.GetVelocity(pc.model.stunSpeed), pc.rb.velocity.y);
    }

    public override void Update()
    {
        duration += Time.deltaTime;
    }
}
