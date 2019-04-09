using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class PlayerController : AMoveController
{   

    [SerializeField] public LayerMask groundMask;
    [SerializeField] public LayerMask trampolineMask;

    [SerializeField] private Vector3 stateInfoPosition = Vector3.zero;
    [SerializeField] private int fontSize = 20;


    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        _playerModel = Instantiate(_playerModel);
        combustible = _playerModel.maxCombustible;

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
        isGrounded = detectCollision(groundMask, _playerModel.offset);
        //isTrampoline = detectCollision(trampolineMask);
    }

    private void LateUpdate()
    {
        currentState.CheckTransition(this);
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
            isTrampoline = detectCollision(trampolineMask, _playerModel.trampolineOffset);
            if (isTrampoline)
                floor++;
        }
        else if (col.CompareTag("Combustible"))
        {
            GameController.instance.GetCombustible(col.gameObject.GetComponent<Combustible>()._combustibleModel.quantity);
            col.gameObject.SetActive(false);
        }
    }


    #region Gizmos

    private void OnDrawGizmos()
    {
        spr = GetComponent<SpriteRenderer>();

        drawGroundRayCast();
        drawTrampolineRayCast();
        drawState();
    }

    private void drawGroundRayCast()
    {
        float distanceBetweenRays = (spr.bounds.size.x - _playerModel.offset) / _playerModel.precisionDown;

        for (int i = 0; i <= _playerModel.precisionDown; i++)
        {
            Vector3 startPoint = new Vector3((spr.bounds.min.x + (_playerModel.offset / 2)) + distanceBetweenRays * i, spr.bounds.min.y, 0);
            Debug.DrawLine(startPoint, startPoint + (Vector3.down * .1f), Color.red);
        }
    }

    private void drawTrampolineRayCast()
    {
        float distanceBetweenRays = (spr.bounds.size.x - _playerModel.trampolineOffset) / _playerModel.precisionDown;

        for (int i = 0; i <= _playerModel.precisionDown; i++)
        {
            Vector3 startPoint = new Vector3((spr.bounds.min.x + (_playerModel.trampolineOffset / 2)) + distanceBetweenRays * i, spr.bounds.min.y, 0);
            Debug.DrawLine(startPoint, startPoint + (Vector3.down * .1f), Color.magenta);
        }
    }

    private void drawState()
    {
        if (currentState != null)
        {
            GUIStyle style = new GUIStyle();
            style.normal.textColor = Color.red;
            style.fontSize = fontSize;
            style.alignment = TextAnchor.MiddleLeft;
            Handles.Label(stateInfoPosition + Camera.main.transform.position, "Current state: " + currentState + "\nFloor: " + floor, style);
        }
    }

    #endregion

}
