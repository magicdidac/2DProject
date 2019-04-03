using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private float FallMultiplier { get; }
    private float LowMultiplier { get; }
    private float Speed { get; }
    private float maxVSpeed { get; }
    private Vector3 _refVelocity = Vector3.zero;
    private float _movementSmooth = .05f;

    public bool CanJump { get; set; }
    public Rigidbody2D RigidBody { get; set; }



    public Player(float p_Speed, float p_FallMultiplier, Rigidbody2D p_rb, float p_maxVSpeed)
    {
        this.FallMultiplier = p_FallMultiplier;
        this.Speed = p_Speed;
        this.RigidBody = p_rb;
        this.maxVSpeed = p_maxVSpeed;
        CanJump = true;
    }

    public void Move()
    {
        RigidBody.velocity = new Vector2(Speed, RigidBody.velocity.y);

        if (RigidBody.velocity.y < -maxVSpeed)
            RigidBody.velocity = new Vector2(Speed, -maxVSpeed);

        if (RigidBody.velocity.y > maxVSpeed*2)
            RigidBody.velocity = new Vector2(Speed, maxVSpeed*2);

    }

    public void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (CanJump)
            {
                if (RigidBody.velocity.y <= 0f)
                {
                    Debug.Log("ENTRAAA");
                    //RigidBody.velocity = Vector2.up * FallMultiplier * Time.deltaTime;

                    //RigidBody.velocity = new Vector2(RigidBody.velocity.x, 0);
                    //RigidBody.AddForce(Vector2.up * FallMultiplier * Time.deltaTime, ForceMode2D.Impulse);

                    //RigidBody.velocity = (Vector2.up * Physics2D.gravity.y * FallMultiplier * Time.deltaTime) * -1;

                    RigidBody.velocity = Vector2.up * FallMultiplier;
                    CanJump = false; 
                }
                else
                    //RigidBody.velocity = (Vector2.up * Physics2D.gravity.y * LowMultiplier * Time.deltaTime) * -1;
                    RigidBody.velocity = Vector2.up * LowMultiplier;
            }
        }

        /*if (RigidBody.velocity.y > 0 && !CanJump)
        {
            RigidBody.velocity = (Vector2.up * Physics2D.gravity.y * LowMultiplier * Time.deltaTime) * -1;
        }*/
    }
}
