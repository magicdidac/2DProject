using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSTrampoline : AState
{

    [HideInInspector] private PlayerController pc;

    public PSTrampoline(PlayerController _pc)
    {
        pc = _pc;
    }

    public override void CheckTransition()
    {
        if (pc.transform.position.y > 8 * pc.gc.GetFloor())
        {
            pc.isTrampoline = false;
            pc.ChangeState(new PSOnAir(pc));
        }
    }

    public override void FixedUpdate()
    {
        pc.rb.velocity = new Vector2(pc.model.speed, pc.model.jumpForce);
    }

    public override void Update() { }
}
