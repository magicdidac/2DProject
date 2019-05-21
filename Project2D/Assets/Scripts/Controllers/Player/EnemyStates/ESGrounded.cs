using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESGrounded : AState
{
    [HideInInspector] private EnemyController ec;

    public ESGrounded(EnemyController _ec)
    {
        ec = _ec;       
        ec.shield.gameObject.SetActive(false);
    }

    public override void CheckTransition()
    {
        if (ec.DetectObstacleToJump())
        {
            Jump();
            ec.ChangeState(new ESOnAir(ec));
        }

        if (!ec.DetectGroundToLand()) ec.ChangeState(new ESFloatingUp(ec));
        if (ec.DetectObstacleToSlide()) ec.ChangeState(new ESSliding(ec));
        if (ec.gc.GetFloor() != 0)
            ec.ChangeState(new ESOtherFloor(ec));
    }

    public override void FixedUpdate() {}

    public override void Update() {}

    private void Jump()
    {
        ec.anim.SetTrigger("T-Jump");
        ec.anim.SetBool("B-Ground", false);
        ec.rb.velocity = Vector2.up * ec.model.jumpForce;
    }

}
