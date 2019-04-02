using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private Player _player;
    public float _speed;
    public float _fallMultiplier;
    public float _lowMultiplier; //not implemented yet

    void Start()
    {
        _player = new Player(_speed, _fallMultiplier, GetComponent<Rigidbody2D>());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _player.RigidBody.velocity = Vector2.right * _speed;
        _player.Jump();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider is EdgeCollider2D)
        {
            //is the ground
            _player.CanJump = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider is EdgeCollider2D)
        {
            //is the ground
            _player.CanJump = false;
        }
    }
}
