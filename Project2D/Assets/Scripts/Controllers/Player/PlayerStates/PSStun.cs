using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSStun : AState
{

    [HideInInspector] private PlayerController pc;
    float duration = 0f;

    public PSStun(PlayerController _pc) : base()
    {
        pc = _pc;
        pc.rigidbody2d.gravityScale = 2.7f;
        pc.animator.SetTrigger("T-Impact");
        pc.animator.SetBool("B-Slide", false);
    }

    public override void CheckTransition()
    {
        if (duration >= pc.model.stunTime)
        {
            pc.isStuned = false;
            pc.ChangeState(new PSGrounded(pc));
        }
        pc.canStunt = false;
        pc.EnableStun();
    }

    public override void FixedUpdate()
    {
        pc.rigidbody2d.velocity = new Vector2(gc.GetVelocity(pc.model.stunSpeed), pc.rigidbody2d.velocity.y);
    }

    public override void Update()
    {
        duration += Time.deltaTime;
    }
}
