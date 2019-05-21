using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESOtherFloor : AState
{
    [HideInInspector] private EnemyController ec;

    public ESOtherFloor(EnemyController _ec)
    {
        ec = _ec;
        ec.rb.bodyType = RigidbodyType2D.Kinematic;
    }

    public override void CheckTransition()
    {
        if(ec.gc.GetFloor() == 0 && ec.DetectGroundToLand())
        {
            ec.rb.bodyType = RigidbodyType2D.Dynamic;
            ec.shield.gameObject.SetActive(false);
            ec.ChangeState(new ESOnAir(ec));
        }
    }

    public override void FixedUpdate()
    {
        
    }

    public override void Update()
    {
        ec.rb.velocity = new Vector2(ec.rb.velocity.x, 0);
        ec.transform.position = Vector3.Lerp(ec.transform.position, new Vector3(ec.transform.position.x, 1), .1f);
    }
}
