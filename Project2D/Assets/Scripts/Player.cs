using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private float FallMultiplier { get; }
    private float LowMultiplier { get; }
    private float Speed { get; }
    private Vector3 _refVelocity = Vector3.zero;
    private float _movementSmooth = .05f;

    public bool CanJump { get; set; }
    public Rigidbody2D RigidBody { get; set; }



    public Player(float p_Speed, float p_FallMultiplier, Rigidbody2D p_rb)
    {
        this.FallMultiplier = p_FallMultiplier;
        this.Speed = p_Speed;
        this.RigidBody = p_rb;
        CanJump = false;
    }

    public void Move()
    {
        // Move the character by finding the target velocity
        Vector3 v_targetVelocity = new Vector2(Speed * 10f, RigidBody.velocity.y);
        // And then smoothing it out and applying it to the character
        RigidBody.velocity = Vector3.SmoothDamp(RigidBody.velocity, v_targetVelocity, ref _refVelocity, _movementSmooth);
    }

    public void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (CanJump)
            {
                //RigidBody.velocity = Vector2.up * FallMultiplier;

                RigidBody.AddForce(new Vector2(0, FallMultiplier * 50), ForceMode2D.Impulse);

                /*else if (_rb.velocity.y > 0 && !Input.GetButtonDown("Jump"))
                {
                    _rb.velocity += Vector2.up * _lowMultiplier;
                }*/
            } 
        }
    }
}
