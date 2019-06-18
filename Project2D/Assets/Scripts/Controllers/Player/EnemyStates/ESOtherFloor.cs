using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESOtherFloor : AState
{
    [HideInInspector] private EnemyController ec;

    public ESOtherFloor(EnemyController _ec) : base()
    {
        ec = _ec;
        ec.rigidbody2d.bodyType = RigidbodyType2D.Kinematic;
        ec.shield.SetTrigger("FadeIn");
    }

    public override void CheckTransition()
    {
        if(gc.GetFloor() == 0 && ec.DetectGroundToLand())
        {
            ec.rigidbody2d.bodyType = RigidbodyType2D.Dynamic;
            ec.shield.SetTrigger("FadeOut");
            ec.ChangeState(new ESOnAir(ec));
        }
    }

    public override void FixedUpdate()
    {
        
    }

    public override void Update()
    {
        ec.rigidbody2d.velocity = new Vector2(gc.GetVelocity(ec.rigidbody2d.velocity.x), 0);
        ec.transform.position = Vector3.Lerp(ec.transform.position, new Vector3(ec.transform.position.x, 1), .1f);
    }
}
