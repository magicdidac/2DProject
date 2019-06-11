using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSTrampoline : AState
{

    [HideInInspector] private PlayerController pc;

    public PSTrampoline(PlayerController _pc) : base()
    {
        pc = _pc;
        gc.audioController.PlaySound("trampoline");
        GameObject.FindGameObjectWithTag("Trampoline").GetComponent<Trampoline>().Shoot();
    }

    public override void CheckTransition()
    {
        if (pc.transform.position.y > 8 * gc.GetFloor())
        {
            pc.isTrampoline = false;
            pc.ChangeState(new PSOnAir(pc));
        }
    }

    public override void FixedUpdate()
    {
        pc.rigidbody2d.velocity = new Vector2(3, pc.model.jumpForce);
    }

    public override void Update() { }
}
