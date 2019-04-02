using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    float FallMultiplier { get; }
    float LowMultiplier { get; }
    float Speed { get; }
    public bool CanJump { get; set; }
    public Rigidbody2D RigidBody { get; set; }

    public Player(float p_Speed, float p_FallMultiplier, Rigidbody2D p_rb)
    {
        this.FallMultiplier = p_FallMultiplier;
        this.Speed = p_Speed;
        this.RigidBody = p_rb;
        CanJump = false;
    }

    public void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            RigidBody.velocity = Vector2.up * FallMultiplier;

            //RigidBody.AddForce(Vector2.up * FallMultiplier, ForceMode2D.Impulse);

            /*else if (_rb.velocity.y > 0 && !Input.GetButtonDown("Jump"))
            {
                _rb.velocity += Vector2.up * _lowMultiplier;
            }*/
        }
    }
}
