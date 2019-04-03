using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private int precisionDown;
    [SerializeField]
    private float offset;
    [SerializeField]
    private LayerMask groundMask;

    private SpriteRenderer spr;


    //NEW
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float jumpForce;
    private Rigidbody2D rb;
    private bool isGrounded = false;


    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector2(_speed, rb.velocity.y);
    }

    private void Update()
    {

        groundCollision();
        Jump();
    }


    private void OnDrawGizmos()
    {
        spr = GetComponent<SpriteRenderer>();

        float distanceBetweenRays = (spr.bounds.size.x-offset) / precisionDown;


        for (int i = 0; i <= precisionDown; i++)
        {
            Vector3 startPoint = new Vector3((spr.bounds.min.x+(offset/2)) + distanceBetweenRays * i, spr.bounds.min.y, 0);
            Debug.DrawLine(startPoint, startPoint + (Vector3.down * .1f), Color.red);
        }
    }

    private void groundCollision()
    {
        List<RaycastHit2D> hits = new List<RaycastHit2D>();

        float distanceBetweenRays = spr.bounds.size.x / precisionDown;


        for (int i = 0; i <= precisionDown; i++)
        {
            Vector3 startPoint = new Vector3((spr.bounds.min.x + (offset / 2)) + distanceBetweenRays * i, spr.bounds.min.y, 0);
            hits.Add(Physics2D.Raycast(startPoint, Vector2.down, .1f, groundMask));
        }

        foreach (RaycastHit2D hit in hits)
        {
            if (hit)
            {
                isGrounded = true;
                return;
            }
        }
        isGrounded = true;
    }

    private void Jump()
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = Vector2.up * jumpForce;
        }
    }

}
