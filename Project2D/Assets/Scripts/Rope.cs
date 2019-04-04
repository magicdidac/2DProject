using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Rope : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Vector3 initialPosition;
    private bool move = false;

    [SerializeField]
    private float speed = 1.5f;

    [SerializeField]
    private int offset = 8;

    private void Start()
    {
        initialPosition = transform.position;
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (move && transform.position.y < initialPosition.y + offset)
            rb2d.velocity = Vector2.up * speed;
        else if(transform.position.y > initialPosition.y + offset) {
            move = false;
            if(transform.childCount != 0)
                transform.GetChild(0).transform.parent = null;
            rb2d.velocity = Vector2.zero;
        }
    }

    public void startMovement() { move = true; }

}
