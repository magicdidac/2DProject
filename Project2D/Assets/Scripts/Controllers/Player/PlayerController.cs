using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour, IMoveController
{

    //Components
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public SpriteRenderer spr;
    [HideInInspector] public Animator anim;

    //State
    [HideInInspector] public AState currentState;
    

    [SerializeField] public LayerMask groundMask;
    [SerializeField] public LayerMask trampolineMask;

    [SerializeField] public PlayerModel _playerModel;

    [SerializeField] private Vector3 stateInfoPosition = Vector3.zero;
    [SerializeField] private int fontSize = 20;


    //NEW
    
    [HideInInspector] public bool isGrounded = false;
    [HideInInspector] public bool isTrampoline = false;
    [HideInInspector] public bool isStuned = false;
    [HideInInspector] public bool isSliding = false;
    [HideInInspector] public bool isRope = false;
    [HideInInspector] public bool isTirolina = false;
    [HideInInspector] public int floor = 0;


    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        _playerModel = Instantiate(_playerModel);

        ChangeState(new PSGrounded(this));
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        currentState.FixedUpdate(this);
    }

    private void Update()
    {
        currentState.Update(this);
        isGrounded = detectCollision(groundMask);
        //isTrampoline = detectCollision(trampolineMask);
    }

    private void LateUpdate()
    {
        currentState.CheckTransition(this);
    }

    public void ChangeState(AState ps) { currentState = ps; }

    public bool detectCollision(LayerMask p_lm)
    {
        List<RaycastHit2D> hits = new List<RaycastHit2D>();

        float distanceBetweenRays = spr.bounds.size.x / _playerModel.precisionDown;


        for (int i = 0; i <= _playerModel.precisionDown; i++)
        {
            Vector3 startPoint = new Vector3((spr.bounds.min.x + (_playerModel.offset / 2)) + distanceBetweenRays * i, spr.bounds.min.y, 0);
            hits.Add(Physics2D.Raycast(startPoint, Vector2.down, .1f, p_lm));
        }

        foreach (RaycastHit2D hit in hits)
        {
            if (hit)
            {
                return true;
            }
        }

        return false;
    }

    private void OnTriggerEnter2D(Collider2D col)
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
        else if (col.CompareTag("Rope") && !isRope)
        {
            //Add rope animation start
            transform.SetParent(col.gameObject.transform);
            rb.velocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Kinematic;
            floor++;
            isRope = true;
            col.GetComponent<Rope> ().startMovement();
        }

        else if (col.CompareTag("Down"))
        {
            floor--;
        }

        else if (col.CompareTag("Trampoline"))
        {
            isTrampoline = detectCollision(trampolineMask);
            if (isTrampoline)
            {
                floor++;
            }
        }

        else if (col.CompareTag("Tirolina"))
        {
            transform.SetParent(col.gameObject.transform);
            isTirolina = true;
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.velocity = Vector2.zero;
            //float endPoint = col.bounds.max.x;
            //if (transform.position.x >= endPoint)
            //    rb.bodyType = RigidbodyType2D.Dynamic;
            /* Tendria que buscar donde calcular esto del limite de manera mas limpia */
        }
    }

    private void OnDrawGizmos()
    {
        spr = GetComponent<SpriteRenderer>();

        float distanceBetweenRays = (spr.bounds.size.x - _playerModel.offset) / _playerModel.precisionDown;


        for (int i = 0; i <= _playerModel.precisionDown; i++)
        {
            Vector3 startPoint = new Vector3((spr.bounds.min.x + (_playerModel.offset / 2)) + distanceBetweenRays * i, spr.bounds.min.y, 0);
            Debug.DrawLine(startPoint, startPoint + (Vector3.down * .1f), Color.red);
        }

        if (currentState != null)
        {
            GUIStyle style = new GUIStyle();
            style.normal.textColor = Color.red;
            style.fontSize = fontSize;
            style.alignment = TextAnchor.MiddleLeft;
            Handles.Label(stateInfoPosition + Camera.main.transform.position, "Current state: " + currentState + "\nFloor: " + floor, style);
        }
    }

}
