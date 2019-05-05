using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSZipLine : AState
{
    private float tirolinaSize;
    private Transform endPoint;

    private AMoveController moveController;

    public PSZipLine(AMoveController pc)
    {
        moveController = pc;

        pc.rb.gravityScale = 0;
        endPoint = pc.transform.parent.Find("EndPoint");
        tirolinaSize = endPoint.position.x - pc.transform.position.x;
        pc._playerModel.speed = pc._playerModel.normalSpeed;

        pc.anim.SetBool("B-ZipLine", true);
        pc.anim.SetTrigger("T-ZipLine");

    }

    public override void CheckTransition(AMoveController pc)
    {
        if (pc.transform.position.x >= endPoint.position.x)
            ChangeStateTo(new PSOnAir(pc));

        if (Input.GetKeyDown(KeyCode.S))
            ChangeStateTo(new PSSliding(pc));
    }

    private void ChangeStateTo(AState state)
    {
        moveController.transform.parent.GetComponent<BoxCollider2D>().enabled = false;
        moveController.anim.SetTrigger("T-ZipLineOut");

        moveController.isTirolina = false;
        moveController.transform.parent = null;
        moveController.ChangeState(state);
    }

    public override void FixedUpdate(AMoveController pc)
    {
        Vector2 startPoint = new Vector2 (pc.transform.position.x, endPoint.position.y-1);
        Vector2 destPoint = new Vector2(endPoint.position.x, endPoint.position.y-1);
        pc.transform.position = Vector2.Lerp(startPoint, destPoint, .01f);
        //pc.rb.velocity = (endPoint.position - pc.transform.position).normalized * pc._playerModel.speed;
    }

    public override void Update(AMoveController pc)
    {
        Jump(pc);
    }

    private void Jump(AMoveController pc)
    {
        float distance = endPoint.position.x - pc.transform.position.x;
        if (!pc.isTirolinaD && Input.GetButtonDown("Jump") && distance < tirolinaSize - 1) //-> saltar cuando me de la gana
        //if (!pc.isTirolinaD && Input.GetButtonDown("Jump") && distance < 2) //--> saltar solo al final
        {
            pc.rb.velocity = Vector2.up * pc._playerModel.jumpForce;
            ChangeStateTo(new PSOnAir(pc));
        }
    }
}
