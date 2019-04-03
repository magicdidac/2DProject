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
    {/*
        // Move the character by finding the target velocity
        Vector3 v_targetVelocity = new Vector2(Speed * 10f, RigidBody.velocity.y);
        // And then smoothing it out and applying it to the character
        RigidBody.velocity = Vector3.SmoothDamp(RigidBody.velocity, v_targetVelocity, ref _refVelocity, _movementSmooth);*/

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
            Debug.Log("YAS");
            if (CanJump)
            {
                Debug.Log("YEEES");
                //RigidBody.velocity = Vector2.up * FallMultiplier;
                RigidBody.velocity = new Vector2(RigidBody.velocity.x, 0);
                RigidBody.AddForce(Vector2.up * FallMultiplier, ForceMode2D.Impulse);
                CanJump = false;

                /*else if (_rb.velocity.y > 0 && !Input.GetButtonDown("Jump"))
                {
                    _rb.velocity += Vector2.up * _lowMultiplier;
                }*/
            } 
        }
    }
}
