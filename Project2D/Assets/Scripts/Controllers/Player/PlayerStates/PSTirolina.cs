using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSTirolina : AState
{
    public override void CheckTransition(PlayerController pc)
    {
        float endpoint = pc.GetComponent<Bounds>().max.x;
        if (pc.transform.position.x >= endpoint)
        {
            pc.isTirolina = false;
            pc.rb.bodyType = RigidbodyType2D.Dynamic;
            //pc.rb.AddForce(Vector2.one * pc._playerModel.exitRopeForce, ForceMode2D.Impulse);
            pc.ChangeState(new PSOnAir(pc));
        }
    }

    public override void FixedUpdate(PlayerController pc)
    {
        //pc.rb.velocity = Vector2.zero;
        //pc.rb.bodyType = RigidbodyType2D.Kinematic;
    }

    public override void Update(PlayerController pc)
    {
        
    }
}
