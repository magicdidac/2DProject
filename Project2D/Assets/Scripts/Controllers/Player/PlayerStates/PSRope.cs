using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSRope : PlayerState
{

    public override void CheckTransition(PlayerController pc)
    {
        if (pc.transform.parent == null)
        {
            pc.isRope = false;
            pc.rb.bodyType = RigidbodyType2D.Dynamic;
            pc.rb.AddForce(Vector2.one * pc._playerModel.exitRopeForce, ForceMode2D.Impulse);
            pc.ChangeState(new PSOnAir(pc));
        }
            
    }

    public override void FixedUpdate(PlayerController pc)
    {
        pc.rb.velocity = new Vector2(0, pc.rb.velocity.y);
    }

    public override void Update(PlayerController pc)
    {
        //throw new System.NotImplementedException();
    }
}
