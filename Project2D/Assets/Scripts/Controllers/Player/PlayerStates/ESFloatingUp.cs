using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESFloatingUp : AState
{
    //Additional temporal variable to test the rb velocity to go up while floating and multiplie br RadiusDetection to test when fall down
    private readonly float plusOffset = 1.5f;

    public ESFloatingUp(AMoveController pc)
    {
        EnemyController ec = ((EnemyController)pc);
        ec.canShoot = false;
        ec.canCharge = false;
        ec.rb.bodyType = RigidbodyType2D.Kinematic;
        ec.GetComponent<BoxCollider2D>().isTrigger = true;
        ec.rb.velocity = Vector2.up * plusOffset;
        ec.shield.gameObject.SetActive(true);
        Debug.Log(this + "\n" + ec.isGrounded);
    }

    public override void CheckTransition(AMoveController pc)
    {
        EnemyController ec = ((EnemyController)pc);
        if (ec.isGrounded) ec.ChangeState(new ESGrounded(ec));
    }

    public override void FixedUpdate(AMoveController pc)
    {
        
    }

    public override void Update(AMoveController pc)
    {
        //Floating functionality
        EnemyController ec = (EnemyController)pc;
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(ec.transform.position.x, ec.transform.position.y + 1), Vector2.down, Mathf.Infinity);

        if (hit.collider == null) return;

        if (!ec.isGrounded && (hit.collider.CompareTag("Platform")))
        {
            ec.rb.bodyType = RigidbodyType2D.Dynamic;
            //ec.GetComponent<BoxCollider2D>().isTrigger = false;
            //ec.isGrounded = true;
        }
    }
}
