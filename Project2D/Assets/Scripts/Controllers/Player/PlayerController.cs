using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : AMoveController
{   

    [SerializeField] public LayerMask groundMask;   
    [SerializeField] public LayerMask trampolineMask;

    private void Awake()
    {
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
                gc.enemy.AddDistance();
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
            gc.floor++;
            isRope = true;
            col.GetComponent<Rope> ().startMovement();
        }
        else if (col.CompareTag("Down"))
            gc.floor--;
        else if (col.CompareTag("Trampoline"))
        {
            isTrampoline = detectCollision(trampolineMask, _playerModel.trampolineOffset);
            if (isTrampoline)
                gc.floor++;
        }

        else if (col.CompareTag("Coin"))
        {
            GameController.instance.AddScore(1);
            col.gameObject.SetActive(false);
        }

        else if (col.CompareTag("SuperCoin"))
        {
            GameController.instance.AddScore(5);
            col.gameObject.SetActive(false);
        }
    }


    #region Gizmos

    private void OnDrawGizmos()
    {
        spr = GetComponent<SpriteRenderer>();

        drawGroundRayCast();
        drawTrampolineRayCast();
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

    #endregion

}
