using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSOnAir : AState
{
    [HideInInspector] private PlayerController pc;

    public PSOnAir(PlayerController _pc) : base()
    {
        pc = _pc;
        pc.rigidbody2d.gravityScale = 2.7f;
    }

    public override void CheckTransition()
    {
        if (Input.GetKey(KeyCode.S) && pc.rigidbody2d.velocity.y <= 0)
            pc.ChangeState(new PSSliding(pc));

        if (pc.isGrounded)
        {
            if (pc.animator.GetBool("B-Rope"))
                pc.animator.SetBool("B-Rope", false);
            if (pc.animator.GetBool("B-ZipLine"))
                pc.animator.SetBool("B-ZipLine", false);
            gc.audioController.PlaySound("dropping");
            pc.ChangeState(new PSGrounded(pc));
        }

        if (pc.isTrampoline)
            pc.ChangeState(new PSTrampoline(pc));

        if (pc.isRope)
            pc.ChangeState(new PSRope(pc));

        if (pc.isTirolina)
            pc.ChangeState(new PSZipLine(pc));
    }

    public override void FixedUpdate()
    {
        /*if (pc.rigidbody2d.velocity.x < pc.model.speed)
            pc.rigidbody2d.velocity = new Vector2(gc.GetVelocity(pc.model.speed), pc.rigidbody2d.velocity.y);
        else
            pc.rigidbody2d.velocity = new Vector2(gc.GetVelocity(pc.rigidbody2d.velocity.x), pc.rigidbody2d.velocity.y);*/

        pc.rigidbody2d.velocity = new Vector2(gc.GetVelocity(pc.model.speed), pc.rigidbody2d.velocity.y);

    }

    public override void Update()
    {
        return;
    }
}
