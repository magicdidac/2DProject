using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSZipLine : AState
{
    private float tirolinaSize;
    private ZipLine zip;
    private float startTime;

    private AMoveController moveController;

    public PSZipLine(AMoveController pc)
    {
        moveController = pc;
        zip = pc.zipLine;
        startTime = Time.time;

        pc.rb.gravityScale = 0;
        pc.rb.velocity = Vector2.zero;
        tirolinaSize = zip.endPoint.position.x - pc.transform.position.x;
        pc._playerModel.speed = pc._playerModel.normalSpeed;
        pc.transform.position = new Vector2(pc.transform.position.x, GetVerticalPosition());

        pc.anim.SetBool("B-ZipLine", true);
        pc.anim.SetTrigger("T-ZipLine");

    }

    public override void CheckTransition(AMoveController pc)
    {
        if (pc.transform.position.x >= zip.endPoint.position.x)
        {
            ChangeStateTo(new PSOnAir(pc));
            moveController.anim.SetTrigger("T-ZipLineOut");
        }

        if (Input.GetKeyDown(KeyCode.S))
            ChangeStateTo(new PSSliding(pc));
    }

    private void ChangeStateTo(AState state)
    {
        zip.col.enabled = false;

        moveController.isTirolina = false;
        moveController.transform.parent = null;
        moveController.ChangeState(state);
    }

    public override void FixedUpdate(AMoveController pc)
    {
        Vector2 startPoint = new Vector2 (pc.transform.position.x, pc.transform.position.y);
        Vector2 destPoint = new Vector2(zip.endPoint.position.x, zip.endPoint.position.y-1.25f);
        pc.rb.velocity = (destPoint - startPoint).normalized * pc._playerModel.speed;
    }

    public override void Update(AMoveController pc)
    {
        Jump(pc);
    }

    private void Jump(AMoveController pc)
    {
        float distance = zip.endPoint.position.x - pc.transform.position.x;
        if (!pc.isTirolinaD && Input.GetButtonDown("Jump") && distance < tirolinaSize - 1) //-> saltar cuando me de la gana
        //if (!pc.isTirolinaD && Input.GetButtonDown("Jump") && distance < 2) //--> saltar solo al final
        {
            pc.rb.velocity = Vector2.up * pc._playerModel.jumpForce;
            ChangeStateTo(new PSOnAir(pc));
            moveController.anim.SetTrigger("T-ZipLineOut");
        }
    }

    private float GetVerticalPosition()
    {
        float total = zip.endPoint.position.x - zip.startPoint.position.x;
        float current = zip.endPoint.position.x - moveController.transform.position.x;
        float percentage = (current * 100) / total;
        percentage = (100 - percentage);
        float totalVertical = zip.endPoint.position.y - zip.startPoint.position.y;
        float currentvertical = (totalVertical * percentage) / 100;
        return (zip.startPoint.position.y + currentvertical)-1.25f;
    }

}

