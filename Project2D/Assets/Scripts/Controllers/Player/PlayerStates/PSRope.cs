using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSRope : AState
{

    [HideInInspector] private PlayerController pc;

    public PSRope(PlayerController _pc)
    {
        pc = _pc;
        pc.anim.SetTrigger("T-Rope");
        pc.anim.SetBool("B-Rope", true);
    }

    public override void CheckTransition()
    {
        if (pc.transform.parent == null)
        {
            pc.anim.SetTrigger("T-RopeOut");
            pc.isRope = false;
            pc.rb.bodyType = RigidbodyType2D.Dynamic;
            pc.rb.AddForce(Vector2.one * pc.model.exitRopeForce, ForceMode2D.Impulse);
            pc.ChangeState(new PSOnAir(pc));
        }
            
    }

    public override void FixedUpdate()
    {
        pc.transform.position = Vector3.Lerp(pc.transform.position, new Vector3(pc.transform.parent.position.x-.3f, pc.transform.position.y, 0), .5f);
        pc.rb.velocity = new Vector2(0, pc.rb.velocity.y);
    }

    public override void Update() { }
}
