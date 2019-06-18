using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESOnAir : AState
{
    [HideInInspector] private EnemyController ec;

    public ESOnAir(EnemyController _ec) : base()
    {
        ec = _ec;
    }

    public override void CheckTransition()
    {
        
        if (ec.isGrounded)
        {
            ec.animator.SetBool("B-Ground", true);
            ec.ChangeState(new ESGrounded(ec));
        }

        if (ec.transform.position.y < -1)
            ec.ChangeState(new ESFloatingUp(ec));

        if (ec.DetectGroundToLand("Down"))
            ec.ChangeState(new ESFloatingUp(ec));

        if (ec.DetectObstacleToFall(ec.obstacleMask))
        {
            ec.rigidbody2d.velocity = Vector2.down * ec.model.jumpForce * 2;
        }

    }

    public override void FixedUpdate()
    {
    }

    public override void Update()
    {
    }
}
