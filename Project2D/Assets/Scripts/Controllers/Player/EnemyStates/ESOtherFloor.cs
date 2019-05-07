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
        if(pc.gc.getFloor() == 0)
        {
            pc.rb.bodyType = RigidbodyType2D.Dynamic;
            pc.ChangeState(new ESOnAir(pc));
        }
    }

    public override void FixedUpdate(AMoveController pc)
    {
        
    }

    public override void Update(AMoveController pc)
    {
        pc.rb.velocity = new Vector2(pc.rb.velocity.x, 0);
    }
}
