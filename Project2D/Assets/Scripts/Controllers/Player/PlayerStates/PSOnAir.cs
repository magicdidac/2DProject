using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSOnAir : AState
{
    public PSOnAir(AMoveController pc)
    {
        pc.rb.gravityScale = 2.7f;
    }

    public override void CheckTransition(AMoveController pc)
    {
        if (Input.GetKey(KeyCode.S) && pc.rb.velocity.y <= 0)
        {
            pc.ChangeState(new PSSliding(pc));
        }

        if (pc.isGrounded)
        {
            if (pc.anim.GetBool("B-Rope"))
                pc.anim.SetBool("B-Rope", false);
            if (pc.anim.GetBool("B-ZipLine"))
                pc.anim.SetBool("B-ZipLine", false);
            pc.ChangeState(new PSGrounded(pc));
        }

        if (pc.isTrampoline) pc.ChangeState(new PSTrampoline());

        if (pc.isRope)
            pc.ChangeState(new PSRope(pc));

        if (pc.isTirolina) pc.ChangeState(new PSZipLine(pc));
    }

    public override void FixedUpdate(AMoveController pc)
    {
        if(pc.rb.velocity.x < pc._playerModel.speed)
            pc.rb.velocity = new Vector2(pc._playerModel.speed, pc.rb.velocity.y);
        else
            pc.rb.velocity = new Vector2(pc.rb.velocity.x, pc.rb.velocity.y);
    }

    public override void Update(AMoveController pc)
    {
        return;
    }
}
