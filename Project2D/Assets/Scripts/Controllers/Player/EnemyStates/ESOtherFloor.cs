using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESOtherFloor : AState
{
    public ESOtherFloor(AMoveController pc)
    {
        pc.rb.bodyType = RigidbodyType2D.Kinematic;
    }

    public override void CheckTransition(AMoveController pc)
    {
        EnemyController ec = (EnemyController)pc;
        if(pc.gc.getFloor() == 0 && ec.DetectGroundToLand())
        {
            pc.rb.bodyType = RigidbodyType2D.Dynamic;
            ec.shield.gameObject.SetActive(false);
            pc.ChangeState(new ESOnAir(pc));
        }
    }

    public override void FixedUpdate(AMoveController pc)
    {
        
    }

    public override void Update(AMoveController pc)
    {
        pc.rb.velocity = new Vector2(pc.rb.velocity.x, 0);
        pc.transform.position = Vector3.Lerp(pc.transform.position, new Vector3(pc.transform.position.x, 1), .1f);
    }
}
