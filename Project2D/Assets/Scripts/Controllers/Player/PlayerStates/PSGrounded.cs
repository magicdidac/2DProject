using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSGrounded : PlayerState
{
    public PSGrounded(PlayerController pc)
    {

    }

    public override void CheckTransition(PlayerController pc)
    {
        if (!pc.isGrounded) pc.ChangeState(new PSOnAir(pc));
    }

    public override void FixedUpdate(PlayerController pc)
    {
        pc.rb.velocity = new Vector2(pc._playerModel.speed, pc.rb.velocity.y);
    }

    public override void Update(PlayerController pc)
    {
        //pc.groundCollision();
        Jump(pc);
    }

    /*private void groundCollision(PlayerController pc)
    {
        List<RaycastHit2D> hits = new List<RaycastHit2D>();

        float distanceBetweenRays = pc.spr.bounds.size.x / pc.precisionDown;


        for (int i = 0; i <= pc.precisionDown; i++)
        {
            Vector3 startPoint = new Vector3((pc.spr.bounds.min.x + (pc.offset / 2)) + distanceBetweenRays * i, pc.spr.bounds.min.y, 0);
            hits.Add(Physics2D.Raycast(startPoint, Vector2.down, .1f, pc.groundMask));
        }

        foreach (RaycastHit2D hit in hits)
        {
            if (hit)
            {
                pc.isGrounded = true;
                return;
            }
        }
        pc.isGrounded = true;
    }*/

    private void Jump(PlayerController pc)
    {
        if (pc.isGrounded && Input.GetButtonDown("Jump"))
        {
            pc.rb.velocity = Vector2.up * pc._playerModel.jumpForce;
        }
    }
}
