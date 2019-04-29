using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSRope : AState
{

    public PSRope(AMoveController pc)
    {
        pc.anim.SetTrigger("Rope");
    }

    public override void CheckTransition(AMoveController pc)
    {
        if (pc.transform.parent == null)
        {
            pc.anim.SetTrigger("Rope");
            pc.isRope = false;
            pc.rb.bodyType = RigidbodyType2D.Dynamic;
            pc.rb.AddForce(Vector2.one * pc._playerModel.exitRopeForce, ForceMode2D.Impulse);
            pc.ChangeState(new PSOnAir(pc));
        }
            
    }

    public override void FixedUpdate(AMoveController pc)
    {
        pc.transform.position = Vector3.Lerp(pc.transform.position, new Vector3(pc.transform.parent.position.x-.3f, pc.transform.position.y, 0), .5f);
        pc.rb.velocity = new Vector2(0, pc.rb.velocity.y);
    }

    public override void Update(AMoveController pc) { }
}
