using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSTirolina : AState
{
    public override void CheckTransition(PlayerController pc)
    {
        Transform endPoint = pc.transform.parent.Find("EndPoint");
        if (pc.transform.position.x >= endPoint.position.x)
        {
            pc.isTirolina = false;
            pc.rb.bodyType = RigidbodyType2D.Dynamic;
            //pc.rb.AddForce(Vector2.one * pc._playerModel.exitRopeForce, ForceMode2D.Impulse);
            pc.ChangeState(new PSOnAir(pc));
        }
    }

    public override void FixedUpdate(PlayerController pc)
    {
        Transform endPoint = pc.transform.parent.Find("EndPoint");
        //pc.transform.position = Vector3.Lerp(pc.transform.position, new Vector3(pc.transform.parent.position.x, pc.transform.position.y, 0), .5f);
        pc.transform.position = Vector3.Lerp(pc.transform.position, new Vector3(endPoint.position.x, endPoint.transform.position.y, 0), .1f);
        //pc.rb.velocity = new Vector2(0, pc.rb.velocity.y);
        //pc.rb.velocity = new Vector2(pc.rb.velocity.x, 0);
    }

    public override void Update(PlayerController pc)
    {
        
    }
}
