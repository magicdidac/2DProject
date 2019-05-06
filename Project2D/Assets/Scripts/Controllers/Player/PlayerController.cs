using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : AMoveController
{   

    [SerializeField] public LayerMask groundMask;   
    [SerializeField] public LayerMask trampolineMask;

    [HideInInspector] private GameObject downObject;

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
        if(anim != null)
            anim.SetBool("B-Ground",isGrounded);
        currentState.Update(this);
        isGrounded = detectCollision(groundMask, _playerModel.offset);
    }

    private void LateUpdate()
    {
        currentState.CheckTransition(this);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Box"))
        {
            if (isGrounded && !anim.GetBool("B-Slide"))
            {
                gc.enemy.AddDistance();
                isStuned = true;
                col.gameObject.SetActive(false);
            }
            else col.gameObject.SetActive(false);
        }
        else if (col.CompareTag("Rope") && !isRope)
        {
            transform.SetParent(col.gameObject.transform);
            rb.velocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Kinematic;
            gc.setFloor(gc.getFloor() + 1);
            isRope = true;
            col.GetComponent<Rope>().startMovement();
        }
        else if (col.CompareTag("Down") && col.gameObject != downObject)
        {
            downObject = col.gameObject;
            gc.setFloor(gc.getFloor() - 1);
        }
        else if (col.CompareTag("Trampoline"))
        {
            isTrampoline = detectCollision(trampolineMask, _playerModel.trampolineOffset);
            if (isTrampoline)
                gc.setFloor(gc.getFloor() + 1);
        }
        else if (col.tag.Contains("Tirolina"))
        {
            zipLine = col.gameObject.transform.parent.GetComponent<ZipLine> ();
            isTirolina = true;
            gc.setFloor(gc.getFloor() + zipLine.floorDiference);
        }
        else if (col.CompareTag("Coin"))
        {
            gc.AddCoins(1);
            col.gameObject.SetActive(false);
        }

        else if (col.CompareTag("SuperCoin"))
        {
            gc.AddCoins(5);
            col.gameObject.SetActive(false);
        }
        else if (col.CompareTag("Combustible"))
        {
            gc.GetCombustible(col.gameObject.GetComponent<Combustible>()._combustibleModel.quantity);
            col.gameObject.SetActive(false);
        }

        else if (col.CompareTag("Enemy"))
        {
            gc.GameWin(true);
        }

        /*else if (col.CompareTag("Kill"))
        {
            gc.GameWin(false);
        }*/
    }


    #region Gizmos

    private void OnDrawGizmos()
    {
        spr = transform.GetChild(0).GetComponent<SpriteRenderer>();

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
