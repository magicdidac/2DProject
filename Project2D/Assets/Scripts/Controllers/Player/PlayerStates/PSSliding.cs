using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSSliding : PlayerState
{
    float time = 0;

    public PSSliding(PlayerController pc)
    {
        pc.anim.SetBool("isSliding", true);
        
    }

    public override void CheckTransition(PlayerController pc)
    {
        if(time >= pc._playerModel.slideTime)
        {
            pc.anim.SetBool("isSliding", false);
            pc.ChangeState(new PSGrounded(pc));
        }
    }

    public override void FixedUpdate(PlayerController pc)
    {

    }

    public override void Update(PlayerController pc)
    {
        time += Time.deltaTime;
    }
}
