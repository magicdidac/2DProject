using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSTirolina : AState
{
    private float tirolinaSize;
    private Transform endPoint;

    public PSTirolina(AMoveController pc)
    {
        pc.rb.gravityScale = 0;
        endPoint = pc.transform.parent.Find("EndPoint");
        tirolinaSize = endPoint.position.x - pc.transform.position.x;
    }

    public override void CheckTransition(AMoveController pc)
    {
        if (pc.transform.position.x >= endPoint.position.x)
        {
            pc.isTirolina = false;
            pc.transform.parent = null;
            pc.ChangeState(new PSOnAir(pc));
        }
    }

    public override void FixedUpdate(AMoveController pc)
    {
        pc.rb.velocity = (endPoint.position - pc.transform.position).normalized * pc._playerModel.speed;
    }

    public override void Update(AMoveController pc)
    {
        Jump(pc);
    }

    private void Jump(AMoveController pc)
    {
        //pc._playerModel.jumpForce = 14f;
        float distance = endPoint.position.x - pc.transform.position.x;
        if (!pc.isTirolinaD && Input.GetButtonDown("Jump") && distance < tirolinaSize - 1) //-> saltar cuando me de la gana
        //if (!pc.isTirolinaD && Input.GetButtonDown("Jump") && distance < 2) --> saltar solo al final
        {
            pc.isTirolina = false;
            pc.transform.parent = null;
            pc.ChangeState(new PSOnAir(pc));
            pc.rb.velocity = Vector2.up * pc._playerModel.jumpForce;
        }
    }
}
