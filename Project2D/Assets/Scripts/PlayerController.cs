using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private Player _player;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _fallMultiplier;
    [SerializeField]
    private float _lowMultiplier; //not implemented yet

    void Start()
    {
        _player = new Player(_speed, _fallMultiplier, GetComponent<Rigidbody2D>());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _player.Move();
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
