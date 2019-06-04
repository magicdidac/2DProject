using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSRope : AState
{

    [HideInInspector] private PlayerController pc;

    public PSRope(PlayerController _pc) : base()
    {
        pc = _pc;
        pc.animator.SetTrigger("T-Rope");
        pc.animator.SetBool("B-Rope", true);
    }

    public override void CheckTransition()
    {
        if (pc.transform.parent == null)
        {
            pc.animator.SetTrigger("T-RopeOut");
            pc.isRope = false;
            pc.rigidbody2d.bodyType = RigidbodyType2D.Dynamic;
            pc.rigidbody2d.AddForce(Vector2.one * pc.model.exitRopeForce, ForceMode2D.Impulse);
            pc.ChangeState(new PSOnAir(pc));
        }
            
    }

    public override void FixedUpdate()
    {
        pc.transform.position = Vector3.Lerp(pc.transform.position, new Vector3(pc.transform.parent.position.x-.3f, pc.transform.position.y, 0), .5f);
        pc.rigidbody2d.velocity = new Vector2(0, pc.rigidbody2d.velocity.y);
    }

    public override void Update() { }
}
