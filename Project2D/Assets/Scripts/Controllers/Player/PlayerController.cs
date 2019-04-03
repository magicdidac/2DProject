using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerModel _playerModel;

    private Player _player;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _fallMultiplier;
    [SerializeField]
    private float _lowMultiplier; //not implemented yet

    [SerializeField]
    private float _maxVerticalSpeed = 0.1f;

    [SerializeField]
    private int precisionDown;
    [SerializeField]
    private float offset;
    [SerializeField]
    private LayerMask groundMask;

    [HideInInspector] bool _isSliding;

    private SpriteRenderer spr;
    [HideInInspector] public Animator anim;

    [HideInInspector] public PlayerState currentState;

    void Start()
    {
        _player = new Player(_speed, _fallMultiplier, GetComponent<Rigidbody2D>(),_maxVerticalSpeed);
        spr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        ChangeState(new PSGrounded(this));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentState.FixedUpdate(this);
        _player.Move();
    }

    private void Update()
    {
        currentState.Update(this);
        groundCollision();
        _player.Jump();
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
                _player.CanJump = true;
                return;
            }
        }

        _player.CanJump = false;

    }

}
