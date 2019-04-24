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

        /*RaycastHit2D hit = Physics2D.Raycast(new Vector2(ec.transform.position.x + ec.RadiusDetection, ec.transform.position.y + 1),
            Vector2.right, ec.RadiusDetection);

        if (hit.collider != null)
        {
            if (ec.isGrounded && (hit.collider.CompareTag("Box"))) //salta los Box y atraviesa los Kill o viceversa??
            {
                Debug.Log("Jump");
                ec.rb.velocity = Vector2.up * ec._playerModel.jumpForce;
                hasJump = true;
            }
        }*/
        Jump(ec);
    }

    private void Jump(EnemyController ec)
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(ec.transform.position.x + ec.RadiusDetection, ec.transform.position.y + 1),
            Vector2.right, ec.RadiusDetection);

        if (hit.collider != null)
        {
            if (ec.isGrounded && (hit.collider.CompareTag("Box"))) //salta los Box y atraviesa los Kill o viceversa??
            {
                float upDistance = Mathf.Abs(hit.collider.transform.position.y - ec.transform.position.y);
                float forwardDistance = Mathf.Abs(hit.collider.bounds.max.x - hit.collider.bounds.min.x);
                /*
                 * UpDistance
                 *      Box normal -> 0.75f
                 *      Box alta -> 1.16f
                 *      Box larga -> 0.75f
                 *      
                 * ForwardDistance
                 *      Box normal -> 1f
                 *      Box alta -> 2f
                 *      Box larga -> 2f
                 *      
                 *      podria utilizar simplemente la altura y ancchura de box sin tener en cuenta la propia del enemy
                 */

                Debug.Log("UpDistance: " + upDistance);
                Debug.Log("Jump");
                ec.rb.velocity = Vector2.up * ec._playerModel.jumpForce * (upDistance * (forwardDistance));
                hasJump = true;
            }
        }

    }
}
