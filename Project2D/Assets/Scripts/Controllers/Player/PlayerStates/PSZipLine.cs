using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSZipLine : AState
{
    private float tirolinaSize;
    private ZipLine zip;


    [HideInInspector] private PlayerController pc;

    public PSZipLine(PlayerController _pc) : base()
    {

        pc = _pc;
        zip = pc.zipLine;

        pc.rigidbody2d.gravityScale = 0;
        pc.rigidbody2d.velocity = Vector2.zero;
        tirolinaSize = zip.endPoint.position.x - pc.transform.position.x;
        pc.model.speed = pc.model.normalSpeed;

        pc.animator.SetBool("B-ZipLine", true);
        pc.animator.SetTrigger("T-ZipLine");

    }

    public override void CheckTransition()
    {
        if (pc.transform.position.x >= zip.endPoint.position.x)
        {
            ChangeStateTo(new PSOnAir(pc));
            this.pc.animator.SetTrigger("T-ZipLineOut");
        }

        if (Input.GetKeyDown(KeyCode.S))
            ChangeStateTo(new PSSliding(pc));
    }

    private void ChangeStateTo(AState state)
    {
        zip.col.enabled = false;

        pc.isTirolina = false;
        pc.transform.parent = null;
        pc.ChangeState(state);
    }

    public override void FixedUpdate()
    {
        pc.rigidbody2d.velocity = new Vector2(gc.GetVelocity(pc.model.normalSpeed), 0);
        pc.transform.position = new Vector3(pc.transform.position.x, zip.GetPositionByPosition(pc.handObject.position).y - pc.GetVerticalDifferenceHand());
    }

    public override void Update()
    {
        Jump();
    }

    private void Jump()
    {
        float distance = zip.endPoint.position.x - pc.transform.position.x;
        //if (!pc.isTirolinaD && Input.GetButtonDown("Jump") && distance < tirolinaSize - 1) //-> saltar cuando me de la gana
        //if (!pc.isTirolinaD && Input.GetButtonDown("Jump") && distance < 2) //--> saltar solo al final
        if (!pc.isTirolinaD && Input.GetButtonDown("Jump"))
        {
            pc.rigidbody2d.velocity = Vector2.up * pc.model.jumpForce;
            ChangeStateTo(new PSOnAir(pc));
            this.pc.animator.SetTrigger("T-ZipLineOut");
        }
    }


}

