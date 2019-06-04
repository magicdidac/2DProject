using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESFloatingUp : AState
{
    //Additional temporal variable to test the rb velocity to go up while floating and multiplie br RadiusDetection to test when fall down
    private readonly float plusOffset = 1.5f;

    [HideInInspector] private EnemyController ec;

    public ESFloatingUp(EnemyController _ec)
    {
        ec = _ec;
        ec.rb.bodyType = RigidbodyType2D.Kinematic;
        ec.col.isTrigger = true;
        ec.shield.gameObject.SetActive(true);
    }

    public override void CheckTransition()
    {
        if (ec.DetectGroundToLand())
        {
            ec.rb.bodyType = RigidbodyType2D.Dynamic;
            ec.col.isTrigger = false;
            ec.shield.gameObject.SetActive(false);
            ec.ChangeState(new ESOnAir(ec));
        }
        if (ec.gc.GetFloor() != 0)
        {
            ec.rb.bodyType = RigidbodyType2D.Dynamic;
            ec.col.isTrigger = false;
            ec.ChangeState(new ESOtherFloor(ec));
        }
    }

    public override void FixedUpdate()
    {
        
    }

    public override void Update()
    {
        if (ec.transform.position.y < 1)
            ec.rb.velocity = new Vector2(ec.gc.GetVelocity(ec.rb.velocity.x), plusOffset);
        else
        {
            ec.rb.velocity = new Vector2(ec.gc.GetVelocity(ec.rb.velocity.x), 0);
            ec.transform.position = new Vector3(ec.transform.position.x, 1);
        }
    }
}
