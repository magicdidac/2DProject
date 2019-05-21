using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSDead : AState
{
    public PSDead(PlayerController pc)
    {
        
        pc.rb.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

        /*pc.rb.sharedMaterial.bounciness = 3f;
        pc.rb.sharedMaterial.friction = .2f;*/
    }

    public override void CheckTransition()
    {
        
    }

    public override void FixedUpdate()
    {
        
    }

    public override void Update()
    {
        
    }
}
