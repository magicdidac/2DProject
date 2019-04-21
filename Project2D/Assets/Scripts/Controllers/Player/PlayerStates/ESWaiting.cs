using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESWaiting : AState
{
    AMoveController myPc;
    private bool hasJump = false;

    public ESWaiting(AMoveController pc)
    {
        EnemyController ec = (EnemyController)pc;
        ec.calculateTimeToNextShoot();
        ec.isGrounded = true;

        if (ec.rb != null)
        {
            ec.rb.bodyType = RigidbodyType2D.Kinematic;
            ec.GetComponent<BoxCollider2D>().isTrigger = true;
        }
        ec.shield.gameObject.SetActive(false);
        Debug.Log(this);
    }

    public override void CheckTransition(AMoveController pc)
    {
        EnemyController ec = (EnemyController)pc;
        if (!ec.isGrounded && hasJump) ec.ChangeState(new ESOnAir(pc));
        else if (!ec.isGrounded) ec.ChangeState(new ESFloatingUp(pc));
    }

    public override void FixedUpdate(AMoveController pc)
    {

    }

    public override void Update(AMoveController pc)
    {
        EnemyController ec = (EnemyController)pc;

        RaycastHit2D hit = Physics2D.Raycast(new Vector2(ec.transform.position.x + ec.RadiusDetection, ec.transform.position.y + 1),
            Vector2.right, ec.RadiusDetection);

        if (hit.collider != null)
        {
            if (ec.isGrounded && (hit.collider.CompareTag("Box"))) //salta los Box y atraviesa los Kill o viceversa??
            {
                Debug.Log("Jump");
                ec.rb.velocity = Vector2.up * ec._playerModel.jumpForce;
                hasJump = true;
            }
        }
    }

    private void Jump(EnemyController ec)
    {
    }
}
