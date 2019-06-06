using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESFloatingUp : AState
{
    //Additional temporal variable to test the rb velocity to go up while floating and multiplie br RadiusDetection to test when fall down
    private readonly float plusOffset = 1.5f;

    [HideInInspector] private EnemyController ec;

    public ESFloatingUp(EnemyController _ec) : base()
    {
        ec = _ec;
        ec.rigidbody2d.bodyType = RigidbodyType2D.Kinematic;
        ec.col.isTrigger = true;
        ec.shield.gameObject.SetActive(true);
        gc.audioController.PlaySound("shield");
    }

    public override void CheckTransition()
    {
        if (ec.DetectGroundToLand())
        {
            ec.rigidbody2d.bodyType = RigidbodyType2D.Dynamic;
            ec.col.isTrigger = false;
            ec.shield.gameObject.SetActive(false);
            gc.audioController.StopSound("dropping");
            gc.audioController.StopSound("shield");
            ec.ChangeState(new ESOnAir(ec));
        }
        if (gc.GetFloor() != 0)
        {
            ec.rigidbody2d.bodyType = RigidbodyType2D.Dynamic;
            ec.col.isTrigger = false;
            gc.audioController.StopSound("dropping");
            gc.audioController.StopSound("shield");
            ec.ChangeState(new ESOtherFloor(ec));
        }
    }

    public override void FixedUpdate()
    {
        
    }

    public override void Update()
    {
        if (ec.transform.position.y < 1)
            ec.rigidbody2d.velocity = new Vector2(gc.GetVelocity(ec.rigidbody2d.velocity.x), plusOffset);
        else
        {
            ec.rigidbody2d.velocity = new Vector2(gc.GetVelocity(ec.rigidbody2d.velocity.x), 0);
            ec.transform.position = new Vector3(ec.transform.position.x, 1);
        }
    }
}
