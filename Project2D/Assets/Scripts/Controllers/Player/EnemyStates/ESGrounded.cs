using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESGrounded : AState
{
    [HideInInspector] private EnemyController ec;

    public ESGrounded(EnemyController _ec) : base()
    {
        ec = _ec;
    }

    public override void CheckTransition()
    {
        if (ec.DetectObstacleToJump())
        {
            Jump();
            ec.ChangeState(new ESOnAir(ec));
        }

        if (!ec.DetectGroundToLand()) ec.ChangeState(new ESFloatingUp(ec));

        //if (ec.DetectObstacleToSlide()) ec.ChangeState(new ESSliding(ec));
        if (gc.GetFloor() != 0)
            ec.ChangeState(new ESOtherFloor(ec));
    }

    public override void FixedUpdate() {}

    public override void Update() {}

    private void Jump()
    {
        ec.animator.SetTrigger("T-Jump");
        ec.animator.SetBool("B-Ground", false);
        ec.rigidbody2d.velocity = Vector2.up * ec.model.jumpForce;
    }

}
