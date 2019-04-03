using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerModel _playerModel;

    [SerializeField]
    public LayerMask groundMask;

    [HideInInspector] bool _isSliding;

    [HideInInspector] public SpriteRenderer spr;
    [HideInInspector] public Animator anim;

    [HideInInspector] public PlayerState currentState;

    //NEW
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public bool isGrounded = false;
    [HideInInspector] public bool isStuned = false;


    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        _playerModel = Instantiate(_playerModel);

        ChangeState(new PSGrounded(this));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector2(_playerModel.speed, rb.velocity.y);

        currentState.FixedUpdate(this);
    }

    private void Update()
    {
        currentState.Update(this);
        groundCollision();
        //Jump();
    }

    private void LateUpdate()
    {
        currentState.CheckTransition(this);
    }

    public void ChangeState(PlayerState ps)
    {
        currentState = ps;
        //Debug.Log("CurrentState = " + currentState);
    }

    private void OnDrawGizmos()
    {
        spr = GetComponent<SpriteRenderer>();

        float distanceBetweenRays = (spr.bounds.size.x- _playerModel.offset) / _playerModel.precisionDown;


        for (int i = 0; i <= _playerModel.precisionDown; i++)
        {
            Vector3 startPoint = new Vector3((spr.bounds.min.x+(_playerModel.offset /2)) + distanceBetweenRays * i, spr.bounds.min.y, 0);
            Debug.DrawLine(startPoint, startPoint + (Vector3.down * .1f), Color.red);
        }
    }

    public void groundCollision()
    {
        List<RaycastHit2D> hits = new List<RaycastHit2D>();

        float distanceBetweenRays = spr.bounds.size.x / _playerModel.precisionDown;


        for (int i = 0; i <= _playerModel.precisionDown; i++)
        {
            Vector3 startPoint = new Vector3((spr.bounds.min.x + (_playerModel.offset / 2)) + distanceBetweenRays * i, spr.bounds.min.y, 0);
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
        isGrounded = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Box"))
        {
            if (isGrounded && !anim.GetBool("isSliding"))
            {
                isStuned = true;
                col.gameObject.SetActive(false);
            }
            else col.gameObject.SetActive(false);
        }
    }

    /*private void Jump()
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = Vector2.up * jumpForce;
        }
    }*/

}
