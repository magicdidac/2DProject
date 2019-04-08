using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSTrampoline : AState
{

    public override void CheckTransition(AMoveController pc)
    {
        if (pc.transform.position.y > 8 * pc.gc.floor)
        {
            pc.isTrampoline = false;
            pc.ChangeState(new PSOnAir(pc));
        }
    }

    public override void FixedUpdate(AMoveController pc)
    {
        pc.rb.velocity = new Vector2(pc._playerModel.speed, pc._playerModel.jumpForce);
    }

    public override void Update(AMoveController pc) { }
}
