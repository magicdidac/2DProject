using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSTirolina : AState
{
    public PSTirolina(AMoveController pc)
    {
        pc.rb.gravityScale = 0;
    }

    public override void CheckTransition(AMoveController pc)
    {
        Transform endPoint = pc.transform.parent.Find("EndPoint");
        if (pc.transform.position.x >= endPoint.position.x)
        {
            pc.isTirolina = false;
            pc.transform.parent = null;
            pc.ChangeState(new PSOnAir(pc));
        }
    }

    public override void FixedUpdate(AMoveController pc)
    {
        Transform endPoint = pc.transform.parent.Find("EndPoint");
        pc.rb.velocity = (new Vector3(endPoint.position.x, pc.transform.position.y) - pc.transform.position).normalized * pc._playerModel.speed;
    }

    public override void Update(AMoveController pc)
    {
        
    }
}
