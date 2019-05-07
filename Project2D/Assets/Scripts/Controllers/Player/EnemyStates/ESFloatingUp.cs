using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESFloatingUp : AState
{
    //Additional temporal variable to test the rb velocity to go up while floating and multiplie br RadiusDetection to test when fall down
    private readonly float plusOffset = 1.5f;

    public ESFloatingUp(AMoveController pc)
    {
        EnemyController ec = ((EnemyController)pc);
        ec.canShoot = false;
        ec.canCharge = false;
        ec.rb.bodyType = RigidbodyType2D.Kinematic;
        ec.col.isTrigger = true;
        ec.shield.gameObject.SetActive(true);
    }

    public override void CheckTransition(AMoveController pc)
    {
        EnemyController ec = ((EnemyController)pc);
        if (ec.DetectGroundToLand())
        {
            ec.rb.bodyType = RigidbodyType2D.Dynamic;
            ec.col.isTrigger = false;
            ec.shield.gameObject.SetActive(false);
            ec.ChangeState(new ESOnAir(ec));
        }
    }

    public override void FixedUpdate(AMoveController pc)
    {
        
    }

    public override void Update(AMoveController pc)
    {
        EnemyController ec = (EnemyController)pc;
        if (ec.transform.position.y < 1)
            ec.rb.velocity = new Vector2(ec.rb.velocity.x, plusOffset);
        else
            ec.rb.velocity = new Vector2(ec.rb.velocity.x, 0);
    }
}
